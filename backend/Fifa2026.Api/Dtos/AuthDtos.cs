using System.ComponentModel.DataAnnotations;

namespace Fifa2026.Api.Dtos;

public class RegistroRequestDto
{
    [Required, MinLength(2)]
    public string Nombre { get; set; } = "";

    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required, MinLength(6)]
    public string Password { get; set; } = "";
}

public class LoginRequestDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}

public record AuthResponseDto(string Token, DateTime Expira, int UsuarioId, string Nombre, string Email, string Rol);
