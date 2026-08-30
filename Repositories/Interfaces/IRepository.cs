using Homework2.Models;

namespace Homework2.Repositories.Interfaces
{
    public interface IRepository
    {
        Task Create(Movie movie);
        Task Delete(int? id);
        Task<Movie?> Get(int id);
        Task Set(int id, Movie movie);
    }
}
