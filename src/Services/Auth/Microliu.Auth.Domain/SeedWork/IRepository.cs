using System.Linq;

namespace Microliu.Auth.Domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        //IUnitOfWork UnitOfWork { get; }

        T GetEntity(string id);

        IQueryable<T> GetAll();
    }
}
