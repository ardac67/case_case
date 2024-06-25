
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

public class FilmRepository : IFilmRepository
{
    private readonly AppDbContext db;
    private readonly SmtpSettings smtpSettings;
    public FilmRepository(AppDbContext db,IOptions<SmtpSettings> smtpSettings)
    {
        this.db = db;
        this.smtpSettings = smtpSettings.Value;
    }

    public async Task<List<Film>> GetAllAsync()
    {
        return await db.Films.Include(f => f.comments).ToListAsync();
    }

    public async Task SaveAsync(Comments comment)
    {
        db.Comments.Add(comment);
        await db.SaveChangesAsync();
    }
        public async Task<List<Film>> GetFilmsLimitedAsync(int pageNumber, int pageSize)
    {
        return await db.Films
                        .OrderBy(f => f.ReleaseDate)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
    }

    public Task<List<Film>> GetFilmsByNameAsync(string Name)
    {
        return db.Films.Include(f=> f.comments).Where(f => f.Title.Equals(Name)).ToListAsync();
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(smtpSettings.SenderName, smtpSettings.SenderEmail));
        message.To.Add(new MailboxAddress("", to));
        message.Subject = subject;

        message.Body = new TextPart("plain")
        {
            Text = body
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port, false);
            await client.AuthenticateAsync(smtpSettings.Username, smtpSettings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

    public Task<Film> GetFilmByIdAsync(Guid id)
    {
        return db.Films.Include(f => f.comments).FirstOrDefaultAsync(f => f.Id == id);
    }
}