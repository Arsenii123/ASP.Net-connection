using DataAccess.Models;

namespace BuisnessLogicPlayer.Services.Interfaces
{
    public interface IDetails
    {
        Task<Movie?> Details(int? id);

    }
}
