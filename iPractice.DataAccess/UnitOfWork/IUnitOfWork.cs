using System;
using System.Threading.Tasks;
using iPractice.DataAccess.Models;
using iPractice.DataAccess.Repositories.Data.Repositories;

namespace iPractice.DataAccess.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IDisposable
        
    {
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;
        public Task<int> Complete();
    }
}