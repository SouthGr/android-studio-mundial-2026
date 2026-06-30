using System.ComponentModel.DataAnnotations;

namespace Fifa2026.Api.Dtos;

public class ReservaRequestDto
{
    [Required]
    public string PartidoId { get; set; } = "";

    [Required]
    public string Categoria { get; set; } = "";

    [Range(1, 10)]
    public int Cantidad { get; set; } = 1;
}

public record ReservaResponseDto(
    string Codigo, string PartidoId, string Categoria, int Cantidad,
    decimal PrecioUnitario, decimal Total, DateTime FechaReserva, int Disponibles);
