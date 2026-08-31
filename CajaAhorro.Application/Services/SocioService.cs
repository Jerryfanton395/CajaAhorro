using CajaAhorro.Domain;
using CajaAhorro.Domain.Entidades;
using CajaAhorro.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CajaAhorro.Application.Services;

public class SocioService
{
    public async Task<List<SocioRespuestaDto>> ObtenerSociosAsync()
    {
        using var context = new AhorroDbContext();
        return await context.Socios
            .Select(s => new SocioRespuestaDto
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Telefono = s.Telefono,
                FechaRegistro = s.FechaRegistro
            })
            .ToListAsync();
    }

    public async Task<SocioRespuestaDto> RegistrarSocioAsync(CrearSocioDto dto)
    {
        using var context = new AhorroDbContext();
        var socio = new Socio
        {
            Nombre = dto.Nombre,
            Telefono = dto.Telefono,
            FechaRegistro = DateTime.Now
        };

        context.Socios.Add(socio);
        await context.SaveChangesAsync();

        return new SocioRespuestaDto
        {
            Id = socio.Id,
            Nombre = socio.Nombre,
            Telefono = socio.Telefono,
            FechaRegistro = socio.FechaRegistro
        };
    }
}