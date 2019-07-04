using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BlazorSampleApp.Api.Services
{
    public class DataTable<TEntity> : IQueryable<TEntity> where TEntity : class
    {
        public DataTable(BlazorDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            Data = DbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Entities => Data;

        protected DbContext DbContext { get; }

        public DbSet<TEntity> Data { get; }

        Type IQueryable.ElementType => Entities.ElementType;

        Expression IQueryable.Expression => Entities.Expression;

        IQueryProvider IQueryable.Provider => Entities.Provider;

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Entities.GetEnumerator();
        }
    }
}
