using ComputerStore.DAL;
using ComputerStore.DAL.Models;

namespace ComputerStore.BL;

public class DbContext
{
    private readonly DataBaseContext _dataBase;

    public DbContext()
    {
        _dataBase = new DataBaseContextFactory().CreateDbContext(Array.Empty<string>());
    }

    public IEnumerable<Component> GetAllComponents()
    {
        return _dataBase.Components;
    }

    public void DeleteById(int id)
    {
        var component = _dataBase.Components.FirstOrDefault(c => c.Id == id);

        if (component is null) return;
        
        _dataBase.Components.Remove(component);
        _dataBase.SaveChanges();
    }

    public void Create(Component component)
    {
        _dataBase.Components.Add(component);
        _dataBase.SaveChanges();
    }

    public void Update(Component component)
    {
        var temp = _dataBase.Components.FirstOrDefault(c => c.Id == component.Id);
        
        if (temp is null) return;
        
        temp.Name = component.Name;
        temp.Type = component.Type;
        temp.Price = component.Price;
        _dataBase.SaveChanges();
    }
}