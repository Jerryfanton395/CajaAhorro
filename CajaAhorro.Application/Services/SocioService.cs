using CajaAhorro.Application.DTOs;
using CajaAhorro.Application.Interfaces;
using CajaAhorro.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CajaAhorro.Application.Services;

public class SocioService : ISocioService
{
    private readonly AhorroDbContext _context;

    public SocioService(AhorroDbContext context)
    {
        _context = context;
    }

    public async Task<List<SocioRespuestaDto>> ObtenerSociosAsync()
    {
        return await _context.Socios
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
        var socio = new Socio
        {
            Nombre = dto.Nombre,
            Telefono = dto.Telefono,
            FechaRegistro = DateTime.Now
        };

        _context.Socios.Add(socio);
        await _context.SaveChangesAsync();

        return new SocioRespuestaDto
        {
            Id = socio.Id,
            Nombre = socio.Nombre,
            Telefono = socio.Telefono,
            FechaRegistro = socio.FechaRegistro
        };
    }
}