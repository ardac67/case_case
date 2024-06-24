public class FilmGetDto
{
    public int Page { get; set; }
    public List<FilmPushDto> Results { get; set; }
    public int TotalPages { get; set; }
    public int TotalResults { get; set; }
}