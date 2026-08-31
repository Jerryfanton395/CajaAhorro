namespace CajaAhorro.Application.DTOs;

public class RealizarMovimientoDto
{
    public int AhorroId { get; set; }
    public decimal Monto { get; set; }
    public string Concepto { get; set; } = string.Empty;
}