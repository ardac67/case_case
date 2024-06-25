public class Comments
{
    public Guid Id { get; set; }
    public string Comment { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public Guid FilmId { get; set; }
    public Film Film { get; set; }

    public int Point { get; set; }
}