using System;
using System.Linq;
using System.Linq.Expressions;

using ReadingClub.Data.Models.Contracts;

namespace ReadingClub.Data.Common.Contracts
{
    public interface IRepository<T> where T : class, IAuditable, IDeletable
    {
        IQueryable<T> GetAll { get; }

        IQueryable<T> GetAllWithDeleted { get; }

        T GetById(int id);

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);

        IQueryable<T> Search(Expression<Func<T, bool>> predicate);
    }
}
