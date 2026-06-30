// ============================================================
// SeedData.cs — Generado a partir de datos reales del Mundial 2026
// Fuente: Wikipedia (2026 FIFA World Cup, páginas por grupo), NBC Sports, CBS Sports
// NO editar a mano — regenerar con scripts/gen-seed.js si los datos cambian.
// ============================================================
using System;
using System.Collections.Generic;
using Fifa2026.Api.Models;

namespace Fifa2026.Api.Data;

public static class SeedData
{
    public static List<Equipo> Equipos => new()
    {
        new Equipo { CodigoFifa = "MEX", Nombre = "México", BanderaEmoji = "🇲🇽", Grupo = "A", EsAnfitrion = true },
        new Equipo { CodigoFifa = "RSA", Nombre = "Sudáfrica", BanderaEmoji = "🇿🇦", Grupo = "A", EsAnfitrion = false },
        new Equipo { CodigoFifa = "KOR", Nombre = "Corea del Sur", BanderaEmoji = "🇰🇷", Grupo = "A", EsAnfitrion = false },
        new Equipo { CodigoFifa = "CZE", Nombre = "Chequia", BanderaEmoji = "🇨🇿", Grupo = "A", EsAnfitrion = false },
        new Equipo { CodigoFifa = "SUI", Nombre = "Suiza", BanderaEmoji = "🇨🇭", Grupo = "B", EsAnfitrion = false },
        new Equipo { CodigoFifa = "CAN", Nombre = "Canadá", BanderaEmoji = "🇨🇦", Grupo = "B", EsAnfitrion = true },
        new Equipo { CodigoFifa = "BIH", Nombre = "Bosnia y Herzegovina", BanderaEmoji = "🇧🇦", Grupo = "B", EsAnfitrion = false },
        new Equipo { CodigoFifa = "QAT", Nombre = "Catar", BanderaEmoji = "🇶🇦", Grupo = "B", EsAnfitrion = false },
        new Equipo { CodigoFifa = "BRA", Nombre = "Brasil", BanderaEmoji = "🇧🇷", Grupo = "C", EsAnfitrion = false },
        new Equipo { CodigoFifa = "MAR", Nombre = "Marruecos", BanderaEmoji = "🇲🇦", Grupo = "C", EsAnfitrion = false },
        new Equipo { CodigoFifa = "SCO", Nombre = "Escocia", BanderaEmoji = "🏴", Grupo = "C", EsAnfitrion = false },
        new Equipo { CodigoFifa = "HAI", Nombre = "Haití", BanderaEmoji = "🇭🇹", Grupo = "C", EsAnfitrion = false },
        new Equipo { CodigoFifa = "USA", Nombre = "Estados Unidos", BanderaEmoji = "🇺🇸", Grupo = "D", EsAnfitrion = true },
        new Equipo { CodigoFifa = "AUS", Nombre = "Australia", BanderaEmoji = "🇦🇺", Grupo = "D", EsAnfitrion = false },
        new Equipo { CodigoFifa = "PAR", Nombre = "Paraguay", BanderaEmoji = "🇵🇾", Grupo = "D", EsAnfitrion = false },
        new Equipo { CodigoFifa = "TUR", Nombre = "Türkiye", BanderaEmoji = "🇹🇷", Grupo = "D", EsAnfitrion = false },
        new Equipo { CodigoFifa = "GER", Nombre = "Alemania", BanderaEmoji = "🇩🇪", Grupo = "E", EsAnfitrion = false },
        new Equipo { CodigoFifa = "CIV", Nombre = "Costa de Marfil", BanderaEmoji = "🇨🇮", Grupo = "E", EsAnfitrion = false },
        new Equipo { CodigoFifa = "ECU", Nombre = "Ecuador", BanderaEmoji = "🇪🇨", Grupo = "E", EsAnfitrion = false },
        new Equipo { CodigoFifa = "CUW", Nombre = "Curazao", BanderaEmoji = "🇨🇼", Grupo = "E", EsAnfitrion = false },
        new Equipo { CodigoFifa = "NED", Nombre = "Países Bajos", BanderaEmoji = "🇳🇱", Grupo = "F", EsAnfitrion = false },
        new Equipo { CodigoFifa = "JAP", Nombre = "Japón", BanderaEmoji = "🇯🇵", Grupo = "F", EsAnfitrion = false },
        new Equipo { CodigoFifa = "SWE", Nombre = "Suecia", BanderaEmoji = "🇸🇪", Grupo = "F", EsAnfitrion = false },
        new Equipo { CodigoFifa = "TUN", Nombre = "Túnez", BanderaEmoji = "🇹🇳", Grupo = "F", EsAnfitrion = false },
        new Equipo { CodigoFifa = "BEL", Nombre = "Bélgica", BanderaEmoji = "🇧🇪", Grupo = "G", EsAnfitrion = false },
        new Equipo { CodigoFifa = "EGY", Nombre = "Egipto", BanderaEmoji = "🇪🇬", Grupo = "G", EsAnfitrion = false },
        new Equipo { CodigoFifa = "IRN", Nombre = "Irán", BanderaEmoji = "🇮🇷", Grupo = "G", EsAnfitrion = false },
        new Equipo { CodigoFifa = "NZL", Nombre = "Nueva Zelanda", BanderaEmoji = "🇳🇿", Grupo = "G", EsAnfitrion = false },
        new Equipo { CodigoFifa = "ESP", Nombre = "España", BanderaEmoji = "🇪🇸", Grupo = "H", EsAnfitrion = false },
        new Equipo { CodigoFifa = "CPV", Nombre = "Cabo Verde", BanderaEmoji = "🇨🇻", Grupo = "H", EsAnfitrion = false },
        new Equipo { CodigoFifa = "URU", Nombre = "Uruguay", BanderaEmoji = "🇺🇾", Grupo = "H", EsAnfitrion = false },
        new Equipo { CodigoFifa = "SAU", Nombre = "Arabia Saudita", BanderaEmoji = "🇸🇦", Grupo = "H", EsAnfitrion = false },
        new Equipo { CodigoFifa = "FRA", Nombre = "Francia", BanderaEmoji = "🇫🇷", Grupo = "I", EsAnfitrion = false },
        new Equipo { CodigoFifa = "NOR", Nombre = "Noruega", BanderaEmoji = "🇳🇴", Grupo = "I", EsAnfitrion = false },
        new Equipo { CodigoFifa = "SEN", Nombre = "Senegal", BanderaEmoji = "🇸🇳", Grupo = "I", EsAnfitrion = false },
        new Equipo { CodigoFifa = "IRQ", Nombre = "Irak", BanderaEmoji = "🇮🇶", Grupo = "I", EsAnfitrion = false },
        new Equipo { CodigoFifa = "ARG", Nombre = "Argentina", BanderaEmoji = "🇦🇷", Grupo = "J", EsAnfitrion = false },
        new Equipo { CodigoFifa = "AUT", Nombre = "Austria", BanderaEmoji = "🇦🇹", Grupo = "J", EsAnfitrion = false },
        new Equipo { CodigoFifa = "ALG", Nombre = "Argelia", BanderaEmoji = "🇩🇿", Grupo = "J", EsAnfitrion = false },
        new Equipo { CodigoFifa = "JOR", Nombre = "Jordania", BanderaEmoji = "🇯🇴", Grupo = "J", EsAnfitrion = false },
        new Equipo { CodigoFifa = "COL", Nombre = "Colombia", BanderaEmoji = "🇨🇴", Grupo = "K", EsAnfitrion = false },
        new Equipo { CodigoFifa = "POR", Nombre = "Portugal", BanderaEmoji = "🇵🇹", Grupo = "K", EsAnfitrion = false },
        new Equipo { CodigoFifa = "COD", Nombre = "R.D. del Congo", BanderaEmoji = "🇨🇩", Grupo = "K", EsAnfitrion = false },
        new Equipo { CodigoFifa = "UZB", Nombre = "Uzbekistán", BanderaEmoji = "🇺🇿", Grupo = "K", EsAnfitrion = false },
        new Equipo { CodigoFifa = "ENG", Nombre = "Inglaterra", BanderaEmoji = "🏴", Grupo = "L", EsAnfitrion = false },
        new Equipo { CodigoFifa = "CRO", Nombre = "Croacia", BanderaEmoji = "🇭🇷", Grupo = "L", EsAnfitrion = false },
        new Equipo { CodigoFifa = "GHA", Nombre = "Ghana", BanderaEmoji = "🇬🇭", Grupo = "L", EsAnfitrion = false },
        new Equipo { CodigoFifa = "PAN", Nombre = "Panamá", BanderaEmoji = "🇵🇦", Grupo = "L", EsAnfitrion = false },
    };

