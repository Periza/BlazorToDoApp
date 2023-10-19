using BlazorToDoApp.Validators;
using System.ComponentModel.DataAnnotations;

namespace BlazorToDoApp.Data.DTOs;

public class ToDoItemDTO
{
    [Required]
    [StringLength(30, ErrorMessage = "Title must be between 3 and 30 characters long.", MinimumLength = 3)]
    public string Title { get; set; }
    public bool Complete { get; set; }

    [StringLength(500, ErrorMessage = "Description should not be more than 500 characters.")]
    public string Description;

    [Required]
    public DateTime DateTime;

    [Required]
    public int? CategoryId{ get; set; }
}