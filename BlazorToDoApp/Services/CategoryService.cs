using DevExpress.Xpo;
using BlazorToDoApp.Data.Entities;
using BlazorToDoApp.Data.Repositories;
using BlazorToDoApp.Data.DTOs;

namespace BlazorToDoApp.Services;

public class CategoryService
{
    private Repository<Category> _categoryRepository { get; set; }

    public CategoryService(Repository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> getAllCategories()
    {
        return (IEnumerable<Category>)await _categoryRepository.GetAllAsync();
    }

    public async Task<Category> GetCategoryById(int id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }

    public async Task AddNewCategory(CategoryDTO categoryDTO)
    {
        var category = new Category(_categoryRepository._unitOfWork)
        {
            Name = categoryDTO.Name
        };
        await _categoryRepository.AddAsync(category);
    }

    public async Task UpdateCategory(int categoryId, CategoryDTO categoryDTO)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category is not null)
        {
            category.Name = categoryDTO.Name;
            await _categoryRepository.UpdateAsync(category);
        }
    }

    public async Task DeleteCategory(int id)
    {
        await _categoryRepository.DeleteAsync(id);
    }

    
}