    public static List<Estadio> Estadios => new()
    {
        new Estadio { EstadioId = "EST01", Nombre = "Estadio Azteca", Ciudad = "Ciudad de México", Pais = "México", BanderaPaisEmoji = "🇲🇽", Capacidad = 80824, Superficie = "Césped natural", Inaugurado = 1966, Descripcion = "El estadio más icónico del fútbol latinoamericano, primero en albergar dos finales mundialistas (1970 y 1986). Sede del partido inaugural del Mundial 2026.", Datos = "Escenario de la \"Mano de Dios\" de Maradona y del \"Gol del Siglo\" en 1986.", ImagenEmoji = "🏟️" },
        new Estadio { EstadioId = "EST02", Nombre = "MetLife Stadium", Ciudad = "East Rutherford, Nueva Jersey", Pais = "Estados Unidos", BanderaPaisEmoji = "🇺🇸", Capacidad = 80663, Superficie = "Césped híbrido", Inaugurado = 2010, Descripcion = "El estadio más grande de la Costa Este, sede de la Gran Final del Mundial 2026. Hogar de los Giants y Jets de la NFL.", Datos = "Albergará la final el 19 de julio de 2026.", ImagenEmoji = "🏟️" },
        new Estadio { EstadioId = "EST03", Nombre = "AT&T Stadium", Ciudad = "Arlington, Texas", Pais = "Estados Unidos", BanderaPaisEmoji = "🇺🇸", Capacidad = 70649, Superficie = "Césped sintético", Inaugurado = 2009, Descripcion = "Conocido como \"Jerry World\", uno de los estadios más modernos del planeta, con una pantalla de video gigante suspendida.", Datos = "Construido con una inversión de 1.300 millones de dólares.", ImagenEmoji = "🏟️" },
        new Estadio { EstadioId = "EST04", Nombre = "SoFi Stadium", Ciudad = "Inglewood, California", Pais = "Estados Unidos", BanderaPaisEmoji = "🇺🇸", Capacidad = 70492, Superficie = "Césped natural", Inaugurado = 2020, Descripcion = "El estadio más costoso jamás construido, con una cubierta translúcida icónica. Hogar de los Rams y Chargers de la NFL.", Datos = "Fue sede del Super Bowl LVI en 2022.", ImagenEmoji = "🏟️" },
        new Estadio { EstadioId = "EST05", Nombre = "Arrowhead Stadium", Ciudad = "Kansas City, Misuri", Pais = "Estados Unidos", BanderaPaisEmoji = "🇺🇸", Capacidad = 69045, Superficie = "Césped natural", Inaugurado = 1972, Descripcion = "Tradicional estadio de fútbol americano, célebre por ser uno de los más ruidosos del mundo, adaptado para el Mundial 2026.", Datos = "Hogar de los Kansas City Chiefs de la NFL.", ImagenEmoji = "🏟️" },
        new Estadio { EstadioId = "EST06", Nombre = "Levi's Stadium", Ciudad = "Santa Clara, California", Pais = "Estados Unidos", BanderaPaisEmoji = "🇺🇸", Capacidad = 68827, Superficie = "Césped natural", Inaugurado = 2014, Descripcion = "Estadio de tecnología de punta en el corazón de Silicon Valley, hogar de los San Francisco 49ers.", Datos = "Cuenta con un techo solar y certificación LEED Platino.", ImagenEmoji = "🏟️" },
        new Estadio { EstadioId = "EST07", Nombre = "NRG Stadium", Ciudad = "Houston, Texas", Pais = "Estados Unidos", BanderaPaisEmoji = "🇺🇸", Capacidad = 68777, Superficie = "Césped híbrido", Inaugurado = 2002, Descripcion = "Estadio con techo retráctil, hogar de los Houston Texans, adaptado para recibir al Mundial 2026.", Datos = "Fue sede del Super Bowl LI en 2017.", ImagenEmoji = "🏟️" },
        new Estadio { EstadioId = "EST08", Nombre = "Lincoln Financial Field", Ciudad = "Filadelfia, Pensilvania", Pais = "Estados Unidos", BanderaPaisEmoji = "🇺🇸", Capacidad = 68324, Superficie = "Césped natural", Inaugurado = 2003, Descripcion = "Hogar de los Philadelphia Eagles, reconocido por la pasión de su hinchada.", Datos = "Cuenta con paneles solares y dos turbinas eólicas propias.", ImagenEmoji = "🏟️" },
        new Estadio { EstadioId = "EST09", Nombre = "Mercedes-Benz Stadium", Ciudad = "Atlanta, Georgia", Pais = "Estados Unidos", BanderaPaisEmoji = "🇺🇸", Capacidad = 68239, Superficie = "Césped sintético", Inaugurado = 2017, Descripcion = "Estadio de techo retráctil con un diseño en forma de \"ojo de halcón\", uno de los más modernos de EE. UU.", Datos = "Fue sede del Super Bowl LIII en 2019.", ImagenEmoji = "🏟️" },
        new Estadio { EstadioId = "EST10", Nombre = "Lumen Field", Ciudad = "Seattle, Washington", Pais = "Estados Unidos", BanderaPaisEmoji = "🇺🇸", Capacidad = 66925, Superficie = "Césped sintético", Inaugurado = 2002, Descripcion = "Reconocido por su ambiente ensordecedor, hogar de los Seattle Seahawks y el Seattle Sounders FC.", Datos = "Tiene récord de ruido en estadios de la NFL.", ImagenEmoji = "🏟️" },
        new Estadio { EstadioId = "EST11", Nombre = "Hard Rock Stadium", Ciudad = "Miami Gardens, Florida", Pais = "Estados Unidos", BanderaPaisEmoji = "🇺🇸", Capacidad = 64478, Superficie = "Césped natural", Inaugurado = 1987, Descripcion = "Sede del Tercer Puesto del Mundial 2026, hogar de los Miami Dolphins.", Datos = "Ha albergado seis Super Bowls a lo largo de su historia.", ImagenEmoji = "🏟️" },
        new Estadio { EstadioId = "EST12", Nombre = "Gillette Stadium", Ciudad = "Foxborough, Massachusetts", Pais = "Estados Unidos", BanderaPaisEmoji = "🇺🇸", Capacidad = 64146, Superficie = "Césped sintético", Inaugurado = 2002, Descripcion = "Estadio de los New England Patriots, ubicado entre Boston y Providence.", Datos = "Sede de Cuartos de Final del Mundial 2026.", ImagenEmoji = "🏟️" },
        new Estadio { EstadioId = "EST13", Nombre = "BC Place", Ciudad = "Vancouver, Columbia Británica", Pais = "Canadá", BanderaPaisEmoji = "🇨🇦", Capacidad = 52497, Superficie = "Césped sintético", Inaugurado = 1983, Descripcion = "El único estadio techado del Mundial 2026, con una cubierta de aire comprimido de las más grandes del mundo.", Datos = "Fue sede de la Final de la Copa del Mundo Femenina 2015.", ImagenEmoji = "🏟️" },
        new Estadio { EstadioId = "EST14", Nombre = "Estadio BBVA", Ciudad = "Guadalupe, Nuevo León", Pais = "México", BanderaPaisEmoji = "🇲🇽", Capacidad = 51243, Superficie = "Césped natural", Inaugurado = 2015, Descripcion = "Uno de los estadios más modernos de América Latina, a las faldas del Cerro de la Silla.", Datos = "Sede habitual del club Rayados de Monterrey.", ImagenEmoji = "🏟️" },
        new Estadio { EstadioId = "EST15", Nombre = "Estadio Akron", Ciudad = "Zapopan, Jalisco", Pais = "México", BanderaPaisEmoji = "🇲🇽", Capacidad = 45664, Superficie = "Césped natural", Inaugurado = 2010, Descripcion = "Estadio de diseño vanguardista inspirado en un volcán, hogar de Chivas de Guadalajara.", Datos = "Su fachada está inspirada en el Volcán de Tequila.", ImagenEmoji = "🏟️" },
        new Estadio { EstadioId = "EST16", Nombre = "BMO Field", Ciudad = "Toronto, Ontario", Pais = "Canadá", BanderaPaisEmoji = "🇨🇦", Capacidad = 43036, Superficie = "Césped natural", Inaugurado = 2007, Descripcion = "El estadio de fútbol más pequeño de la sede 2026, ampliado especialmente para el torneo.", Datos = "Hogar habitual del Toronto FC de la MLS.", ImagenEmoji = "🏟️" },
    };

