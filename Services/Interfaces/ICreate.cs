using Homework2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework2.Services.Interfaces
{
    public interface ICreate
    {
        public async Task Create([Bind("Name,Director,Genre,Description,Age")] Movie movie,   // ← добавил Age
        IFormFile? posterFile)
        {
            Console.WriteLine("Create");

        }

    }
}
