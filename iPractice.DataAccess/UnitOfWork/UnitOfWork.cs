using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iPractice.DataAccess.Models;
using iPractice.DataAccess.Repositories;
using iPractice.DataAccess.Repositories.Data.Repositories;

namespace iPractice.DataAccess.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : ApplicationDbContext 
    {
        private bool _disposed;
        private readonly TContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        
        public UnitOfWork()
        {
            Type type = typeof(TContext);
            object result = Activator.CreateInstance(type);
            _context = (TContext)result;
        
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            IRepository<TEntity> repository = new Repository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }


        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Rollback()
        {
            _context
                .ChangeTracker
                .Entries()
                .ToList()
                .ForEach(x => x.Reload());
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}