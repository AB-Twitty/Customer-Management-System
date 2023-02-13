using System.Linq.Expressions;
using Contact.BLL.Interfaces;
using Contact.DAL;
using Microsoft.EntityFrameworkCore;

namespace Contact.BLL.Repository_Classes
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ContactDBContext _context;
        public RepositoryBase(ContactDBContext context)
        {
            _context = context;
        }
        public IQueryable<T> FindAll()
        {
            return _context.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T,bool>> exp)
        {
            return _context.Set<T>().Where(exp).AsNoTracking();
        }
        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
