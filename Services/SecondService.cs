using Homework2.Models;
using Homework2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homework2.Services
{
    public class SecondService:IMyService2
    {
        public Guid Id { get; }
        private readonly MovieContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        public SecondService(MovieContext context, IWebHostEnvironment appEnvironmen)
        {
            Id = Guid.NewGuid();
            _context = context;
            _appEnvironment = appEnvironmen;
        }
        public async Task ToDo(int id, Movie movie, IFormFile? posterFile)
        {

            // Загружаем фильм из базы вместе с постером
            var movieInDb = await _context.Movies
                .Include(m => m.Poster)
                .FirstOrDefaultAsync(m => m.Id == id);


            // Если загрузили новый файл — меняем постер
            if (posterFile != null && posterFile.Length > 0)
            {
                movieInDb.Name = movie.Name;
                movieInDb.Director = movie.Director;
                movieInDb.Genre = movie.Genre;
                movieInDb.Description = movie.Description;
                movieInDb.Age = movie.Age;
                var uploadsFolder = Path.Combine(_appEnvironment.WebRootPath, "img");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueName = Guid.NewGuid() + "_" + Path.GetFileName(posterFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await posterFile.CopyToAsync(stream);
                }

                // Создаём новый FileModel
                var newPoster = new FileModel
                {
                    Name = posterFile.FileName,
                    Path = "/img/" + uniqueName,
                    UploadDate = DateTime.Now
                };

                // Можно удалить старый файл с диска (по желанию)
                // if (movieInDb.Poster != null)
                // {
                //     var oldPath = Path.Combine(_appEnvironment.WebRootPath, movieInDb.Poster.Path.TrimStart('/'));
                //     if (System.IO.File.Exists(oldPath))
                //         System.IO.File.Delete(oldPath);
                // }

                movieInDb.Poster = newPoster;
            }

        }
    }
}
