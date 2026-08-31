using Microsoft.EntityFrameworkCore;

namespace CajaAhorro.Domain.Entidades;

public class AhorroDbContext : DbContext
{
    public DbSet<Socio> Socios { get; set; }
    public DbSet<Ahorro> Ahorros { get; set; }
    public DbSet<DetalleAhorro> DetallesAhorros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Define que la base de datos se guardará en un archivo local llamado caja_ahorro.db
        optionsBuilder.UseSqlite("Data Source=caja_ahorro.db");
    }
}