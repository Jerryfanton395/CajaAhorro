using CajaAhorro.Domain;
using CajaAhorro.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CajaAhorro.Application.Services;

public class AhorroService
{
    // Módulo 1: Crear una nueva tanda / plan de ahorro
    public async Task<int> CrearAhorroAsync(string nombre, int totalNumeros, decimal montoPorNumero, DateTime fechaInicio, DateTime fechaPago)
    {
        using var context = new AhorroDbContext();

        var nuevoAhorro = new Ahorro
        {
            Nombre = nombre,
            TotalNumeros = totalNumeros,
            MontoPorNumero = montoPorNumero,
            FechaInicio = fechaInicio,
            FechaPagoParticipantes = fechaPago,
            Finalizada = false
        };

        context.Ahorros.Add(nuevoAhorro);
        await context.SaveChangesAsync();

        return nuevoAhorro.Id; // Devuelve el ID generado para usarlo en las asignaciones
    }

    // Módulo 3: Asignar un número y fecha a una persona
    public async Task AsignarNumeroAsync(int ahorroId, int socioId, int numeroAsignado, DateTime fechaCobro)
    {
        using var context = new AhorroDbContext();

        // Obtenemos el ahorro para saber el monto total que le tocará recibir
        var ahorro = await context.Ahorros.FindAsync(ahorroId);
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

        context.DetallesAhorros.Add(detalle);
        await context.SaveChangesAsync();
    }

    // Módulo 4: Obtener la lista completa del ahorro con sus números y socios asignados
    public async Task<Ahorro?> ObtenerDetalleCompletoAsync(int ahorroId)
    {
        using var context = new AhorroDbContext();

        return await context.Ahorros
            .Include(a => a.ListaDetalles)
                .ThenInclude(d => d.Persona)
            .FirstOrDefaultAsync(a => a.Id == ahorroId);
    }

    // Módulo 5: Registrar un depósito
    public async Task RegistrarDepositoAsync(int ahorroId, decimal monto, string concepto)
    {
        using var context = new AhorroDbContext();
        var movimiento = new DetalleAhorro
        {
            AhorroId = ahorroId,
            MontoAEntregar = monto
        };

        context.DetallesAhorros.Add(movimiento);
        await context.SaveChangesAsync();
    }

    // Módulo 6: Registrar un retiro
    public async Task<bool> RegistrarRetiroAsync(int ahorroId, decimal monto, string concepto)
    {
        using var context = new AhorroDbContext();

        var totalAcumulado = await context.DetallesAhorros
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

        context.DetallesAhorros.Add(movimiento);
        await context.SaveChangesAsync();
        return true;
    }
}