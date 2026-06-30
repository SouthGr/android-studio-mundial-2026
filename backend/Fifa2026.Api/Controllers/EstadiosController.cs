using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fifa2026.Api.Data;
using Fifa2026.Api.Dtos;
using Fifa2026.Api.Models;

namespace Fifa2026.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstadiosController : ControllerBase
{
    private readonly Fifa2026DbContext _db;
    public EstadiosController(Fifa2026DbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<EstadioDto>>> GetAll()
    {
        var estadios = await _db.Estadios
            .OrderBy(e => e.Nombre)
            .Select(e => new EstadioDto(
                e.EstadioId, e.Nombre, e.Ciudad, e.Pais, e.BanderaPaisEmoji,
                e.Capacidad, e.Superficie, e.Inaugurado, e.Descripcion, e.Datos, e.ImagenEmoji,
                e.Partidos.Count))
            .ToListAsync();
        return Ok(estadios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstadioDto>> GetById(string id)
    {
        var e = await _db.Estadios.Include(x => x.Partidos).FirstOrDefaultAsync(x => x.EstadioId == id.ToUpper());
        if (e is null) return NotFound();
        return Ok(new EstadioDto(
            e.EstadioId, e.Nombre, e.Ciudad, e.Pais, e.BanderaPaisEmoji,
            e.Capacidad, e.Superficie, e.Inaugurado, e.Descripcion, e.Datos, e.ImagenEmoji,
            e.Partidos.Count));
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<EstadioDto>> Crear([FromBody] EstadioInputDto req)
    {
        var id = req.EstadioId.Trim().ToUpper();
        if (await _db.Estadios.AnyAsync(e => e.EstadioId == id))
            return BadRequest(new { mensaje = $"Ya existe un estadio con el id {id}." });

        var estadio = new Estadio
        {
            EstadioId = id, Nombre = req.Nombre, Ciudad = req.Ciudad, Pais = req.Pais,
            BanderaPaisEmoji = req.BanderaPaisEmoji, Capacidad = req.Capacidad, Superficie = req.Superficie,
            Inaugurado = req.Inaugurado, Descripcion = req.Descripcion, Datos = req.Datos,
            ImagenEmoji = string.IsNullOrWhiteSpace(req.ImagenEmoji) ? "🏟️" : req.ImagenEmoji,
        };
        _db.Estadios.Add(estadio);
        await _db.SaveChangesAsync();

        return Ok(new EstadioDto(
            estadio.EstadioId, estadio.Nombre, estadio.Ciudad, estadio.Pais, estadio.BanderaPaisEmoji,
            estadio.Capacidad, estadio.Superficie, estadio.Inaugurado, estadio.Descripcion, estadio.Datos, estadio.ImagenEmoji, 0));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<EstadioDto>> Actualizar(string id, [FromBody] EstadioInputDto req)
    {
        var estadio = await _db.Estadios.Include(e => e.Partidos).FirstOrDefaultAsync(e => e.EstadioId == id.ToUpper());
        if (estadio is null) return NotFound(new { mensaje = "Estadio no encontrado." });

        estadio.Nombre = req.Nombre;
        estadio.Ciudad = req.Ciudad;
        estadio.Pais = req.Pais;
        estadio.BanderaPaisEmoji = req.BanderaPaisEmoji;
        estadio.Capacidad = req.Capacidad;
        estadio.Superficie = req.Superficie;
        estadio.Inaugurado = req.Inaugurado;
        estadio.Descripcion = req.Descripcion;
        estadio.Datos = req.Datos;
        estadio.ImagenEmoji = string.IsNullOrWhiteSpace(req.ImagenEmoji) ? "🏟️" : req.ImagenEmoji;

        await _db.SaveChangesAsync();

        return Ok(new EstadioDto(
            estadio.EstadioId, estadio.Nombre, estadio.Ciudad, estadio.Pais, estadio.BanderaPaisEmoji,
            estadio.Capacidad, estadio.Superficie, estadio.Inaugurado, estadio.Descripcion, estadio.Datos, estadio.ImagenEmoji,
            estadio.Partidos.Count));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Eliminar(string id)
    {
        var estadio = await _db.Estadios.Include(e => e.Partidos).FirstOrDefaultAsync(e => e.EstadioId == id.ToUpper());
        if (estadio is null) return NotFound(new { mensaje = "Estadio no encontrado." });

        if (estadio.Partidos.Count > 0)
            return BadRequest(new { mensaje = $"No se puede eliminar: tiene {estadio.Partidos.Count} partido(s) asignado(s)." });

        _db.Estadios.Remove(estadio);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
