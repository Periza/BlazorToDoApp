using DevExpress.Xpo;
using System.ComponentModel;

namespace BlazorToDoApp.Data.Entities;

public class ToDoItem : BaseEntity
{
    public ToDoItem(Session session) : base(session) {}

    private string _title;
    public string Title
    {
        get => _title;
        set => SetPropertyValue(nameof(Title), ref _title, value);
    }

    private string _description;
    public string Description
    {
        get => _description;
        set => SetPropertyValue(nameof(Description), ref _description, value);
    }


    private bool _complete;
    public bool Complete
    {
        get => _complete;
        set => SetPropertyValue(nameof(Complete), ref _complete, value);
    }

    private DateTime _date;
    public DateTime DateTime {
        get => _date;
        set => SetPropertyValue(nameof(DateTime), ref _date, value);
    }


    Category _category;
    [Association("Category-ToDoItems")]
    public Category Category
    {
        get => _category;
        set => SetPropertyValue(nameof(Category),ref _category, value);
    }
}
