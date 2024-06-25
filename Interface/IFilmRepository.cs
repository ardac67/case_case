public interface IFilmRepository
{
    Task<List<Film>> GetAllAsync();
}