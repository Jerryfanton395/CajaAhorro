namespace CajaAhorro.Domain.Entidades;

public class DetalleAhorro
{
    public int Id { get; set; }

    // Relación con el Ahorro (Módulo 1)
    public int AhorroId { get; set; }
    public Ahorro PlanAhorro { get; set; } = null!;

    // Relación con el Socio (Módulo 2)
    public int SocioId { get; set; }
    public Socio Persona { get; set; } = null!;

    // Asignación de turnos y entregas (Módulos 3 y 4)
    public int NumeroAsignado { get; set; }
    public DateTime FechaCobro { get; set; }
    public decimal MontoAEntregar { get; set; }
}
