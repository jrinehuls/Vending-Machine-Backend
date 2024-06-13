using VendingMachine.Models.Entities;

namespace VendingMachine.Repositories
{
    public interface ISnackRepository
    {
        Task<Snack?> GetSnackByIdAsync(long id);

        Task<List<Snack>> GetAllSnacksAsync();

        Task SaveChangesAsync();
    }
}
