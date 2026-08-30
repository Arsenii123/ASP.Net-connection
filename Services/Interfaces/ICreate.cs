using Homework2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework2.Services.Interfaces
{
    public interface ICreate
    {
        Task Create([Bind("Name,Director,Genre,Description,Age")] Movie movie, IFormFile? posterFile);


    }
}
