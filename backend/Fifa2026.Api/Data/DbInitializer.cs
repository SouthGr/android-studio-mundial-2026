using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Fifa2026.Api.Models;

namespace Fifa2026.Api.Data;

public static class DbInitializer
{
    public const string AdminEmail = "admin@fifa2026.com";
    public const string AdminPasswordInicial = "Admin2026!";

    public static void Seed(Fifa2026DbContext db)
    {
        db.Database.Migrate();

        if (!db.Equipos.Any())
        {
            db.Equipos.AddRange(SeedData.Equipos);
            db.Estadios.AddRange(SeedData.Estadios);
            db.SaveChanges();

            db.Partidos.AddRange(SeedData.Partidos);
            db.SaveChanges();

            db.CategoriasEntrada.AddRange(SeedData.CategoriasEntrada());
            db.SaveChanges();
        }

        if (!db.Usuarios.Any(u => u.Email == AdminEmail))
        {
            var admin = new Usuario
            {
                Nombre = "Administrador FIFA 2026",
                Email = AdminEmail,
                Rol = "Administrador",
                FechaRegistro = DateTime.UtcNow,
            };
            admin.PasswordHash = new PasswordHasher<Usuario>().HashPassword(admin, AdminPasswordInicial);
            db.Usuarios.Add(admin);
            db.SaveChanges();
        }
    }
}
