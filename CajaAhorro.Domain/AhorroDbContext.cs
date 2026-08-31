using Microsoft.EntityFrameworkCore;

namespace CajaAhorro.Domain.Entidades;

public class AhorroDbContext : DbContext
{
    #region Constructor
    public AhorroDbContext(DbContextOptions<AhorroDbContext> options) : base(options)
    {

    }
    #endregion

    #region DbSet
    public DbSet<Socio> Socios { get; set; }
    public DbSet<Ahorro> Ahorros { get; set; }
    public DbSet<DetalleAhorro> DetallesAhorros { get; set; }

    #endregion

    #region OnModelCreating
    // Aqui es la base para configurar las entidades y relaciones de la base de datos
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    #endregion
}