using Microsoft.AspNetCore.Mvc;
using CajaAhorro.Application.DTOs;
using CajaAhorro.Application.Interfaces;

namespace CajaAhorro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SociosController : ControllerBase
{
    private readonly ISocioService _socioService;

    public SociosController(ISocioService socioService)
    {
        _socioService = socioService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SocioRespuestaDto>>> ObtenerTodos()
    {
        var socios = await _socioService.ObtenerSociosAsync();
        return Ok(socios);
    }

    [HttpPost]
    public async Task<ActionResult<SocioRespuestaDto>> Registrar([FromBody] CrearSocioDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nombre))
            return BadRequest(new { mensaje = "El nombre del socio es obligatorio." });

        var nuevoSocio = await _socioService.RegistrarSocioAsync(dto);
        return Ok(nuevoSocio);
    }
}