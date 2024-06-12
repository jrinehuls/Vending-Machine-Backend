using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VendingMachine.Data;
using VendingMachine.Models.Entities;

namespace VendingMachine.Repositories.Impl
{
    public class SnackRepository : ISnackRepository
    {
        private readonly DataContext _context;

        public SnackRepository(DataContext context)
        {
            _context = context;   
        }

        public async Task<Snack?> GetSnackByIdAsync(long id)
        {
            return await _context.Snacks.FirstOrDefaultAsync(s => s.Id == id);
        }

        public IQueryable<Snack> GetAllSnacks()
        {
            return _context.Snacks.AsQueryable();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
