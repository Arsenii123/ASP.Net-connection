using Homework2.Models;
using Homework2.Repositories.Interfaces;
using Homework2.Services.Interfaces;

namespace Homework2.Services
{
    public class DetailsService:IDetails
    {
        public Guid Id { get; }=Guid.NewGuid();
        private IRepository _repository;
        public DetailsService(IRepository repository) { 
            _repository = repository;
        }
        public async Task<Movie?> Details(int? id)
        {
            if (id == null) return null;
            return await _repository.Get(id.Value);
        }
    }
}
