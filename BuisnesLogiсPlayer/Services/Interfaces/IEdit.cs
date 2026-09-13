using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuisnessLogicPlayer.Services.Interfaces
{
    public interface IEdit
    {
        Task Edit(int id, Movie movie, IFormFile? posterFile);

    }
}
