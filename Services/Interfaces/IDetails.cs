using Homework2.Models;

namespace Homework2.Services.Interfaces
{
    public interface IDetails
    {
        Task<Movie?> Details(int? id);

    }
}
