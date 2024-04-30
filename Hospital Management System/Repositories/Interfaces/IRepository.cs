﻿using Hospital_Management_System.Entities;
using Hospital_Management_System.Entities.Base;
using System.Linq.Expressions;

namespace Hospital_Management_System.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IQueryable<T>> GetAll(Expression<Func<T, bool>>? expression = null);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangeAsync();
    }
}