    public static List<Partido> Partidos => new()
    {
        new Partido { PartidoId = "G01", Fase = "Fase de Grupos", Grupo = "A", Jornada = 1, Fecha = DateOnly.Parse("2026-06-11"), Hora = "13:00", EstadioId = "EST01", LocalCod = "MEX", VisitanteCod = "RSA", Estado = "finalizado", GolesLocal = 2, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G02", Fase = "Fase de Grupos", Grupo = "A", Jornada = 1, Fecha = DateOnly.Parse("2026-06-11"), Hora = "20:00", EstadioId = "EST15", LocalCod = "KOR", VisitanteCod = "CZE", Estado = "finalizado", GolesLocal = 2, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G03", Fase = "Fase de Grupos", Grupo = "A", Jornada = 2, Fecha = DateOnly.Parse("2026-06-18"), Hora = "12:00", EstadioId = "EST09", LocalCod = "CZE", VisitanteCod = "RSA", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G04", Fase = "Fase de Grupos", Grupo = "A", Jornada = 2, Fecha = DateOnly.Parse("2026-06-18"), Hora = "19:00", EstadioId = "EST15", LocalCod = "MEX", VisitanteCod = "KOR", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G05", Fase = "Fase de Grupos", Grupo = "A", Jornada = 3, Fecha = DateOnly.Parse("2026-06-24"), Hora = "19:00", EstadioId = "EST01", LocalCod = "CZE", VisitanteCod = "MEX", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 3, NotaResultado = null },
        new Partido { PartidoId = "G06", Fase = "Fase de Grupos", Grupo = "A", Jornada = 3, Fecha = DateOnly.Parse("2026-06-24"), Hora = "19:00", EstadioId = "EST14", LocalCod = "RSA", VisitanteCod = "KOR", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G07", Fase = "Fase de Grupos", Grupo = "B", Jornada = 1, Fecha = DateOnly.Parse("2026-06-12"), Hora = "15:00", EstadioId = "EST16", LocalCod = "CAN", VisitanteCod = "BIH", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G08", Fase = "Fase de Grupos", Grupo = "B", Jornada = 1, Fecha = DateOnly.Parse("2026-06-13"), Hora = "12:00", EstadioId = "EST06", LocalCod = "QAT", VisitanteCod = "SUI", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G09", Fase = "Fase de Grupos", Grupo = "B", Jornada = 2, Fecha = DateOnly.Parse("2026-06-18"), Hora = "12:00", EstadioId = "EST04", LocalCod = "SUI", VisitanteCod = "BIH", Estado = "finalizado", GolesLocal = 4, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G10", Fase = "Fase de Grupos", Grupo = "B", Jornada = 2, Fecha = DateOnly.Parse("2026-06-18"), Hora = "15:00", EstadioId = "EST13", LocalCod = "CAN", VisitanteCod = "QAT", Estado = "finalizado", GolesLocal = 6, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G11", Fase = "Fase de Grupos", Grupo = "B", Jornada = 3, Fecha = DateOnly.Parse("2026-06-24"), Hora = "12:00", EstadioId = "EST13", LocalCod = "SUI", VisitanteCod = "CAN", Estado = "finalizado", GolesLocal = 2, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G12", Fase = "Fase de Grupos", Grupo = "B", Jornada = 3, Fecha = DateOnly.Parse("2026-06-24"), Hora = "12:00", EstadioId = "EST10", LocalCod = "BIH", VisitanteCod = "QAT", Estado = "finalizado", GolesLocal = 3, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G13", Fase = "Fase de Grupos", Grupo = "C", Jornada = 1, Fecha = DateOnly.Parse("2026-06-13"), Hora = "18:00", EstadioId = "EST02", LocalCod = "BRA", VisitanteCod = "MAR", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G14", Fase = "Fase de Grupos", Grupo = "C", Jornada = 1, Fecha = DateOnly.Parse("2026-06-13"), Hora = "21:00", EstadioId = "EST12", LocalCod = "HAI", VisitanteCod = "SCO", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G15", Fase = "Fase de Grupos", Grupo = "C", Jornada = 2, Fecha = DateOnly.Parse("2026-06-19"), Hora = "18:00", EstadioId = "EST12", LocalCod = "SCO", VisitanteCod = "MAR", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G16", Fase = "Fase de Grupos", Grupo = "C", Jornada = 2, Fecha = DateOnly.Parse("2026-06-19"), Hora = "20:30", EstadioId = "EST08", LocalCod = "BRA", VisitanteCod = "HAI", Estado = "finalizado", GolesLocal = 3, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G17", Fase = "Fase de Grupos", Grupo = "C", Jornada = 3, Fecha = DateOnly.Parse("2026-06-24"), Hora = "18:00", EstadioId = "EST11", LocalCod = "SCO", VisitanteCod = "BRA", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 3, NotaResultado = null },
        new Partido { PartidoId = "G18", Fase = "Fase de Grupos", Grupo = "C", Jornada = 3, Fecha = DateOnly.Parse("2026-06-24"), Hora = "18:00", EstadioId = "EST09", LocalCod = "MAR", VisitanteCod = "HAI", Estado = "finalizado", GolesLocal = 4, GolesVisitante = 2, NotaResultado = null },
        new Partido { PartidoId = "G19", Fase = "Fase de Grupos", Grupo = "D", Jornada = 1, Fecha = DateOnly.Parse("2026-06-12"), Hora = "18:00", EstadioId = "EST04", LocalCod = "USA", VisitanteCod = "PAR", Estado = "finalizado", GolesLocal = 4, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G20", Fase = "Fase de Grupos", Grupo = "D", Jornada = 1, Fecha = DateOnly.Parse("2026-06-13"), Hora = "21:00", EstadioId = "EST13", LocalCod = "AUS", VisitanteCod = "TUR", Estado = "finalizado", GolesLocal = 2, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G21", Fase = "Fase de Grupos", Grupo = "D", Jornada = 2, Fecha = DateOnly.Parse("2026-06-19"), Hora = "12:00", EstadioId = "EST10", LocalCod = "USA", VisitanteCod = "AUS", Estado = "finalizado", GolesLocal = 2, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G22", Fase = "Fase de Grupos", Grupo = "D", Jornada = 2, Fecha = DateOnly.Parse("2026-06-19"), Hora = "20:00", EstadioId = "EST06", LocalCod = "TUR", VisitanteCod = "PAR", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G23", Fase = "Fase de Grupos", Grupo = "D", Jornada = 3, Fecha = DateOnly.Parse("2026-06-25"), Hora = "19:00", EstadioId = "EST04", LocalCod = "TUR", VisitanteCod = "USA", Estado = "finalizado", GolesLocal = 3, GolesVisitante = 2, NotaResultado = null },
        new Partido { PartidoId = "G24", Fase = "Fase de Grupos", Grupo = "D", Jornada = 3, Fecha = DateOnly.Parse("2026-06-25"), Hora = "19:00", EstadioId = "EST06", LocalCod = "PAR", VisitanteCod = "AUS", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G25", Fase = "Fase de Grupos", Grupo = "E", Jornada = 1, Fecha = DateOnly.Parse("2026-06-14"), Hora = "12:00", EstadioId = "EST07", LocalCod = "GER", VisitanteCod = "CUW", Estado = "finalizado", GolesLocal = 7, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G26", Fase = "Fase de Grupos", Grupo = "E", Jornada = 1, Fecha = DateOnly.Parse("2026-06-14"), Hora = "19:00", EstadioId = "EST08", LocalCod = "CIV", VisitanteCod = "ECU", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G27", Fase = "Fase de Grupos", Grupo = "E", Jornada = 2, Fecha = DateOnly.Parse("2026-06-20"), Hora = "16:00", EstadioId = "EST16", LocalCod = "GER", VisitanteCod = "CIV", Estado = "finalizado", GolesLocal = 2, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G28", Fase = "Fase de Grupos", Grupo = "E", Jornada = 2, Fecha = DateOnly.Parse("2026-06-20"), Hora = "19:00", EstadioId = "EST05", LocalCod = "ECU", VisitanteCod = "CUW", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G29", Fase = "Fase de Grupos", Grupo = "E", Jornada = 3, Fecha = DateOnly.Parse("2026-06-25"), Hora = "16:00", EstadioId = "EST08", LocalCod = "CUW", VisitanteCod = "CIV", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 2, NotaResultado = null },
        new Partido { PartidoId = "G30", Fase = "Fase de Grupos", Grupo = "E", Jornada = 3, Fecha = DateOnly.Parse("2026-06-25"), Hora = "16:00", EstadioId = "EST02", LocalCod = "ECU", VisitanteCod = "GER", Estado = "finalizado", GolesLocal = 2, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G31", Fase = "Fase de Grupos", Grupo = "F", Jornada = 1, Fecha = DateOnly.Parse("2026-06-14"), Hora = "15:00", EstadioId = "EST03", LocalCod = "NED", VisitanteCod = "JAP", Estado = "finalizado", GolesLocal = 2, GolesVisitante = 2, NotaResultado = null },
        new Partido { PartidoId = "G32", Fase = "Fase de Grupos", Grupo = "F", Jornada = 1, Fecha = DateOnly.Parse("2026-06-14"), Hora = "20:00", EstadioId = "EST14", LocalCod = "SWE", VisitanteCod = "TUN", Estado = "finalizado", GolesLocal = 5, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G33", Fase = "Fase de Grupos", Grupo = "F", Jornada = 2, Fecha = DateOnly.Parse("2026-06-20"), Hora = "12:00", EstadioId = "EST07", LocalCod = "NED", VisitanteCod = "SWE", Estado = "finalizado", GolesLocal = 5, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G34", Fase = "Fase de Grupos", Grupo = "F", Jornada = 2, Fecha = DateOnly.Parse("2026-06-20"), Hora = "22:00", EstadioId = "EST14", LocalCod = "TUN", VisitanteCod = "JAP", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 4, NotaResultado = null },
        new Partido { PartidoId = "G35", Fase = "Fase de Grupos", Grupo = "F", Jornada = 3, Fecha = DateOnly.Parse("2026-06-25"), Hora = "18:00", EstadioId = "EST03", LocalCod = "JAP", VisitanteCod = "SWE", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G36", Fase = "Fase de Grupos", Grupo = "F", Jornada = 3, Fecha = DateOnly.Parse("2026-06-25"), Hora = "18:00", EstadioId = "EST05", LocalCod = "TUN", VisitanteCod = "NED", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 3, NotaResultado = null },
        new Partido { PartidoId = "G37", Fase = "Fase de Grupos", Grupo = "G", Jornada = 1, Fecha = DateOnly.Parse("2026-06-15"), Hora = "12:00", EstadioId = "EST10", LocalCod = "BEL", VisitanteCod = "EGY", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G38", Fase = "Fase de Grupos", Grupo = "G", Jornada = 1, Fecha = DateOnly.Parse("2026-06-15"), Hora = "18:00", EstadioId = "EST04", LocalCod = "IRN", VisitanteCod = "NZL", Estado = "finalizado", GolesLocal = 2, GolesVisitante = 2, NotaResultado = null },
        new Partido { PartidoId = "G39", Fase = "Fase de Grupos", Grupo = "G", Jornada = 2, Fecha = DateOnly.Parse("2026-06-21"), Hora = "12:00", EstadioId = "EST04", LocalCod = "BEL", VisitanteCod = "IRN", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G40", Fase = "Fase de Grupos", Grupo = "G", Jornada = 2, Fecha = DateOnly.Parse("2026-06-21"), Hora = "18:00", EstadioId = "EST13", LocalCod = "NZL", VisitanteCod = "EGY", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 3, NotaResultado = null },
        new Partido { PartidoId = "G41", Fase = "Fase de Grupos", Grupo = "G", Jornada = 3, Fecha = DateOnly.Parse("2026-06-26"), Hora = "20:00", EstadioId = "EST10", LocalCod = "EGY", VisitanteCod = "IRN", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G42", Fase = "Fase de Grupos", Grupo = "G", Jornada = 3, Fecha = DateOnly.Parse("2026-06-26"), Hora = "20:00", EstadioId = "EST13", LocalCod = "NZL", VisitanteCod = "BEL", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 5, NotaResultado = null },
        new Partido { PartidoId = "G43", Fase = "Fase de Grupos", Grupo = "H", Jornada = 1, Fecha = DateOnly.Parse("2026-06-15"), Hora = "12:00", EstadioId = "EST09", LocalCod = "ESP", VisitanteCod = "CPV", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G44", Fase = "Fase de Grupos", Grupo = "H", Jornada = 1, Fecha = DateOnly.Parse("2026-06-15"), Hora = "18:00", EstadioId = "EST11", LocalCod = "SAU", VisitanteCod = "URU", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G45", Fase = "Fase de Grupos", Grupo = "H", Jornada = 2, Fecha = DateOnly.Parse("2026-06-21"), Hora = "12:00", EstadioId = "EST09", LocalCod = "ESP", VisitanteCod = "SAU", Estado = "finalizado", GolesLocal = 4, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G46", Fase = "Fase de Grupos", Grupo = "H", Jornada = 2, Fecha = DateOnly.Parse("2026-06-21"), Hora = "18:00", EstadioId = "EST11", LocalCod = "URU", VisitanteCod = "CPV", Estado = "finalizado", GolesLocal = 2, GolesVisitante = 2, NotaResultado = null },
        new Partido { PartidoId = "G47", Fase = "Fase de Grupos", Grupo = "H", Jornada = 3, Fecha = DateOnly.Parse("2026-06-26"), Hora = "19:00", EstadioId = "EST07", LocalCod = "CPV", VisitanteCod = "SAU", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G48", Fase = "Fase de Grupos", Grupo = "H", Jornada = 3, Fecha = DateOnly.Parse("2026-06-26"), Hora = "18:00", EstadioId = "EST15", LocalCod = "URU", VisitanteCod = "ESP", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G49", Fase = "Fase de Grupos", Grupo = "I", Jornada = 1, Fecha = DateOnly.Parse("2026-06-16"), Hora = "15:00", EstadioId = "EST02", LocalCod = "FRA", VisitanteCod = "SEN", Estado = "finalizado", GolesLocal = 3, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G50", Fase = "Fase de Grupos", Grupo = "I", Jornada = 1, Fecha = DateOnly.Parse("2026-06-16"), Hora = "18:00", EstadioId = "EST12", LocalCod = "IRQ", VisitanteCod = "NOR", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 4, NotaResultado = null },
        new Partido { PartidoId = "G51", Fase = "Fase de Grupos", Grupo = "I", Jornada = 2, Fecha = DateOnly.Parse("2026-06-22"), Hora = "17:00", EstadioId = "EST08", LocalCod = "FRA", VisitanteCod = "IRQ", Estado = "finalizado", GolesLocal = 3, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G52", Fase = "Fase de Grupos", Grupo = "I", Jornada = 2, Fecha = DateOnly.Parse("2026-06-22"), Hora = "20:00", EstadioId = "EST02", LocalCod = "NOR", VisitanteCod = "SEN", Estado = "finalizado", GolesLocal = 3, GolesVisitante = 2, NotaResultado = null },
        new Partido { PartidoId = "G53", Fase = "Fase de Grupos", Grupo = "I", Jornada = 3, Fecha = DateOnly.Parse("2026-06-26"), Hora = "15:00", EstadioId = "EST12", LocalCod = "NOR", VisitanteCod = "FRA", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 4, NotaResultado = null },
        new Partido { PartidoId = "G54", Fase = "Fase de Grupos", Grupo = "I", Jornada = 3, Fecha = DateOnly.Parse("2026-06-26"), Hora = "15:00", EstadioId = "EST16", LocalCod = "SEN", VisitanteCod = "IRQ", Estado = "finalizado", GolesLocal = 5, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G55", Fase = "Fase de Grupos", Grupo = "J", Jornada = 1, Fecha = DateOnly.Parse("2026-06-16"), Hora = "20:00", EstadioId = "EST05", LocalCod = "ARG", VisitanteCod = "ALG", Estado = "finalizado", GolesLocal = 3, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G56", Fase = "Fase de Grupos", Grupo = "J", Jornada = 1, Fecha = DateOnly.Parse("2026-06-16"), Hora = "21:00", EstadioId = "EST06", LocalCod = "AUT", VisitanteCod = "JOR", Estado = "finalizado", GolesLocal = 3, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G57", Fase = "Fase de Grupos", Grupo = "J", Jornada = 2, Fecha = DateOnly.Parse("2026-06-22"), Hora = "12:00", EstadioId = "EST03", LocalCod = "ARG", VisitanteCod = "AUT", Estado = "finalizado", GolesLocal = 2, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G58", Fase = "Fase de Grupos", Grupo = "J", Jornada = 2, Fecha = DateOnly.Parse("2026-06-22"), Hora = "20:00", EstadioId = "EST06", LocalCod = "JOR", VisitanteCod = "ALG", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 2, NotaResultado = null },
        new Partido { PartidoId = "G59", Fase = "Fase de Grupos", Grupo = "J", Jornada = 3, Fecha = DateOnly.Parse("2026-06-27"), Hora = "21:00", EstadioId = "EST05", LocalCod = "ALG", VisitanteCod = "AUT", Estado = "finalizado", GolesLocal = 3, GolesVisitante = 3, NotaResultado = null },
        new Partido { PartidoId = "G60", Fase = "Fase de Grupos", Grupo = "J", Jornada = 3, Fecha = DateOnly.Parse("2026-06-27"), Hora = "21:00", EstadioId = "EST03", LocalCod = "JOR", VisitanteCod = "ARG", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 3, NotaResultado = null },
        new Partido { PartidoId = "G61", Fase = "Fase de Grupos", Grupo = "K", Jornada = 1, Fecha = DateOnly.Parse("2026-06-17"), Hora = "12:00", EstadioId = "EST07", LocalCod = "POR", VisitanteCod = "COD", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G62", Fase = "Fase de Grupos", Grupo = "K", Jornada = 1, Fecha = DateOnly.Parse("2026-06-17"), Hora = "20:00", EstadioId = "EST01", LocalCod = "UZB", VisitanteCod = "COL", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 3, NotaResultado = null },
        new Partido { PartidoId = "G63", Fase = "Fase de Grupos", Grupo = "K", Jornada = 2, Fecha = DateOnly.Parse("2026-06-23"), Hora = "12:00", EstadioId = "EST07", LocalCod = "POR", VisitanteCod = "UZB", Estado = "finalizado", GolesLocal = 5, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G64", Fase = "Fase de Grupos", Grupo = "K", Jornada = 2, Fecha = DateOnly.Parse("2026-06-23"), Hora = "20:00", EstadioId = "EST15", LocalCod = "COL", VisitanteCod = "COD", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G65", Fase = "Fase de Grupos", Grupo = "K", Jornada = 3, Fecha = DateOnly.Parse("2026-06-27"), Hora = "19:30", EstadioId = "EST11", LocalCod = "COL", VisitanteCod = "POR", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G66", Fase = "Fase de Grupos", Grupo = "K", Jornada = 3, Fecha = DateOnly.Parse("2026-06-27"), Hora = "19:30", EstadioId = "EST09", LocalCod = "COD", VisitanteCod = "UZB", Estado = "finalizado", GolesLocal = 3, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G67", Fase = "Fase de Grupos", Grupo = "L", Jornada = 1, Fecha = DateOnly.Parse("2026-06-17"), Hora = "15:00", EstadioId = "EST03", LocalCod = "ENG", VisitanteCod = "CRO", Estado = "finalizado", GolesLocal = 4, GolesVisitante = 2, NotaResultado = null },
        new Partido { PartidoId = "G68", Fase = "Fase de Grupos", Grupo = "L", Jornada = 1, Fecha = DateOnly.Parse("2026-06-17"), Hora = "19:00", EstadioId = "EST16", LocalCod = "GHA", VisitanteCod = "PAN", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G69", Fase = "Fase de Grupos", Grupo = "L", Jornada = 2, Fecha = DateOnly.Parse("2026-06-23"), Hora = "16:00", EstadioId = "EST12", LocalCod = "ENG", VisitanteCod = "GHA", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 0, NotaResultado = null },
        new Partido { PartidoId = "G70", Fase = "Fase de Grupos", Grupo = "L", Jornada = 2, Fecha = DateOnly.Parse("2026-06-23"), Hora = "19:00", EstadioId = "EST16", LocalCod = "PAN", VisitanteCod = "CRO", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "G71", Fase = "Fase de Grupos", Grupo = "L", Jornada = 3, Fecha = DateOnly.Parse("2026-06-27"), Hora = "17:00", EstadioId = "EST02", LocalCod = "PAN", VisitanteCod = "ENG", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 2, NotaResultado = null },
        new Partido { PartidoId = "G72", Fase = "Fase de Grupos", Grupo = "L", Jornada = 3, Fecha = DateOnly.Parse("2026-06-27"), Hora = "17:00", EstadioId = "EST08", LocalCod = "CRO", VisitanteCod = "GHA", Estado = "finalizado", GolesLocal = 2, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "R32-01", Fase = "Dieciseisavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-06-28"), Hora = "15:00", EstadioId = "EST04", LocalCod = "RSA", VisitanteCod = "CAN", Estado = "finalizado", GolesLocal = 0, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "R32-02", Fase = "Dieciseisavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-06-29"), Hora = "12:00", EstadioId = "EST07", LocalCod = "BRA", VisitanteCod = "JAP", Estado = "finalizado", GolesLocal = 2, GolesVisitante = 1, NotaResultado = null },
        new Partido { PartidoId = "R32-03", Fase = "Dieciseisavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-06-29"), Hora = "15:00", EstadioId = "EST12", LocalCod = "GER", VisitanteCod = "PAR", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 1, NotaResultado = "Paraguay avanza 4-3 en penales" },
        new Partido { PartidoId = "R32-04", Fase = "Dieciseisavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-06-29"), Hora = "18:00", EstadioId = "EST14", LocalCod = "NED", VisitanteCod = "MAR", Estado = "finalizado", GolesLocal = 1, GolesVisitante = 1, NotaResultado = "Marruecos avanza 3-2 en penales" },
        new Partido { PartidoId = "R32-05", Fase = "Dieciseisavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-06-30"), Hora = "13:00", EstadioId = "EST03", LocalCod = "CIV", VisitanteCod = "NOR", Estado = "programado", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R32-06", Fase = "Dieciseisavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-06-30"), Hora = "15:00", EstadioId = "EST02", LocalCod = "FRA", VisitanteCod = "SWE", Estado = "programado", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R32-07", Fase = "Dieciseisavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-06-30"), Hora = "19:00", EstadioId = "EST01", LocalCod = "MEX", VisitanteCod = "ECU", Estado = "programado", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R32-08", Fase = "Dieciseisavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-01"), Hora = "12:00", EstadioId = "EST09", LocalCod = "ENG", VisitanteCod = "COD", Estado = "programado", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R32-09", Fase = "Dieciseisavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-01"), Hora = "15:00", EstadioId = "EST10", LocalCod = "BEL", VisitanteCod = "SEN", Estado = "programado", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R32-10", Fase = "Dieciseisavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-01"), Hora = "18:00", EstadioId = "EST06", LocalCod = "USA", VisitanteCod = "BIH", Estado = "programado", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R32-11", Fase = "Dieciseisavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-02"), Hora = "12:00", EstadioId = "EST04", LocalCod = "ESP", VisitanteCod = "AUT", Estado = "programado", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R32-12", Fase = "Dieciseisavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-02"), Hora = "15:00", EstadioId = "EST13", LocalCod = "SUI", VisitanteCod = "ALG", Estado = "programado", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R32-13", Fase = "Dieciseisavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-02"), Hora = "18:00", EstadioId = "EST16", LocalCod = "POR", VisitanteCod = "CRO", Estado = "programado", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R32-14", Fase = "Dieciseisavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-03"), Hora = "12:00", EstadioId = "EST03", LocalCod = "AUS", VisitanteCod = "EGY", Estado = "programado", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R32-15", Fase = "Dieciseisavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-03"), Hora = "15:00", EstadioId = "EST11", LocalCod = "ARG", VisitanteCod = "CPV", Estado = "programado", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R32-16", Fase = "Dieciseisavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-03"), Hora = "18:00", EstadioId = "EST05", LocalCod = "COL", VisitanteCod = "GHA", Estado = "programado", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R16-01", Fase = "Octavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-04"), Hora = "15:00", EstadioId = "EST02", LocalCod = null, VisitanteCod = null, Estado = "por_definir", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R16-02", Fase = "Octavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-04"), Hora = "15:00", EstadioId = "EST08", LocalCod = null, VisitanteCod = null, Estado = "por_definir", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R16-03", Fase = "Octavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-04"), Hora = "15:00", EstadioId = "EST01", LocalCod = null, VisitanteCod = null, Estado = "por_definir", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R16-04", Fase = "Octavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-05"), Hora = "15:00", EstadioId = "EST03", LocalCod = null, VisitanteCod = null, Estado = "por_definir", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R16-05", Fase = "Octavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-05"), Hora = "15:00", EstadioId = "EST10", LocalCod = null, VisitanteCod = null, Estado = "por_definir", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R16-06", Fase = "Octavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-05"), Hora = "15:00", EstadioId = "EST07", LocalCod = null, VisitanteCod = null, Estado = "por_definir", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R16-07", Fase = "Octavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-06"), Hora = "15:00", EstadioId = "EST09", LocalCod = null, VisitanteCod = null, Estado = "por_definir", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "R16-08", Fase = "Octavos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-07"), Hora = "15:00", EstadioId = "EST13", LocalCod = null, VisitanteCod = null, Estado = "por_definir", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "QF-01", Fase = "Cuartos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-09"), Hora = "15:00", EstadioId = "EST12", LocalCod = null, VisitanteCod = null, Estado = "por_definir", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "QF-02", Fase = "Cuartos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-10"), Hora = "15:00", EstadioId = "EST04", LocalCod = null, VisitanteCod = null, Estado = "por_definir", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "QF-03", Fase = "Cuartos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-11"), Hora = "15:00", EstadioId = "EST05", LocalCod = null, VisitanteCod = null, Estado = "por_definir", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "QF-04", Fase = "Cuartos de Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-11"), Hora = "15:00", EstadioId = "EST11", LocalCod = null, VisitanteCod = null, Estado = "por_definir", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "SF-01", Fase = "Semifinal", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-14"), Hora = "15:00", EstadioId = "EST03", LocalCod = null, VisitanteCod = null, Estado = "por_definir", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "SF-02", Fase = "Semifinal", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-15"), Hora = "15:00", EstadioId = "EST09", LocalCod = null, VisitanteCod = null, Estado = "por_definir", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "TP-01", Fase = "Tercer Puesto", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-18"), Hora = "13:00", EstadioId = "EST11", LocalCod = null, VisitanteCod = null, Estado = "por_definir", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
        new Partido { PartidoId = "FIN-01", Fase = "Gran Final", Grupo = null, Jornada = null, Fecha = DateOnly.Parse("2026-07-19"), Hora = "15:00", EstadioId = "EST02", LocalCod = null, VisitanteCod = null, Estado = "por_definir", GolesLocal = null, GolesVisitante = null, NotaResultado = null },
    };

    public static List<CategoriaEntrada> CategoriasEntrada()
    {
        var list = new List<CategoriaEntrada>();
        list.Add(new CategoriaEntrada { PartidoId = "G01", Categoria = "general", Precio = 95m, Total = 58193, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G01", Categoria = "preferencial", Precio = 200m, Total = 17781, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G01", Categoria = "vip", Precio = 550m, Total = 4849, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G02", Categoria = "general", Precio = 95m, Total = 32878, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G02", Categoria = "preferencial", Precio = 200m, Total = 10046, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G02", Categoria = "vip", Precio = 550m, Total = 2740, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G03", Categoria = "general", Precio = 95m, Total = 49132, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G03", Categoria = "preferencial", Precio = 200m, Total = 15013, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G03", Categoria = "vip", Precio = 550m, Total = 4094, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G04", Categoria = "general", Precio = 95m, Total = 32878, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G04", Categoria = "preferencial", Precio = 200m, Total = 10046, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G04", Categoria = "vip", Precio = 550m, Total = 2740, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G05", Categoria = "general", Precio = 95m, Total = 58193, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G05", Categoria = "preferencial", Precio = 200m, Total = 17781, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G05", Categoria = "vip", Precio = 550m, Total = 4849, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G06", Categoria = "general", Precio = 95m, Total = 36895, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G06", Categoria = "preferencial", Precio = 200m, Total = 11273, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G06", Categoria = "vip", Precio = 550m, Total = 3075, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G07", Categoria = "general", Precio = 95m, Total = 30986, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G07", Categoria = "preferencial", Precio = 200m, Total = 9468, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G07", Categoria = "vip", Precio = 550m, Total = 2582, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G08", Categoria = "general", Precio = 95m, Total = 49555, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G08", Categoria = "preferencial", Precio = 200m, Total = 15142, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G08", Categoria = "vip", Precio = 550m, Total = 4130, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G09", Categoria = "general", Precio = 95m, Total = 50754, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G09", Categoria = "preferencial", Precio = 200m, Total = 15508, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G09", Categoria = "vip", Precio = 550m, Total = 4230, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G10", Categoria = "general", Precio = 95m, Total = 37798, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G10", Categoria = "preferencial", Precio = 200m, Total = 11549, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G10", Categoria = "vip", Precio = 550m, Total = 3150, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G11", Categoria = "general", Precio = 95m, Total = 37798, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G11", Categoria = "preferencial", Precio = 200m, Total = 11549, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G11", Categoria = "vip", Precio = 550m, Total = 3150, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G12", Categoria = "general", Precio = 95m, Total = 48186, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G12", Categoria = "preferencial", Precio = 200m, Total = 14724, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G12", Categoria = "vip", Precio = 550m, Total = 4016, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G13", Categoria = "general", Precio = 95m, Total = 58077, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G13", Categoria = "preferencial", Precio = 200m, Total = 17746, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G13", Categoria = "vip", Precio = 550m, Total = 4840, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G14", Categoria = "general", Precio = 95m, Total = 46185, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G14", Categoria = "preferencial", Precio = 200m, Total = 14112, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G14", Categoria = "vip", Precio = 550m, Total = 3849, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G15", Categoria = "general", Precio = 95m, Total = 46185, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G15", Categoria = "preferencial", Precio = 200m, Total = 14112, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G15", Categoria = "vip", Precio = 550m, Total = 3849, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G16", Categoria = "general", Precio = 95m, Total = 49193, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G16", Categoria = "preferencial", Precio = 200m, Total = 15031, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G16", Categoria = "vip", Precio = 550m, Total = 4099, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G17", Categoria = "general", Precio = 95m, Total = 46424, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G17", Categoria = "preferencial", Precio = 200m, Total = 14185, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G17", Categoria = "vip", Precio = 550m, Total = 3869, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G18", Categoria = "general", Precio = 95m, Total = 49132, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G18", Categoria = "preferencial", Precio = 200m, Total = 15013, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G18", Categoria = "vip", Precio = 550m, Total = 4094, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G19", Categoria = "general", Precio = 95m, Total = 50754, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G19", Categoria = "preferencial", Precio = 200m, Total = 15508, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G19", Categoria = "vip", Precio = 550m, Total = 4230, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G20", Categoria = "general", Precio = 95m, Total = 37798, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G20", Categoria = "preferencial", Precio = 200m, Total = 11549, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G20", Categoria = "vip", Precio = 550m, Total = 3150, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G21", Categoria = "general", Precio = 95m, Total = 48186, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G21", Categoria = "preferencial", Precio = 200m, Total = 14724, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G21", Categoria = "vip", Precio = 550m, Total = 4016, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G22", Categoria = "general", Precio = 95m, Total = 49555, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G22", Categoria = "preferencial", Precio = 200m, Total = 15142, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G22", Categoria = "vip", Precio = 550m, Total = 4130, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G23", Categoria = "general", Precio = 95m, Total = 50754, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G23", Categoria = "preferencial", Precio = 200m, Total = 15508, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G23", Categoria = "vip", Precio = 550m, Total = 4230, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G24", Categoria = "general", Precio = 95m, Total = 49555, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G24", Categoria = "preferencial", Precio = 200m, Total = 15142, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G24", Categoria = "vip", Precio = 550m, Total = 4130, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G25", Categoria = "general", Precio = 95m, Total = 49519, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G25", Categoria = "preferencial", Precio = 200m, Total = 15131, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G25", Categoria = "vip", Precio = 550m, Total = 4127, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G26", Categoria = "general", Precio = 95m, Total = 49193, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G26", Categoria = "preferencial", Precio = 200m, Total = 15031, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G26", Categoria = "vip", Precio = 550m, Total = 4099, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G27", Categoria = "general", Precio = 95m, Total = 30986, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G27", Categoria = "preferencial", Precio = 200m, Total = 9468, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G27", Categoria = "vip", Precio = 550m, Total = 2582, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G28", Categoria = "general", Precio = 95m, Total = 49712, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G28", Categoria = "preferencial", Precio = 200m, Total = 15190, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G28", Categoria = "vip", Precio = 550m, Total = 4143, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G29", Categoria = "general", Precio = 95m, Total = 49193, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G29", Categoria = "preferencial", Precio = 200m, Total = 15031, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G29", Categoria = "vip", Precio = 550m, Total = 4099, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G30", Categoria = "general", Precio = 95m, Total = 58077, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G30", Categoria = "preferencial", Precio = 200m, Total = 17746, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G30", Categoria = "vip", Precio = 550m, Total = 4840, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G31", Categoria = "general", Precio = 95m, Total = 50867, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G31", Categoria = "preferencial", Precio = 200m, Total = 15543, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G31", Categoria = "vip", Precio = 550m, Total = 4239, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G32", Categoria = "general", Precio = 95m, Total = 36895, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G32", Categoria = "preferencial", Precio = 200m, Total = 11273, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G32", Categoria = "vip", Precio = 550m, Total = 3075, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G33", Categoria = "general", Precio = 95m, Total = 49519, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G33", Categoria = "preferencial", Precio = 200m, Total = 15131, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G33", Categoria = "vip", Precio = 550m, Total = 4127, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G34", Categoria = "general", Precio = 95m, Total = 36895, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G34", Categoria = "preferencial", Precio = 200m, Total = 11273, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G34", Categoria = "vip", Precio = 550m, Total = 3075, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G35", Categoria = "general", Precio = 95m, Total = 50867, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G35", Categoria = "preferencial", Precio = 200m, Total = 15543, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G35", Categoria = "vip", Precio = 550m, Total = 4239, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G36", Categoria = "general", Precio = 95m, Total = 49712, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G36", Categoria = "preferencial", Precio = 200m, Total = 15190, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G36", Categoria = "vip", Precio = 550m, Total = 4143, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G37", Categoria = "general", Precio = 95m, Total = 48186, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G37", Categoria = "preferencial", Precio = 200m, Total = 14724, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G37", Categoria = "vip", Precio = 550m, Total = 4016, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G38", Categoria = "general", Precio = 95m, Total = 50754, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G38", Categoria = "preferencial", Precio = 200m, Total = 15508, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G38", Categoria = "vip", Precio = 550m, Total = 4230, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G39", Categoria = "general", Precio = 95m, Total = 50754, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G39", Categoria = "preferencial", Precio = 200m, Total = 15508, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G39", Categoria = "vip", Precio = 550m, Total = 4230, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G40", Categoria = "general", Precio = 95m, Total = 37798, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G40", Categoria = "preferencial", Precio = 200m, Total = 11549, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G40", Categoria = "vip", Precio = 550m, Total = 3150, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G41", Categoria = "general", Precio = 95m, Total = 48186, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G41", Categoria = "preferencial", Precio = 200m, Total = 14724, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G41", Categoria = "vip", Precio = 550m, Total = 4016, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G42", Categoria = "general", Precio = 95m, Total = 37798, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G42", Categoria = "preferencial", Precio = 200m, Total = 11549, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G42", Categoria = "vip", Precio = 550m, Total = 3150, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G43", Categoria = "general", Precio = 95m, Total = 49132, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G43", Categoria = "preferencial", Precio = 200m, Total = 15013, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G43", Categoria = "vip", Precio = 550m, Total = 4094, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G44", Categoria = "general", Precio = 95m, Total = 46424, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G44", Categoria = "preferencial", Precio = 200m, Total = 14185, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G44", Categoria = "vip", Precio = 550m, Total = 3869, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G45", Categoria = "general", Precio = 95m, Total = 49132, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G45", Categoria = "preferencial", Precio = 200m, Total = 15013, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G45", Categoria = "vip", Precio = 550m, Total = 4094, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G46", Categoria = "general", Precio = 95m, Total = 46424, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G46", Categoria = "preferencial", Precio = 200m, Total = 14185, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G46", Categoria = "vip", Precio = 550m, Total = 3869, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G47", Categoria = "general", Precio = 95m, Total = 49519, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G47", Categoria = "preferencial", Precio = 200m, Total = 15131, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G47", Categoria = "vip", Precio = 550m, Total = 4127, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G48", Categoria = "general", Precio = 95m, Total = 32878, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G48", Categoria = "preferencial", Precio = 200m, Total = 10046, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G48", Categoria = "vip", Precio = 550m, Total = 2740, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G49", Categoria = "general", Precio = 95m, Total = 58077, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G49", Categoria = "preferencial", Precio = 200m, Total = 17746, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G49", Categoria = "vip", Precio = 550m, Total = 4840, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G50", Categoria = "general", Precio = 95m, Total = 46185, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G50", Categoria = "preferencial", Precio = 200m, Total = 14112, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G50", Categoria = "vip", Precio = 550m, Total = 3849, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G51", Categoria = "general", Precio = 95m, Total = 49193, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G51", Categoria = "preferencial", Precio = 200m, Total = 15031, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G51", Categoria = "vip", Precio = 550m, Total = 4099, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G52", Categoria = "general", Precio = 95m, Total = 58077, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G52", Categoria = "preferencial", Precio = 200m, Total = 17746, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G52", Categoria = "vip", Precio = 550m, Total = 4840, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G53", Categoria = "general", Precio = 95m, Total = 46185, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G53", Categoria = "preferencial", Precio = 200m, Total = 14112, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G53", Categoria = "vip", Precio = 550m, Total = 3849, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G54", Categoria = "general", Precio = 95m, Total = 30986, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G54", Categoria = "preferencial", Precio = 200m, Total = 9468, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G54", Categoria = "vip", Precio = 550m, Total = 2582, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G55", Categoria = "general", Precio = 95m, Total = 49712, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G55", Categoria = "preferencial", Precio = 200m, Total = 15190, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G55", Categoria = "vip", Precio = 550m, Total = 4143, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G56", Categoria = "general", Precio = 95m, Total = 49555, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G56", Categoria = "preferencial", Precio = 200m, Total = 15142, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G56", Categoria = "vip", Precio = 550m, Total = 4130, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G57", Categoria = "general", Precio = 95m, Total = 50867, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G57", Categoria = "preferencial", Precio = 200m, Total = 15543, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G57", Categoria = "vip", Precio = 550m, Total = 4239, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G58", Categoria = "general", Precio = 95m, Total = 49555, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G58", Categoria = "preferencial", Precio = 200m, Total = 15142, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G58", Categoria = "vip", Precio = 550m, Total = 4130, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G59", Categoria = "general", Precio = 95m, Total = 49712, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G59", Categoria = "preferencial", Precio = 200m, Total = 15190, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G59", Categoria = "vip", Precio = 550m, Total = 4143, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G60", Categoria = "general", Precio = 95m, Total = 50867, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G60", Categoria = "preferencial", Precio = 200m, Total = 15543, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G60", Categoria = "vip", Precio = 550m, Total = 4239, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G61", Categoria = "general", Precio = 95m, Total = 49519, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G61", Categoria = "preferencial", Precio = 200m, Total = 15131, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G61", Categoria = "vip", Precio = 550m, Total = 4127, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G62", Categoria = "general", Precio = 95m, Total = 58193, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G62", Categoria = "preferencial", Precio = 200m, Total = 17781, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G62", Categoria = "vip", Precio = 550m, Total = 4849, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G63", Categoria = "general", Precio = 95m, Total = 49519, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G63", Categoria = "preferencial", Precio = 200m, Total = 15131, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G63", Categoria = "vip", Precio = 550m, Total = 4127, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G64", Categoria = "general", Precio = 95m, Total = 32878, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G64", Categoria = "preferencial", Precio = 200m, Total = 10046, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G64", Categoria = "vip", Precio = 550m, Total = 2740, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G65", Categoria = "general", Precio = 95m, Total = 46424, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G65", Categoria = "preferencial", Precio = 200m, Total = 14185, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G65", Categoria = "vip", Precio = 550m, Total = 3869, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G66", Categoria = "general", Precio = 95m, Total = 49132, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G66", Categoria = "preferencial", Precio = 200m, Total = 15013, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G66", Categoria = "vip", Precio = 550m, Total = 4094, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G67", Categoria = "general", Precio = 95m, Total = 50867, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G67", Categoria = "preferencial", Precio = 200m, Total = 15543, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G67", Categoria = "vip", Precio = 550m, Total = 4239, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G68", Categoria = "general", Precio = 95m, Total = 30986, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G68", Categoria = "preferencial", Precio = 200m, Total = 9468, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G68", Categoria = "vip", Precio = 550m, Total = 2582, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G69", Categoria = "general", Precio = 95m, Total = 46185, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G69", Categoria = "preferencial", Precio = 200m, Total = 14112, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G69", Categoria = "vip", Precio = 550m, Total = 3849, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G70", Categoria = "general", Precio = 95m, Total = 30986, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G70", Categoria = "preferencial", Precio = 200m, Total = 9468, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G70", Categoria = "vip", Precio = 550m, Total = 2582, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G71", Categoria = "general", Precio = 95m, Total = 58077, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G71", Categoria = "preferencial", Precio = 200m, Total = 17746, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G71", Categoria = "vip", Precio = 550m, Total = 4840, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G72", Categoria = "general", Precio = 95m, Total = 49193, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G72", Categoria = "preferencial", Precio = 200m, Total = 15031, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "G72", Categoria = "vip", Precio = 550m, Total = 4099, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-01", Categoria = "general", Precio = 130m, Total = 50754, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-01", Categoria = "preferencial", Precio = 260m, Total = 15508, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-01", Categoria = "vip", Precio = 650m, Total = 4230, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-02", Categoria = "general", Precio = 130m, Total = 49519, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-02", Categoria = "preferencial", Precio = 260m, Total = 15131, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-02", Categoria = "vip", Precio = 650m, Total = 4127, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-03", Categoria = "general", Precio = 130m, Total = 46185, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-03", Categoria = "preferencial", Precio = 260m, Total = 14112, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-03", Categoria = "vip", Precio = 650m, Total = 3849, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-04", Categoria = "general", Precio = 130m, Total = 36895, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-04", Categoria = "preferencial", Precio = 260m, Total = 11273, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-04", Categoria = "vip", Precio = 650m, Total = 3075, Disponibles = 0 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-05", Categoria = "general", Precio = 130m, Total = 50867, Disponibles = 7630 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-05", Categoria = "preferencial", Precio = 260m, Total = 15543, Disponibles = 2331 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-05", Categoria = "vip", Precio = 650m, Total = 4239, Disponibles = 350 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-06", Categoria = "general", Precio = 130m, Total = 58077, Disponibles = 8712 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-06", Categoria = "preferencial", Precio = 260m, Total = 17746, Disponibles = 2662 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-06", Categoria = "vip", Precio = 650m, Total = 4840, Disponibles = 399 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-07", Categoria = "general", Precio = 130m, Total = 58193, Disponibles = 8729 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-07", Categoria = "preferencial", Precio = 260m, Total = 17781, Disponibles = 2667 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-07", Categoria = "vip", Precio = 650m, Total = 4849, Disponibles = 400 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-08", Categoria = "general", Precio = 130m, Total = 49132, Disponibles = 8598 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-08", Categoria = "preferencial", Precio = 260m, Total = 15013, Disponibles = 2627 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-08", Categoria = "vip", Precio = 650m, Total = 4094, Disponibles = 394 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-09", Categoria = "general", Precio = 130m, Total = 48186, Disponibles = 8433 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-09", Categoria = "preferencial", Precio = 260m, Total = 14724, Disponibles = 2577 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-09", Categoria = "vip", Precio = 650m, Total = 4016, Disponibles = 387 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-10", Categoria = "general", Precio = 130m, Total = 49555, Disponibles = 8672 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-10", Categoria = "preferencial", Precio = 260m, Total = 15142, Disponibles = 2650 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-10", Categoria = "vip", Precio = 650m, Total = 4130, Disponibles = 398 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-11", Categoria = "general", Precio = 130m, Total = 50754, Disponibles = 10151 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-11", Categoria = "preferencial", Precio = 260m, Total = 15508, Disponibles = 3102 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-11", Categoria = "vip", Precio = 650m, Total = 4230, Disponibles = 465 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-12", Categoria = "general", Precio = 130m, Total = 37798, Disponibles = 7560 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-12", Categoria = "preferencial", Precio = 260m, Total = 11549, Disponibles = 2310 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-12", Categoria = "vip", Precio = 650m, Total = 3150, Disponibles = 347 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-13", Categoria = "general", Precio = 130m, Total = 30986, Disponibles = 6197 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-13", Categoria = "preferencial", Precio = 260m, Total = 9468, Disponibles = 1894 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-13", Categoria = "vip", Precio = 650m, Total = 2582, Disponibles = 284 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-14", Categoria = "general", Precio = 130m, Total = 50867, Disponibles = 11445 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-14", Categoria = "preferencial", Precio = 260m, Total = 15543, Disponibles = 3497 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-14", Categoria = "vip", Precio = 650m, Total = 4239, Disponibles = 525 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-15", Categoria = "general", Precio = 130m, Total = 46424, Disponibles = 10445 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-15", Categoria = "preferencial", Precio = 260m, Total = 14185, Disponibles = 3192 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-15", Categoria = "vip", Precio = 650m, Total = 3869, Disponibles = 479 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-16", Categoria = "general", Precio = 130m, Total = 49712, Disponibles = 11185 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-16", Categoria = "preferencial", Precio = 260m, Total = 15190, Disponibles = 3418 });
        list.Add(new CategoriaEntrada { PartidoId = "R32-16", Categoria = "vip", Precio = 650m, Total = 4143, Disponibles = 513 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-01", Categoria = "general", Precio = 180m, Total = 58077, Disponibles = 14519 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-01", Categoria = "preferencial", Precio = 340m, Total = 17746, Disponibles = 4437 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-01", Categoria = "vip", Precio = 800m, Total = 4840, Disponibles = 666 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-02", Categoria = "general", Precio = 180m, Total = 49193, Disponibles = 12298 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-02", Categoria = "preferencial", Precio = 340m, Total = 15031, Disponibles = 3758 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-02", Categoria = "vip", Precio = 800m, Total = 4099, Disponibles = 564 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-03", Categoria = "general", Precio = 180m, Total = 58193, Disponibles = 14548 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-03", Categoria = "preferencial", Precio = 340m, Total = 17781, Disponibles = 4445 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-03", Categoria = "vip", Precio = 800m, Total = 4849, Disponibles = 667 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-04", Categoria = "general", Precio = 180m, Total = 50867, Disponibles = 13988 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-04", Categoria = "preferencial", Precio = 340m, Total = 15543, Disponibles = 4274 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-04", Categoria = "vip", Precio = 800m, Total = 4239, Disponibles = 641 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-05", Categoria = "general", Precio = 180m, Total = 48186, Disponibles = 13251 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-05", Categoria = "preferencial", Precio = 340m, Total = 14724, Disponibles = 4049 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-05", Categoria = "vip", Precio = 800m, Total = 4016, Disponibles = 607 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-06", Categoria = "general", Precio = 180m, Total = 49519, Disponibles = 13618 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-06", Categoria = "preferencial", Precio = 340m, Total = 15131, Disponibles = 4161 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-06", Categoria = "vip", Precio = 800m, Total = 4127, Disponibles = 624 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-07", Categoria = "general", Precio = 180m, Total = 49132, Disponibles = 14740 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-07", Categoria = "preferencial", Precio = 340m, Total = 15013, Disponibles = 4504 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-07", Categoria = "vip", Precio = 800m, Total = 4094, Disponibles = 676 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-08", Categoria = "general", Precio = 180m, Total = 37798, Disponibles = 12284 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-08", Categoria = "preferencial", Precio = 340m, Total = 11549, Disponibles = 3753 });
        list.Add(new CategoriaEntrada { PartidoId = "R16-08", Categoria = "vip", Precio = 800m, Total = 3150, Disponibles = 563 });
        list.Add(new CategoriaEntrada { PartidoId = "QF-01", Categoria = "general", Precio = 260m, Total = 46185, Disponibles = 17319 });
        list.Add(new CategoriaEntrada { PartidoId = "QF-01", Categoria = "preferencial", Precio = 480m, Total = 14112, Disponibles = 5292 });
        list.Add(new CategoriaEntrada { PartidoId = "QF-01", Categoria = "vip", Precio = 1000m, Total = 3849, Disponibles = 794 });
        list.Add(new CategoriaEntrada { PartidoId = "QF-02", Categoria = "general", Precio = 260m, Total = 50754, Disponibles = 20302 });
        list.Add(new CategoriaEntrada { PartidoId = "QF-02", Categoria = "preferencial", Precio = 480m, Total = 15508, Disponibles = 6203 });
        list.Add(new CategoriaEntrada { PartidoId = "QF-02", Categoria = "vip", Precio = 1000m, Total = 4230, Disponibles = 931 });
        list.Add(new CategoriaEntrada { PartidoId = "QF-03", Categoria = "general", Precio = 260m, Total = 49712, Disponibles = 21128 });
        list.Add(new CategoriaEntrada { PartidoId = "QF-03", Categoria = "preferencial", Precio = 480m, Total = 15190, Disponibles = 6456 });
        list.Add(new CategoriaEntrada { PartidoId = "QF-03", Categoria = "vip", Precio = 1000m, Total = 4143, Disponibles = 968 });
        list.Add(new CategoriaEntrada { PartidoId = "QF-04", Categoria = "general", Precio = 260m, Total = 46424, Disponibles = 19730 });
        list.Add(new CategoriaEntrada { PartidoId = "QF-04", Categoria = "preferencial", Precio = 480m, Total = 14185, Disponibles = 6029 });
        list.Add(new CategoriaEntrada { PartidoId = "QF-04", Categoria = "vip", Precio = 1000m, Total = 3869, Disponibles = 904 });
        list.Add(new CategoriaEntrada { PartidoId = "SF-01", Categoria = "general", Precio = 420m, Total = 50867, Disponibles = 25434 });
        list.Add(new CategoriaEntrada { PartidoId = "SF-01", Categoria = "preferencial", Precio = 650m, Total = 15543, Disponibles = 7772 });
        list.Add(new CategoriaEntrada { PartidoId = "SF-01", Categoria = "vip", Precio = 1300m, Total = 4239, Disponibles = 1166 });
        list.Add(new CategoriaEntrada { PartidoId = "SF-02", Categoria = "general", Precio = 420m, Total = 49132, Disponibles = 25794 });
        list.Add(new CategoriaEntrada { PartidoId = "SF-02", Categoria = "preferencial", Precio = 650m, Total = 15013, Disponibles = 7882 });
        list.Add(new CategoriaEntrada { PartidoId = "SF-02", Categoria = "vip", Precio = 1300m, Total = 4094, Disponibles = 1182 });
        list.Add(new CategoriaEntrada { PartidoId = "TP-01", Categoria = "general", Precio = 340m, Total = 46424, Disponibles = 27854 });
        list.Add(new CategoriaEntrada { PartidoId = "TP-01", Categoria = "preferencial", Precio = 520m, Total = 14185, Disponibles = 8511 });
        list.Add(new CategoriaEntrada { PartidoId = "TP-01", Categoria = "vip", Precio = 950m, Total = 3869, Disponibles = 1277 });
        list.Add(new CategoriaEntrada { PartidoId = "FIN-01", Categoria = "general", Precio = 550m, Total = 58077, Disponibles = 36298 });
        list.Add(new CategoriaEntrada { PartidoId = "FIN-01", Categoria = "preferencial", Precio = 850m, Total = 17746, Disponibles = 11091 });
        list.Add(new CategoriaEntrada { PartidoId = "FIN-01", Categoria = "vip", Precio = 1500m, Total = 4840, Disponibles = 1664 });
        return list;
    }
}
