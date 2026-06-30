using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fifa2026.Api.Data;
using Fifa2026.Api.Dtos;
using Fifa2026.Api.Models;

namespace Fifa2026.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartidosController : ControllerBase
{
    private readonly Fifa2026DbContext _db;
    public PartidosController(Fifa2026DbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<PartidoDto>>> GetAll(
        [FromQuery] string? grupo, [FromQuery] string? fase,
        [FromQuery] string? estadioId, [FromQuery] string? busqueda)
    {
        var query = _db.Partidos
            .Include(p => p.Estadio)
            .Include(p => p.Local)
            .Include(p => p.Visitante)
            .Include(p => p.Entradas)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(grupo) && grupo != "all")
            query = query.Where(p => p.Grupo == grupo);

        if (!string.IsNullOrWhiteSpace(fase) && fase != "all")
            query = query.Where(p => p.Fase == fase);

        if (!string.IsNullOrWhiteSpace(estadioId) && estadioId != "all")
            query = query.Where(p => p.EstadioId == estadioId);

        if (!string.IsNullOrWhiteSpace(busqueda))
        {
            var q = busqueda.Trim().ToLower();
            query = query.Where(p =>
                (p.Local != null && p.Local.Nombre.ToLower().Contains(q)) ||
                (p.Visitante != null && p.Visitante.Nombre.ToLower().Contains(q)) ||
                (p.Estadio != null && p.Estadio.Nombre.ToLower().Contains(q)));
        }

        var partidos = await query.OrderBy(p => p.Fecha).ThenBy(p => p.Hora).ToListAsync();
        return Ok(partidos.Select(ToDto).ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PartidoDto>> GetById(string id)
    {
        var p = await _db.Partidos
            .Include(x => x.Estadio)
            .Include(x => x.Local)
            .Include(x => x.Visitante)
            .Include(x => x.Entradas)
            .FirstOrDefaultAsync(x => x.PartidoId == id.ToUpper());

        if (p is null) return NotFound();
        return Ok(ToDto(p));
    }

    private static readonly string[] EstadosValidos = { "programado", "en_juego", "finalizado", "por_definir" };
    private static readonly string[] CategoriasValidas = { "general", "preferencial", "vip" };

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<PartidoDto>> Crear([FromBody] PartidoInputDto req)
    {
        var error = await ValidarInput(req, esNuevo: true);
        if (error is not null) return BadRequest(new { mensaje = error });

        var partido = new Partido { PartidoId = req.PartidoId.Trim().ToUpper() };
        AplicarInput(partido, req);

        _db.Partidos.Add(partido);
        await _db.SaveChangesAsync();

        return Ok(await CargarDto(partido.PartidoId));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<PartidoDto>> Actualizar(string id, [FromBody] PartidoInputDto req)
    {
        var partido = await _db.Partidos.Include(p => p.Entradas).FirstOrDefaultAsync(p => p.PartidoId == id.ToUpper());
        if (partido is null) return NotFound(new { mensaje = "Partido no encontrado." });

        var error = await ValidarInput(req, esNuevo: false);
        if (error is not null) return BadRequest(new { mensaje = error });

        AplicarInput(partido, req);
        await _db.SaveChangesAsync();

        return Ok(await CargarDto(partido.PartidoId));
    }

    private async Task<PartidoDto> CargarDto(string partidoId)
    {
        var p = await _db.Partidos
            .Include(x => x.Estadio)
            .Include(x => x.Local)
            .Include(x => x.Visitante)
            .Include(x => x.Entradas)
            .FirstAsync(x => x.PartidoId == partidoId);
        return ToDto(p);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Eliminar(string id)
    {
        var partido = await _db.Partidos.Include(p => p.Reservas).FirstOrDefaultAsync(p => p.PartidoId == id.ToUpper());
        if (partido is null) return NotFound(new { mensaje = "Partido no encontrado." });

        if (partido.Reservas.Count > 0)
            return BadRequest(new { mensaje = $"No se puede eliminar: tiene {partido.Reservas.Count} reserva(s) asociada(s)." });

        _db.Partidos.Remove(partido); // las CategoriasEntrada se borran en cascada
        await _db.SaveChangesAsync();
        return NoContent();
    }

    private async Task<string?> ValidarInput(PartidoInputDto req, bool esNuevo)
    {
        if (esNuevo && await _db.Partidos.AnyAsync(p => p.PartidoId == req.PartidoId.Trim().ToUpper()))
            return $"Ya existe un partido con el id {req.PartidoId}.";

        if (!EstadosValidos.Contains(req.Estado))
            return "Estado inválido. Usá programado, en_juego, finalizado o por_definir.";

        if (!await _db.Estadios.AnyAsync(e => e.EstadioId == req.EstadioId))
            return $"El estadio {req.EstadioId} no existe.";

        if (req.LocalCod is not null && !await _db.Equipos.AnyAsync(e => e.CodigoFifa == req.LocalCod))
            return $"El equipo local {req.LocalCod} no existe.";

        if (req.VisitanteCod is not null && !await _db.Equipos.AnyAsync(e => e.CodigoFifa == req.VisitanteCod))
            return $"El equipo visitante {req.VisitanteCod} no existe.";

        if (!DateOnly.TryParse(req.Fecha, out _))
            return "Fecha inválida. Usá el formato AAAA-MM-DD.";

        foreach (var cat in req.Entradas)
            if (!CategoriasValidas.Contains(cat.Categoria))
                return $"Categoría de entrada inválida: {cat.Categoria}.";

        return null;
    }

    private static void AplicarInput(Partido partido, PartidoInputDto req)
    {
        partido.Fase = req.Fase;
        partido.Grupo = string.IsNullOrWhiteSpace(req.Grupo) ? null : req.Grupo;
        partido.Jornada = req.Jornada;
        partido.Fecha = DateOnly.Parse(req.Fecha);
        partido.Hora = req.Hora;
        partido.EstadioId = req.EstadioId;
        partido.LocalCod = string.IsNullOrWhiteSpace(req.LocalCod) ? null : req.LocalCod;
        partido.VisitanteCod = string.IsNullOrWhiteSpace(req.VisitanteCod) ? null : req.VisitanteCod;
        partido.Estado = req.Estado;
        partido.GolesLocal = req.GolesLocal;
        partido.GolesVisitante = req.GolesVisitante;
        partido.NotaResultado = req.NotaResultado;

        foreach (var catInput in req.Entradas)
        {
            var existente = partido.Entradas.FirstOrDefault(e => e.Categoria == catInput.Categoria);
            if (existente is null)
            {
                partido.Entradas.Add(new CategoriaEntrada
                {
                    PartidoId = partido.PartidoId,
                    Categoria = catInput.Categoria,
                    Precio = catInput.Precio,
                    Total = catInput.Total,
                    Disponibles = catInput.Disponibles,
                });
            }
            else
            {
                existente.Precio = catInput.Precio;
                existente.Total = catInput.Total;
                existente.Disponibles = catInput.Disponibles;
            }
        }
    }

    private static PartidoDto ToDto(Partido p) => new(
        p.PartidoId, p.Fase, p.Grupo, p.Jornada,
        p.Fecha.ToString("yyyy-MM-dd"), p.Hora, p.Estado,
        p.GolesLocal, p.GolesVisitante, p.NotaResultado,
        new EstadioResumenDto(p.Estadio!.EstadioId, p.Estadio.Nombre, p.Estadio.Ciudad, p.Estadio.Pais, p.Estadio.ImagenEmoji),
        p.Local is null ? null : new EquipoDto(p.Local.CodigoFifa, p.Local.Nombre, p.Local.BanderaEmoji, p.Local.Grupo, p.Local.EsAnfitrion),
        p.Visitante is null ? null : new EquipoDto(p.Visitante.CodigoFifa, p.Visitante.Nombre, p.Visitante.BanderaEmoji, p.Visitante.Grupo, p.Visitante.EsAnfitrion),
        p.Entradas.Select(e => new CategoriaEntradaDto(e.Categoria, e.Precio, e.Total, e.Disponibles)).ToList());
}
