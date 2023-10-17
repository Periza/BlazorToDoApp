using System.ComponentModel.DataAnnotations;

namespace BlazorToDoApp.Data.DTOs;

public class CategoryDTO
{
    [Required]
    [StringLength(30, ErrorMessage = "Name must be between 3 and 30 characters long.", MinimumLength = 3)]
    public string Name { get; set; }
}
