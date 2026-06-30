using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fifa2026.Api.Data;
using Fifa2026.Api.Dtos;

namespace Fifa2026.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquiposController : ControllerBase
{
    private readonly Fifa2026DbContext _db;
    public EquiposController(Fifa2026DbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<EquipoDto>>> GetAll()
    {
        var equipos = await _db.Equipos
            .OrderBy(e => e.Grupo).ThenBy(e => e.Nombre)
            .Select(e => new EquipoDto(e.CodigoFifa, e.Nombre, e.BanderaEmoji, e.Grupo, e.EsAnfitrion))
            .ToListAsync();
        return Ok(equipos);
    }

    [HttpGet("{codigo}")]
    public async Task<ActionResult<EquipoDto>> GetByCodigo(string codigo)
    {
        var e = await _db.Equipos.FindAsync(codigo.ToUpper());
        if (e is null) return NotFound();
        return Ok(new EquipoDto(e.CodigoFifa, e.Nombre, e.BanderaEmoji, e.Grupo, e.EsAnfitrion));
    }
}
