public interface IFilmRepository
{
    Task<List<Film>> GetAllAsync();
    public Task SaveAsync(Comments comment);
    public Task<List<Film>> GetFilmsLimitedAsync(int pageNumber, int pageSize);
    public Task<List<Film>> GetFilmsByNameAsync(string Name);

    public Task SendEmailAsync(string to, string subject, string body);

    Task<Film> GetFilmByIdAsync(Guid id);
}