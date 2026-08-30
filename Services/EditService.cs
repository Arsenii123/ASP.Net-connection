using Homework2.Models;
using Homework2.Repositories.Interfaces;
using Homework2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homework2.Services
{
    public class EditService:IEdit
    {
        public Guid Id { get; }=Guid.NewGuid();
        private readonly IWebHostEnvironment _appEnvironment;

        private IRepository _repo;
        public EditService( IWebHostEnvironment appEnvironmen,IRepository repo)
        {
            Id = Guid.NewGuid();
            _appEnvironment = appEnvironmen;
            _repo = repo;
        }
        public async Task Edit(int id, Movie movie, IFormFile? posterFile)
        {

            var movieInDb = await _repo.Get(id);
            if (movieInDb == null)
                return;   // или throw new Exception($"Movie with id {id} not found");
            movieInDb.Name = movie.Name;
            movieInDb.Director = movie.Director;
            movieInDb.Genre = movie.Genre;
            movieInDb.Description = movie.Description;
            movieInDb.Age = movie.Age;

            // Если загрузили новый файл — меняем постер
            if (posterFile != null && posterFile.Length > 0)
            {

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
            await _repo.Set(id,movieInDb);

        }
    }
}
