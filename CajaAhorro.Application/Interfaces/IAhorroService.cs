using CajaAhorro.Domain.Entidades;

namespace CajaAhorro.Application.Interfaces;

public interface IAhorroService
{
    Task<int> CrearAhorroAsync(string nombre, int totalNumeros, decimal montoPorNumero, DateTime fechaInicio, DateTime fechaPago);

    // Módulo 3: Asignar un número y fecha a una persona
    Task AsignarNumeroAsync(int ahorroId, int socioId, int numeroAsignado, DateTime fechaCobro);

    // Módulo 4: Obtener la lista completa del ahorro con sus números y socios asignados
    Task<Ahorro?> ObtenerDetalleCompletoAsync(int ahorroId);

    // Módulo 5: Registrar un depósito
    Task RegistrarDepositoAsync(int ahorroId, decimal monto, string concepto);

    // Módulo 6: Registrar un retiro
    Task<bool> RegistrarRetiroAsync(int ahorroId, decimal monto, string concepto);
}
