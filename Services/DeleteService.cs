using Homework2.Models;
using Homework2.Repositories.Interfaces;
using Homework2.Services.Interfaces;

namespace Homework2.Services
{
    public class DeleteService:IDelete
    {
        public Guid Id { get; }=Guid.NewGuid();
        private IRepository _repo;
        public DeleteService(IRepository repo)
        {
            _repo = repo;
        }
        public async Task Delete(int? id)
        {
            await _repo.Delete(id);
        }
    }
}
