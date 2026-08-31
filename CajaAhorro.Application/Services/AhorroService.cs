using CajaAhorro.Application.Interfaces;
using CajaAhorro.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CajaAhorro.Application.Services;

public class AhorroService : IAhorroService
{
    private readonly AhorroDbContext _context;

    public AhorroService(AhorroDbContext ahorroDbContext)
    {
        _context = ahorroDbContext;
    }

    // Módulo 1: Crear una nueva tanda / plan de ahorro
    public async Task<int> CrearAhorroAsync(string nombre, int totalNumeros, decimal montoPorNumero, DateTime fechaInicio, DateTime fechaPago)
    {
        var nuevoAhorro = new Ahorro
        {
            Nombre = nombre,
            TotalNumeros = totalNumeros,
            MontoPorNumero = montoPorNumero,
            FechaInicio = fechaInicio,
            FechaPagoParticipantes = fechaPago,
            Finalizada = false
        };

        _context.Ahorros.Add(nuevoAhorro);
        await _context.SaveChangesAsync();

        return nuevoAhorro.Id; // Devuelve el ID generado para usarlo en las asignaciones
    }

    // Módulo 3: Asignar un número y fecha a una persona
    public async Task AsignarNumeroAsync(int ahorroId, int socioId, int numeroAsignado, DateTime fechaCobro)
    {

        // Obtenemos el ahorro para saber el monto total que le tocará recibir
        var ahorro = await _context.Ahorros.FindAsync(ahorroId);
        if (ahorro == null) return;

        decimal montoAEntregar = ahorro.TotalNumeros * ahorro.MontoPorNumero;

        var detalle = new DetalleAhorro
        {
            AhorroId = ahorroId,
            SocioId = socioId,
            NumeroAsignado = numeroAsignado,
            FechaCobro = fechaCobro,
            MontoAEntregar = montoAEntregar
        };

        _context.DetallesAhorros.Add(detalle);
        await _context.SaveChangesAsync();
    }

    // Módulo 4: Obtener la lista completa del ahorro con sus números y socios asignados
    public async Task<Ahorro?> ObtenerDetalleCompletoAsync(int ahorroId)
    {

        return await _context.Ahorros
            .Include(a => a.ListaDetalles)
                .ThenInclude(d => d.Persona)
            .FirstOrDefaultAsync(a => a.Id == ahorroId);
    }

    // Módulo 5: Registrar un depósito
    public async Task RegistrarDepositoAsync(int ahorroId, decimal monto, string concepto)
    {
        var movimiento = new DetalleAhorro
        {
            AhorroId = ahorroId,
            MontoAEntregar = monto
        };

        _context.DetallesAhorros.Add(movimiento);
        await _context.SaveChangesAsync();
    }

    // Módulo 6: Registrar un retiro
    public async Task<bool> RegistrarRetiroAsync(int ahorroId, decimal monto, string concepto)
    {
        var totalAcumulado = await _context.DetallesAhorros
            .Where(d => d.AhorroId == ahorroId)
            .SumAsync(d => (decimal?)d.MontoAEntregar) ?? 0;

        if (monto > totalAcumulado)
        {
            return false; // Saldo insuficiente
        }

        var movimiento = new DetalleAhorro
        {
            AhorroId = ahorroId,
            MontoAEntregar = -monto
        };

        _context.DetallesAhorros.Add(movimiento);
        await _context.SaveChangesAsync();
        return true;
    }
}