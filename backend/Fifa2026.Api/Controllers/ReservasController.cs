using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fifa2026.Api.Data;
using Fifa2026.Api.Dtos;
using Fifa2026.Api.Models;

namespace Fifa2026.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly Fifa2026DbContext _db;
    public ReservasController(Fifa2026DbContext db) => _db = db;

    private static readonly string[] CategoriasValidas = { "general", "preferencial", "vip" };
    private const string Alfabeto = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ReservaResponseDto>> Crear([FromBody] ReservaRequestDto req)
    {
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var categoria = req.Categoria.ToLower();
        if (!CategoriasValidas.Contains(categoria))
            return BadRequest(new { mensaje = "Categoría inválida. Usá general, preferencial o vip." });

        var partido = await _db.Partidos
            .Include(p => p.Entradas)
            .FirstOrDefaultAsync(p => p.PartidoId == req.PartidoId);

        if (partido is null) return NotFound(new { mensaje = "Partido no encontrado." });
        if (partido.Estado == "finalizado")
            return BadRequest(new { mensaje = "La venta de entradas para este partido ya cerró." });

        var cat = partido.Entradas.FirstOrDefault(e => e.Categoria == categoria);
        if (cat is null) return NotFound(new { mensaje = "Categoría no disponible para este partido." });

        if (req.Cantidad < 1)
            return BadRequest(new { mensaje = "Seleccioná al menos 1 entrada." });

        if (req.Cantidad > cat.Disponibles)
            return BadRequest(new { mensaje = $"Solo quedan {cat.Disponibles} entradas disponibles en esa categoría." });

        // Descuento de stock + alta de reserva en una misma transacción
        await using var tx = await _db.Database.BeginTransactionAsync();

        cat.Disponibles -= req.Cantidad;

        var reserva = new Reserva
        {
            Codigo = GenerarCodigo(),
            PartidoId = partido.PartidoId,
            UsuarioId = usuarioId,
            Categoria = categoria,
            Cantidad = req.Cantidad,
            PrecioUnitario = cat.Precio,
            Total = cat.Precio * req.Cantidad,
            FechaReserva = DateTime.UtcNow,
        };
        _db.Reservas.Add(reserva);

        await _db.SaveChangesAsync();
        await tx.CommitAsync();

        return Ok(new ReservaResponseDto(
            reserva.Codigo, reserva.PartidoId, reserva.Categoria, reserva.Cantidad,
            reserva.PrecioUnitario, reserva.Total, reserva.FechaReserva, cat.Disponibles));
    }

    [HttpGet("{codigo}")]
    public async Task<ActionResult<ReservaResponseDto>> GetByCodigo(string codigo)
    {
        var r = await _db.Reservas.Include(x => x.Partido).ThenInclude(p => p!.Entradas)
            .FirstOrDefaultAsync(x => x.Codigo == codigo.ToUpper());
        if (r is null) return NotFound();

        var disponibles = r.Partido!.Entradas.First(e => e.Categoria == r.Categoria).Disponibles;
        return Ok(new ReservaResponseDto(r.Codigo, r.PartidoId, r.Categoria, r.Cantidad, r.PrecioUnitario, r.Total, r.FechaReserva, disponibles));
    }

    private static string GenerarCodigo()
    {
        var rnd = Random.Shared;
        var chars = new char[8];
        for (var i = 0; i < 8; i++) chars[i] = Alfabeto[rnd.Next(Alfabeto.Length)];
        return "FIFA-" + new string(chars);
    }
}
