using ComputerStore.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.DAL;

public class DataBaseContext : DbContext
{
    public DbSet<Component> Components { get; set; }

    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
    }
}