using BlazorToDoApp.Data.Entities;

namespace BlazorToDoApp.Data.Repositories.Contracts;

public interface IGenericRepository<T> where T : BaseEntity
{
    // Create
    Task<T> AddAsync(T entity);

    // Read
    Task<IEnumerable<BaseEntity>> GetAllAsync();
    Task<T> GetByIdAsync(int? id);
    // Update
    Task UpdateAsync(T entity);
    // Delete
    Task DeleteAsync(int id);
}
