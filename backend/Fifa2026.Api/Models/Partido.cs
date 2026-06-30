namespace Fifa2026.Api.Models;

public class Partido
{
    public string PartidoId { get; set; } = "";
    public string Fase { get; set; } = "";
    public string? Grupo { get; set; }
    public int? Jornada { get; set; }
    public DateOnly Fecha { get; set; }
    public string Hora { get; set; } = "";

    public string EstadioId { get; set; } = "";
    public Estadio? Estadio { get; set; }

    public string? LocalCod { get; set; }
    public Equipo? Local { get; set; }

    public string? VisitanteCod { get; set; }
    public Equipo? Visitante { get; set; }

    // programado | en_juego | finalizado | por_definir
    public string Estado { get; set; } = "programado";
    public int? GolesLocal { get; set; }
    public int? GolesVisitante { get; set; }

    // Detalle adicional, ej. "Paraguay avanza 4-3 en penales"
    public string? NotaResultado { get; set; }

    public List<CategoriaEntrada> Entradas { get; set; } = new();
    public List<Reserva> Reservas { get; set; } = new();
}
