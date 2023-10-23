using DevExpress.Xpo;
using BlazorToDoApp.Data.Entities;
using BlazorToDoApp.Data.Repositories;
using BlazorToDoApp.Data.DTOs;
using System.Runtime.InteropServices;
using System.Reflection;

namespace BlazorToDoApp.Services;

public class ToDoService
{
    private Repository<ToDoItem> _toDoRepository { get; set; }
    private Repository<Category> _categoryRepository { get; set; }
    public ToDoService(Repository<ToDoItem> toDoRepository, Repository<Category> categoryRepository)
    {
        _toDoRepository = toDoRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<ToDoItem>> getAllToDosAsync()
    {
        return (IEnumerable<ToDoItem>)await _toDoRepository.GetAllAsync();
    }

    public async Task<ToDoItem> GetToDoByIdAsync(int id)
    {
        return await _toDoRepository.GetByIdAsync(id);
    }

    public async Task AddNewToDoItemAsync(ToDoItemDTO toDoItemDTO)
    {

        var toDoItem = new ToDoItem(_toDoRepository._unitOfWork);
        {
            toDoItem.Title = toDoItemDTO.Title;
            toDoItem.Complete = toDoItemDTO.Complete;
            toDoItem.Description = toDoItemDTO.Description;
            toDoItem.DateTime = toDoItemDTO.DateTime;
            
            toDoItem.Category = (await _categoryRepository.GetByIdAsync(toDoItemDTO.CategoryId));
        }
        await _toDoRepository.AddAsync(toDoItem);
    }

    public async Task UpdateToDoItemAsync(int toDoId, ToDoItemDTO toDoDTO)
    {
        var toDoItem = await _toDoRepository.GetByIdAsync(toDoId);
        if(toDoItem is not null)
        {
            toDoItem.Title = toDoDTO.Title;
            toDoItem.Complete = toDoDTO.Complete;
            toDoItem.Description = toDoDTO.Description;
            toDoItem.DateTime = toDoDTO.DateTime;
            // Fetch the category
            var category = await _categoryRepository.GetByIdAsync(toDoDTO.CategoryId);

            toDoItem.Category = category;
        } else
        {
            throw new Exception("Can't find a ToDoItem");
        }

        await _toDoRepository.UpdateAsync(toDoItem);
    }

    public async Task DeleteToDoItemAsync(int id)
    {
        await _toDoRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ToDoItem>> GetToDosByCategoryAsync(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);

        if (category is null)
        {
            return Enumerable.Empty<ToDoItem>();
        }

        return category.ToDoItems.ToList();
    }

    public async Task setCheckedAsync(int id, bool state)
    {
        ToDoItem item = await GetToDoByIdAsync(id);
        item.Complete = state;
        await _toDoRepository.UpdateAsync(item);
    }
}
