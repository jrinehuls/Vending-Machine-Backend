using VendingMachine.Models.Entities;

namespace VendingMachine.Repositories
{
    public interface ISnackRepository
    {
        Task<Snack?> GetSnackByIdAsync(long id);

        IQueryable<Snack> GetAllSnacks();

        Task SaveChangesAsync();
    }
}
