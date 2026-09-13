using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using BuisnessLogicPlayer.Services.Interfaces;

namespace BuisnessLogicPlayer.Services
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
