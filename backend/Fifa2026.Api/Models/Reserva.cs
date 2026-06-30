namespace Fifa2026.Api.Models;

public class Reserva
{
    public int ReservaId { get; set; }
    public string Codigo { get; set; } = "";

    public string PartidoId { get; set; } = "";
    public Partido? Partido { get; set; }

    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }

    public string Categoria { get; set; } = "";
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Total { get; set; }
    public DateTime FechaReserva { get; set; } = DateTime.UtcNow;
}
