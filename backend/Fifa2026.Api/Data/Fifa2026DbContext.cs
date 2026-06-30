using Microsoft.EntityFrameworkCore;
using Fifa2026.Api.Models;

namespace Fifa2026.Api.Data;

public class Fifa2026DbContext : DbContext
{
    public Fifa2026DbContext(DbContextOptions<Fifa2026DbContext> options) : base(options) { }

    public DbSet<Equipo> Equipos => Set<Equipo>();
    public DbSet<Estadio> Estadios => Set<Estadio>();
    public DbSet<Partido> Partidos => Set<Partido>();
    public DbSet<CategoriaEntrada> CategoriasEntrada => Set<CategoriaEntrada>();
    public DbSet<Reserva> Reservas => Set<Reserva>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipo>(e =>
        {
            e.HasKey(x => x.CodigoFifa);
            e.Property(x => x.CodigoFifa).HasMaxLength(3);
            e.Property(x => x.Grupo).HasMaxLength(1);
        });

        modelBuilder.Entity<Estadio>(e =>
        {
            e.HasKey(x => x.EstadioId);
            e.Property(x => x.EstadioId).HasMaxLength(10);
        });

        modelBuilder.Entity<Partido>(e =>
        {
            e.HasKey(x => x.PartidoId);
            e.Property(x => x.PartidoId).HasMaxLength(10);
            e.Property(x => x.Estado).HasMaxLength(20);

            e.HasOne(x => x.Estadio)
                .WithMany(s => s.Partidos)
                .HasForeignKey(x => x.EstadioId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Local)
                .WithMany(eq => eq.PartidosLocal)
                .HasForeignKey(x => x.LocalCod)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Visitante)
                .WithMany(eq => eq.PartidosVisitante)
                .HasForeignKey(x => x.VisitanteCod)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<CategoriaEntrada>(e =>
        {
            e.HasKey(x => new { x.PartidoId, x.Categoria });
            e.Property(x => x.Categoria).HasMaxLength(20);
            e.Property(x => x.Precio).HasColumnType("decimal(10,2)");

            e.HasOne(x => x.Partido)
                .WithMany(p => p.Entradas)
                .HasForeignKey(x => x.PartidoId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Reserva>(e =>
        {
            e.HasKey(x => x.ReservaId);
            e.HasIndex(x => x.Codigo).IsUnique();
            e.Property(x => x.Codigo).HasMaxLength(20);
            e.Property(x => x.Categoria).HasMaxLength(20);
            e.Property(x => x.PrecioUnitario).HasColumnType("decimal(10,2)");
            e.Property(x => x.Total).HasColumnType("decimal(10,2)");

            e.HasOne(x => x.Partido)
                .WithMany(p => p.Reservas)
                .HasForeignKey(x => x.PartidoId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Usuario)
                .WithMany(u => u.Reservas)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Usuario>(e =>
        {
            e.HasKey(x => x.UsuarioId);
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.Email).HasMaxLength(200);
            e.Property(x => x.Nombre).HasMaxLength(120);
            e.Property(x => x.Rol).HasMaxLength(20);
        });
    }
}
