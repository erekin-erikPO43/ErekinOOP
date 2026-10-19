using System.Data.Entity;

public class WarehouseContext : DbContext
{
    public WarehouseContext() : base("Data Source=warehouse.db")
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
}