namespace Homework2.Services.Interfaces
{
    public interface IDelete
    {
        public async Task Delete(int? id)
        {
            Console.WriteLine("Delete");
        }
    }
}
