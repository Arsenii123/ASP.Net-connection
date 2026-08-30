using Homework2.Models;
using Homework2.Repositories.Interfaces;
using Homework2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homework2.Services
{
    public class  CreateService:ICreate
    {
        public Guid Id { get; } = Guid.NewGuid();
        private readonly IWebHostEnvironment _appEnvironment;

        private IRepository _repo;
        public CreateService( IWebHostEnvironment appEnvironmen, IRepository repo)
        {
            Id= Guid.NewGuid();
            _appEnvironment= appEnvironmen;
            _repo = repo;
        }
        public async Task Create([Bind("Name,Director,Genre,Description,Age")] Movie movie,   // ← добавил Age
        IFormFile? posterFile)
        {
            var uploadsFolder = Path.Combine(_appEnvironment.WebRootPath, "img");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(posterFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await posterFile.CopyToAsync(stream);
            }
            // Создаём FileModel
            var fileModel = new FileModel
            {
                Name = posterFile.FileName,
                Path = "/img/" + uniqueName,
                UploadDate = DateTime.Now
            };

            movie.Poster = fileModel;

            await _repo.Create(movie);

          
        }

    }
}
