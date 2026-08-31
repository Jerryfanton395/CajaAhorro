using CajaAhorro.Application.DTOs;

namespace CajaAhorro.Application.Interfaces;

public interface ISocioService
{
    Task<List<SocioRespuestaDto>> ObtenerSociosAsync();

    Task<SocioRespuestaDto> RegistrarSocioAsync(CrearSocioDto dto);
}
