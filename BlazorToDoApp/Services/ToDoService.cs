using DevExpress.Xpo;
using BlazorToDoApp.Data.Entities;
using BlazorToDoApp.Data.Repositories;
using BlazorToDoApp.Data.DTOs;
using System.Runtime.InteropServices;
using System.Reflection;

namespace ToDoApp.Services;

public class ToDoService
{
    private Repository<ToDoItem> _toDoRepository { get; set; }
    private Repository<Category> _categoryRepository { get; set; }

    public ToDoService(Repository<ToDoItem> toDoRepository, Repository<Category> categoryRepository)
    {
        _toDoRepository = toDoRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<ToDoItem>> getAllToDos()
    {
        return (IEnumerable<ToDoItem>)await _toDoRepository.GetAllAsync();
    }

    public async Task<ToDoItem> GetToDoById(int id)
    {
        return await _toDoRepository.GetByIdAsync(id);
    }

    public async Task AddNewToDoItem(ToDoItemDTO toDoItemDTO)
    {

        var toDoItem = new ToDoItem(_toDoRepository._unitOfWork);
        {
            toDoItem.Title = toDoItemDTO.Title;
            toDoItem.Complete = toDoItemDTO.Complete;
            toDoItem.Category = (await _categoryRepository.GetByIdAsync(toDoItemDTO.CategoryId));
        }
        await _toDoRepository.AddAsync(toDoItem);
    }

    public async Task UpdateToDoItem(int toDoId, ToDoItemDTO toDoDTO)
    {
        var toDoItem = await _toDoRepository.GetByIdAsync(toDoId);
        if(toDoItem is not null)
        {
            toDoItem.Title = toDoDTO.Title;
            toDoItem.Complete = toDoDTO.Complete;

            // Fetch the category
            var category = await _categoryRepository.GetByIdAsync(toDoDTO.CategoryId);
            if(category is not null)
            {
                toDoItem.Category = category;
            }
            else
            {
                toDoItem.Category = null;
            }
        }

        await _toDoRepository.UpdateAsync(toDoItem);
    }

    public async Task DeleteToDoItem(int id)
    {
        await _toDoRepository.DeleteAsync(id);
    }
}
