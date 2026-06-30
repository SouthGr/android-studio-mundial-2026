using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fifa2026.Api.Data;
using Fifa2026.Api.Dtos;
using Fifa2026.Api.Models;
using Fifa2026.Api.Services;

namespace Fifa2026.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly Fifa2026DbContext _db;
    private readonly TokenService _tokenService;
    private static readonly PasswordHasher<Usuario> _hasher = new();

    public AuthController(Fifa2026DbContext db, TokenService tokenService)
    {
        _db = db;
        _tokenService = tokenService;
    }

    [HttpPost("registro")]
    public async Task<ActionResult<AuthResponseDto>> Registro([FromBody] RegistroRequestDto req)
    {
        var email = req.Email.Trim().ToLower();

        if (await _db.Usuarios.AnyAsync(u => u.Email == email))
            return BadRequest(new { mensaje = "Ya existe una cuenta registrada con ese email." });

        var usuario = new Usuario
        {
            Nombre = req.Nombre.Trim(),
            Email = email,
            Rol = "Usuario",
            FechaRegistro = DateTime.UtcNow,
        };
        usuario.PasswordHash = _hasher.HashPassword(usuario, req.Password);

        _db.Usuarios.Add(usuario);
        await _db.SaveChangesAsync();

        var (token, expira) = _tokenService.GenerarToken(usuario);
        return Ok(new AuthResponseDto(token, expira, usuario.UsuarioId, usuario.Nombre, usuario.Email, usuario.Rol));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto req)
    {
        var email = req.Email.Trim().ToLower();
        var usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        if (usuario is null)
            return Unauthorized(new { mensaje = "Email o contraseña incorrectos." });

        var resultado = _hasher.VerifyHashedPassword(usuario, usuario.PasswordHash, req.Password);
        if (resultado == PasswordVerificationResult.Failed)
            return Unauthorized(new { mensaje = "Email o contraseña incorrectos." });

        var (token, expira) = _tokenService.GenerarToken(usuario);
        return Ok(new AuthResponseDto(token, expira, usuario.UsuarioId, usuario.Nombre, usuario.Email, usuario.Rol));
    }
}
