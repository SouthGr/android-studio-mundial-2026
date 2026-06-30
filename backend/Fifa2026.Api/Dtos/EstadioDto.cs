namespace Fifa2026.Api.Dtos;

public record EstadioDto(
    string EstadioId, string Nombre, string Ciudad, string Pais, string BanderaPaisEmoji,
    int Capacidad, string Superficie, int Inaugurado, string Descripcion, string Datos, string ImagenEmoji,
    int CantidadPartidos);
