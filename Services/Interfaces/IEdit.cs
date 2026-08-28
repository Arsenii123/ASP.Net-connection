using Homework2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework2.Services.Interfaces
{
    public interface IEdit
    {
        public async Task Edit(int id, Movie movie, IFormFile? posterFile)
        {
            Console.WriteLine("Edit");

        }
    }
}
