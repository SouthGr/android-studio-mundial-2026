package com.fifa2026.app.data.model

import kotlinx.serialization.Serializable

@Serializable
data class EquipoDto(
    val codigoFifa: String,
    val nombre: String,
    val banderaEmoji: String,
    val grupo: String? = null,
    val esAnfitrion: Boolean = false,
)

@Serializable
data class EstadioResumenDto(
    val estadioId: String,
    val nombre: String,
    val ciudad: String,
    val pais: String,
    val imagenEmoji: String,
)

@Serializable
data class EstadioDto(
    val estadioId: String,
    val nombre: String,
    val ciudad: String,
    val pais: String,
    val banderaPaisEmoji: String,
    val capacidad: Int,
    val superficie: String,
    val inaugurado: Int,
    val descripcion: String,
    val datos: String,
    val imagenEmoji: String,
    val cantidadPartidos: Int,
)

@Serializable
data class CategoriaEntradaDto(
    val categoria: String,
    val precio: Double,
    val total: Int,
    val disponibles: Int,
)

@Serializable
data class PartidoDto(
    val partidoId: String,
    val fase: String,
    val grupo: String? = null,
    val jornada: Int? = null,
    val fecha: String,
    val hora: String,
    val estado: String,
    val golesLocal: Int? = null,
    val golesVisitante: Int? = null,
    val notaResultado: String? = null,
    val estadio: EstadioResumenDto,
    val local: EquipoDto? = null,
    val visitante: EquipoDto? = null,
    val entradas: List<CategoriaEntradaDto> = emptyList(),
)

@Serializable
data class ReservaRequestDto(
    val partidoId: String,
    val categoria: String,
    val cantidad: Int,
)

@Serializable
data class ReservaResponseDto(
    val codigo: String,
    val partidoId: String,
    val categoria: String,
    val cantidad: Int,
    val precioUnitario: Double,
    val total: Double,
    val fechaReserva: String,
    val disponibles: Int,
)

@Serializable
data class RegistroRequestDto(
    val nombre: String,
    val email: String,
    val password: String,
)

@Serializable
data class LoginRequestDto(
    val email: String,
    val password: String,
)

@Serializable
data class AuthResponseDto(
    val token: String,
    val expira: String,
    val usuarioId: Int,
    val nombre: String,
    val email: String,
    val rol: String,
)

/** Forma genérica de los errores que devuelve la API: { "mensaje": "..." } */
@Serializable
data class ApiErrorDto(
    val mensaje: String? = null,
)

/** Estado local de un partido derivado de sus categorías de entradas. */
enum class CategoriaEntrada(val id: String, val etiqueta: String) {
    GENERAL("general", "General"),
    PREFERENCIAL("preferencial", "Preferencial"),
    VIP("vip", "VIP");

    companion object {
        fun fromId(id: String) = entries.find { it.id == id } ?: GENERAL
    }
}

enum class EstadoPartido(val id: String) {
    PROGRAMADO("programado"),
    EN_JUEGO("en_juego"),
    FINALIZADO("finalizado"),
    POR_DEFINIR("por_definir");

    companion object {
        fun fromId(id: String) = entries.find { it.id == id } ?: POR_DEFINIR
    }
}
