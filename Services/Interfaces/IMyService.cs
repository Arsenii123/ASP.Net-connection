using Homework2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework2.Services.Interfaces
{
    public interface IMyService
    {
        public async Task ToDo([Bind("Name,Director,Genre,Description,Age")] Movie movie,   // ← добавил Age
        IFormFile? posterFile)
        {

        }

    }
}
