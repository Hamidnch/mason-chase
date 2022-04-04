using Mc2.CrudTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Application.Interfaces;

public interface IMc2Repository<T> where T : BaseEntity
{
    DbSet<T> Get { get; }
    IQueryable<T> Table { get; }

    IReadOnlyList<T> GetAll(bool trackChanges = true);
    Task<IReadOnlyList<T>> GetAllAsync(bool trackChanges);
    Task<T?> GetByIdAsync(int? id);
    Task<IReadOnlyList<T>> GetPagedAsync(int pageNumber, int pageSize);
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}