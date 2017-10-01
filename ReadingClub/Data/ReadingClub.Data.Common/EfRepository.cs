using System;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models.Contracts;

namespace ReadingClub.Data.Common
{
    public class EfRepository<T> : IRepository<T>
            where T : class, IAuditable, IDeletable
    {
        public EfRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", nameof(context));
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        private IDbSet<T> DbSet { get; }

        private DbContext Context { get; }

        public IQueryable<T> GetAll
        {
            get
            {
                return this.DbSet.Where(x => !x.IsDeleted);
            }
        }

        public IQueryable<T> GetAllWithDeleted
        {
            get
            {
                return this.DbSet;
            }
        }

        public T GetById(int id)
        {
            var item = this.DbSet.Find(id);
            if (item.IsDeleted)
            {
                return null;
            }

            return item;
        }

        public void Add(T entity)
        {
            this.DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
        }

        public void Update(T entity)
        {
            var entry = this.Context.Entry(entity);

            entry.State = EntityState.Modified;
        }

        public IQueryable<T> Search(Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.Where(predicate);
        }
    }
}
