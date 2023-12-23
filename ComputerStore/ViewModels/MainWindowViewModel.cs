using System;
using System.Collections.ObjectModel;
using System.Reactive;
using ComputerStore.BL;
using ComputerStore.DAL.Models;
using ReactiveUI;

namespace ComputerStore.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Component> Components { get; set; }

    private Component? _selectedComponent;
    public Component? SelectedComponent
    {
        get => _selectedComponent;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedComponent, value);

            if (_selectedComponent is null) return;
            
            Id = _selectedComponent.Id;
            Name = _selectedComponent.Name;
            Type = _selectedComponent.Type;
            Price = _selectedComponent.Price;
        }
    }

    private int _id;
    public int Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }
    private string _name;
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    
    private ComponentType _type;
    public ComponentType Type
    {
        get => _type;
        set => this.RaiseAndSetIfChanged(ref _type, value);
    }
    
    private double _price;
    public double Price
    {
        get => _price;
        set => this.RaiseAndSetIfChanged(ref _price, value);
    }

    public ReactiveCommand<Unit, Unit> CommandClear { get; }
    public ReactiveCommand<Unit, Unit> CommandDelete { get; }
    public ReactiveCommand<Unit, Unit> CommandSave { get; }
    
    public MainWindowViewModel()
    {
        var db = new DbContext();
        Components = new ObservableCollection<Component>(db.GetAllComponents());

        CommandClear = ReactiveCommand.Create(() => { SelectedComponent = null; });
        CommandDelete = ReactiveCommand.Create(() =>
        {
            db.DeleteById(SelectedComponent.Id);
            Components.Remove(SelectedComponent);
            SelectedComponent = null;
        });
        CommandSave = ReactiveCommand.Create(() =>
        {
            if (SelectedComponent is null)
            {
                db.Create(new Component
                {
                    Name = Name,
                    Type = ComponentType.Processor,
                    Price = Price
                });
            }
            else
            {
                db.Update(new Component()
                {
                    Id = Id,
                    Name = Name,
                    Type = Type,
                    Price = Price
                });
            }
        });
    }
}