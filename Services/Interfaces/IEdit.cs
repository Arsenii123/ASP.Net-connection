using Homework2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework2.Services.Interfaces
{
    public interface IEdit
    {
        Task Edit(int id, Movie movie, IFormFile? posterFile);

    }
}
