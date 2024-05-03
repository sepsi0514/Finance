namespace UnitOfWork.Interfaces
{
    public interface IRepository<Type>
    {
        IQueryable<Type> GetAll();
    }
}
