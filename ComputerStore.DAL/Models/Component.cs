namespace ComputerStore.DAL.Models;

public enum ComponentType
{
    Processor, Motherboard
}

public class Component
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public ComponentType Type { get; set; }
    public double Price { get; set; }
}