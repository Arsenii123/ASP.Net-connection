using Homework2.Models;

namespace Homework2.Services.Interfaces
{
    public interface IDetails
    {
        public async Task<Movie> Details(int? id)
        {
            Console.WriteLine("Details");
            return null;
        }

    }
}
