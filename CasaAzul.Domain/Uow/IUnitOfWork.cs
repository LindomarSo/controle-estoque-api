namespace CasaAzul.Domain.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync();
        void BeginTransaction();
        void BeginCommit();
        void BeginRollback();
    }
}
