using Ganz.Domain;
using Ganz.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Ganz.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDBContext _context;
        private bool _disposed = false;

        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .ToList()
                .ForEach(e => e.State = EntityState.Detached);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
