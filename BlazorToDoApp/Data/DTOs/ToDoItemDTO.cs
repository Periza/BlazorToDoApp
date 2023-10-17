using System.ComponentModel.DataAnnotations;

namespace BlazorToDoApp.Data.DTOs;

public class ToDoItemDTO
{
    [Required]
    [StringLength(30, ErrorMessage = "Title must be between 3 and 30 characters long.", MinimumLength = 3)]
    public string Title { get; set; }
    public bool Complete { get; set; }

    [Required]
    public int? CategoryId{ get; set; }
}