namespace Fifa2026.Api.Models;

public class Usuario
{
    public int UsuarioId { get; set; }
    public string Nombre { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";

    // Usuario | Administrador
    public string Rol { get; set; } = "Usuario";
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

    public List<Reserva> Reservas { get; set; } = new();
}
