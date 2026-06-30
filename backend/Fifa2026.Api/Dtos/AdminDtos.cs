using System.ComponentModel.DataAnnotations;

namespace Fifa2026.Api.Dtos;

public class CategoriaEntradaInputDto
{
    [Required]
    public string Categoria { get; set; } = ""; // general | preferencial | vip

    [Range(0, 100000)]
    public decimal Precio { get; set; }

    [Range(0, 200000)]
    public int Total { get; set; }

    [Range(0, 200000)]
    public int Disponibles { get; set; }
}

public class EstadioInputDto
{
    [Required, MaxLength(10)]
    public string EstadioId { get; set; } = "";

    [Required]
    public string Nombre { get; set; } = "";

    [Required]
    public string Ciudad { get; set; } = "";

    [Required]
    public string Pais { get; set; } = "";

    public string BanderaPaisEmoji { get; set; } = "";

    [Range(0, 300000)]
    public int Capacidad { get; set; }

    public string Superficie { get; set; } = "";

    [Range(1800, 2100)]
    public int Inaugurado { get; set; }

    public string Descripcion { get; set; } = "";
    public string Datos { get; set; } = "";
    public string ImagenEmoji { get; set; } = "🏟️";
}

public class PartidoInputDto
{
    [Required, MaxLength(10)]
    public string PartidoId { get; set; } = "";

    [Required]
    public string Fase { get; set; } = "";

    public string? Grupo { get; set; }
    public int? Jornada { get; set; }

    [Required]
    public string Fecha { get; set; } = ""; // yyyy-MM-dd

    [Required]
    public string Hora { get; set; } = "";

    [Required]
    public string EstadioId { get; set; } = "";

    public string? LocalCod { get; set; }
    public string? VisitanteCod { get; set; }

    [Required]
    public string Estado { get; set; } = "programado"; // programado | en_juego | finalizado | por_definir

    public int? GolesLocal { get; set; }
    public int? GolesVisitante { get; set; }
    public string? NotaResultado { get; set; }

    public List<CategoriaEntradaInputDto> Entradas { get; set; } = new();
}
