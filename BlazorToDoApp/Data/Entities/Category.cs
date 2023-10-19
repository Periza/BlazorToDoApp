using DevExpress.Xpo;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations;

namespace BlazorToDoApp.Data.Entities;

public class Category : BaseEntity
{
    public Category(Session session) : base(session) {}

    private string _name;
    public string Name
    {
        get => _name;
        set => SetPropertyValue(nameof(Name), ref _name, value);
    }

    [DevExpress.Xpo.Association("Category-ToDoItems")]
    [Aggregated]
    public XPCollection<ToDoItem> ToDoItems
    {
        get
        {
            return GetCollection<ToDoItem>(nameof(ToDoItems));
        }
    }
}
