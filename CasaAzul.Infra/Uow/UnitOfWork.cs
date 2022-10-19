using CasaAzul.Domain.Uow;
using CasaAzul.Infra.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace CasaAzul.Infra.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EntityContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(EntityContext context)
        {
            _context = context;
        }

        public void BeginCommit()
        {
            _transaction.Commit();
        }

        public void BeginRollback()
        {
            _transaction.Rollback();
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.SaveChanges();   
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();

            if(_transaction != null)
            {
                _transaction?.Dispose();
                _transaction = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}
