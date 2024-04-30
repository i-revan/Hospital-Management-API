using Hospital_Management_System.DAL;
using Hospital_Management_System.Entities;
using Hospital_Management_System.Entities.Base;
using Hospital_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hospital_Management_System.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity,new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public IQueryable<T> GetAll(Expression<Func<T,bool>>? expression=null)
        {
            IQueryable<T> query = _table;
            if(expression != null) query = query.Where(expression);
            return query;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T entity = await _table.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
