namespace Fifa2026.Api.Models;

public class Equipo
{
    public string CodigoFifa { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string BanderaEmoji { get; set; } = "";
    public string? Grupo { get; set; }
    public bool EsAnfitrion { get; set; }

    public List<Partido> PartidosLocal { get; set; } = new();
    public List<Partido> PartidosVisitante { get; set; } = new();
}
