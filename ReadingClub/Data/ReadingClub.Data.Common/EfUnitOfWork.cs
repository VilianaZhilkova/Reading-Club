using System;
using System.Data.Entity;

using ReadingClub.Data.Common.Contracts;

namespace ReadingClub.Data.Common
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private DbContext context;

        public EfUnitOfWork(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required", nameof(context));
            }

            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
