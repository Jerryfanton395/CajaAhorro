namespace CajaAhorro.Domain.Entidades;

public class Ahorro
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int TotalNumeros { get; set; }
    public decimal MontoPorNumero { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaPagoParticipantes { get; set; }
    public bool Finalizada { get; set; } = false;

    public List<DetalleAhorro> ListaDetalles { get; set; } = new();
}
