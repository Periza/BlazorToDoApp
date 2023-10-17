using DevExpress.Xpo;
using System.ComponentModel;

namespace BlazorToDoApp.Data.Entities;

public class ToDoItem : BaseEntity
{
    public ToDoItem(Session session) : base(session) {}

    string _title;
    public string Title
    {
        get => _title;
        set => SetPropertyValue(nameof(Title), ref _title, value);
    }


    public bool _complete;
    public bool Complete
    {
        get => _complete;
        set => SetPropertyValue(nameof(Complete), ref _complete, value);
    }

    Category _category;
    [Association("Category-ToDoItems")]
    public Category Category
    {
        get => _category;
        set => SetPropertyValue(nameof(Category),ref _category, value);
    }
}
