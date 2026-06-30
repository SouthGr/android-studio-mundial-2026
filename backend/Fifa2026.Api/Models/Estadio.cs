namespace Fifa2026.Api.Models;

public class Estadio
{
    public string EstadioId { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Ciudad { get; set; } = "";
    public string Pais { get; set; } = "";
    public string BanderaPaisEmoji { get; set; } = "";
    public int Capacidad { get; set; }
    public string Superficie { get; set; } = "";
    public int Inaugurado { get; set; }
    public string Descripcion { get; set; } = "";
    public string Datos { get; set; } = "";
    public string ImagenEmoji { get; set; } = "🏟️";

    public List<Partido> Partidos { get; set; } = new();
}
