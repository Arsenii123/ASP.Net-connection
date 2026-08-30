using Homework2.Models;

namespace Homework2.Repositories.Interfaces
{
    public interface IRepository
    {
        public async Task Create(Movie movie) {
            Console.WriteLine("Create");
        }
        public async Task Delete(int? id) {
            Console.WriteLine("Delete");
        }
        public async Task<Movie> Get(int? id) {
            Console.WriteLine("Update");
            return null;
           
        }
        public async Task Set(int id, Movie movie)
        {

        }


    }
}
