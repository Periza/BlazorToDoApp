using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using System.Reflection;
using BlazorToDoApp.Data.Entities;
using BlazorToDoApp.Data.Repositories.Contracts;

namespace BlazorToDoApp.Data.Repositories;

public class Repository<T> : IGenericRepository<T> where T : BaseEntity
{
    public readonly UnitOfWork _unitOfWork;

    public Repository(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<T> AddAsync(T entity)
    {
        await _unitOfWork.SaveAsync(entity);
        await _unitOfWork.CommitChangesAsync();
        return entity;
    }

    public async Task<T> GetByIdAsync(int? id)
    {
        return await _unitOfWork.FindObjectAsync<T>(CriteriaOperator.Parse("Id=?", id));
    }

    public async Task DeleteAsync(int id)
    {
        T toDelete = await GetByIdAsync(id);

        if(toDelete is not null)
        {
            _unitOfWork.Delete(toDelete);
            await _unitOfWork.CommitChangesAsync();
        } else
        {
            throw new Exception($"Entity with ID {id} not found.");
        }
    }

    public async Task<IEnumerable<BaseEntity>> GetAllAsync()
    {
        XPQuery<T> query = new XPQuery<T>(_unitOfWork);
        return await Task.FromResult(query.ToList());
    }

    public async Task UpdateAsync(T entity)
    {
        var existingEntity = await GetByIdAsync(entity.Id);

        if(existingEntity is not null)
        {
            MapChanges(entity, existingEntity);

            await _unitOfWork.CommitChangesAsync();
        } else
        {
            throw new Exception($"Entity with ID {entity.Id} not found.");
        }
    }

    // Map changes using reflection
    private void MapChanges(T source, T taget)
    {
        Type entityType = source.GetType();

        // Get public properties that belong to an instance of the type
        PropertyInfo[] properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach(PropertyInfo property in properties)
        {
            if(property.CanWrite)
            {
                // Get the value from the source
                object sourceValue = property.GetValue(source);

                property.SetValue(taget, sourceValue);
            }
        }
    }
}
