using DevExpress.Xpo;

namespace BlazorToDoApp.Data.Entities;

[NonPersistent]
public class BaseEntity : XPBaseObject
{
    int _id;
    [Key(AutoGenerate = true)]
    public int Id
    {
        get => _id;
        set => SetPropertyValue(nameof(Id), ref _id, value);
    }
    public BaseEntity(Session session) : base(session) { }
}
