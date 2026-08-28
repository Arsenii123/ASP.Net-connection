using Homework2.Models;
using Homework2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homework2.Services
{
    public class  CreateService:ICreate
    {
        public Guid Id { get; }
        private readonly MovieContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        public CreateService(MovieContext context, IWebHostEnvironment appEnvironmen)
        {
            Id= Guid.NewGuid();
            _context= context;
            _appEnvironment= appEnvironmen;
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

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
          
        }

    }
}
