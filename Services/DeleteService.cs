using Homework2.Models;
using Homework2.Services.Interfaces;

namespace Homework2.Services
{
    public class DeleteService:IDelete
    {
        public Guid Id { get; }
        private readonly MovieContext _context;
        public DeleteService(MovieContext context)
        {
            _context = context;
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
    }
}
