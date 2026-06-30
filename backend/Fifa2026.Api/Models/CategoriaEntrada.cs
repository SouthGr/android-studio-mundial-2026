namespace Fifa2026.Api.Models;

public class CategoriaEntrada
{
    public string PartidoId { get; set; } = "";
    public Partido? Partido { get; set; }

    // general | preferencial | vip
    public string Categoria { get; set; } = "";
    public decimal Precio { get; set; }
    public int Total { get; set; }
    public int Disponibles { get; set; }
}
