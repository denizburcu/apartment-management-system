namespace ApartmentManagement.Core.IUnitOfWorks
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
        void Clear();
    }
}
