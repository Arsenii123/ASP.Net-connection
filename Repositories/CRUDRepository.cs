using Homework2.Models;
using Homework2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Homework2.Repositories
{
    public class CRUDRepository:IRepository
    {
        public Guid Id { get; }=Guid.NewGuid();
        private readonly MovieContext _context;

        public CRUDRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task Create(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Movie> Get(int id)
        {
            var movieInDb = await _context.Movies
                .Include(m => m.Poster)
                .FirstOrDefaultAsync(m => m.Id == id);
            return movieInDb;

        }

        public async Task Set(int id,Movie movie)
        {
            if (id != movie.Id) return ;
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return;
            }
        }


    }
}
