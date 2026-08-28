using Homework2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework2.Services.Interfaces
{
    public interface IMyService2
    {
        public async Task ToDo(int id, Movie movie, IFormFile? posterFile)
        {

        }
    }
}
