namespace Fifa2026.Api.Dtos;

public record EstadioResumenDto(string EstadioId, string Nombre, string Ciudad, string Pais, string ImagenEmoji);

public record CategoriaEntradaDto(string Categoria, decimal Precio, int Total, int Disponibles);

public record PartidoDto(
    string PartidoId, string Fase, string? Grupo, int? Jornada,
    string Fecha, string Hora, string Estado,
    int? GolesLocal, int? GolesVisitante, string? NotaResultado,
    EstadioResumenDto Estadio,
    EquipoDto? Local, EquipoDto? Visitante,
    List<CategoriaEntradaDto> Entradas);
