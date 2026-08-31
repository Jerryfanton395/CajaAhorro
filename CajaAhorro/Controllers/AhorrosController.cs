using CajaAhorro.Application;
using CajaAhorro.Application.DTOs;
using CajaAhorro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CajaAhorro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AhorrosController : ControllerBase
{
    private readonly IAhorroService _ahorroService;

    public AhorrosController(IAhorroService ahorroService)
    {
        _ahorroService = ahorroService;
    }

    [HttpPost("deposito")]
    public async Task<IActionResult> RegistrarDeposito([FromBody] RealizarMovimientoDto dto)
    {
        if (dto.Monto <= 0)
            return BadRequest(new { mensaje = "El monto a depositar debe ser mayor a 0." });

        await _ahorroService.RegistrarDepositoAsync(dto.AhorroId, dto.Monto, dto.Concepto);
        return Ok(new { mensaje = "Depósito realizado correctamente." });
    }

    [HttpPost("retiro")]
    public async Task<IActionResult> RegistrarRetiro([FromBody] RealizarMovimientoDto dto)
    {
        if (dto.Monto <= 0)
            return BadRequest(new { mensaje = "El monto a retirar debe ser mayor a 0." });

        var resultado = await _ahorroService.RegistrarRetiroAsync(dto.AhorroId, dto.Monto, dto.Concepto);
        if (!resultado)
            return BadRequest(new { mensaje = "Saldo insuficiente para realizar el retiro." });

        return Ok(new { mensaje = "Retiro realizado correctamente." });
    }
}