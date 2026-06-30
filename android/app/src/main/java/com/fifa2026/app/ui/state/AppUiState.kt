package com.fifa2026.app.ui.state

import com.fifa2026.app.data.model.EquipoDto
import com.fifa2026.app.data.model.EstadioDto
import com.fifa2026.app.data.model.PartidoDto

data class FiltrosPartidos(
    val busqueda: String = "",
    val grupo: String = "all",
    val fase: String = "all",
    val estadioId: String = "all",
)

data class AppUiState(
    val cargando: Boolean = true,
    val error: String? = null,
    val equipos: Map<String, EquipoDto> = emptyMap(),
    val estadios: List<EstadioDto> = emptyList(),
    val partidos: List<PartidoDto> = emptyList(),
    val filtros: FiltrosPartidos = FiltrosPartidos(),
) {
    /** Igual que getEquipo() en app.js: si no hay código, "Por Definir"; si no se encuentra, devuelve el código tal cual. */
    fun getEquipo(codigo: String?): EquipoDto {
        if (codigo == null) return EquipoDto(codigoFifa = "", nombre = "Por Definir", banderaEmoji = "❓")
        return equipos[codigo] ?: EquipoDto(codigoFifa = codigo, nombre = codigo, banderaEmoji = "🏳")
    }

    fun getEstadio(id: String?): EstadioDto? = estadios.find { it.estadioId == id }

    /** Igual que aplicarFiltros() en app.js. */
    fun partidosFiltrados(): List<PartidoDto> {
        val query = filtros.busqueda.trim().lowercase()
        return partidos.filter { p ->
            if (filtros.grupo != "all" && p.grupo != filtros.grupo) return@filter false
            if (filtros.fase != "all" && p.fase != filtros.fase) return@filter false
            if (filtros.estadioId != "all" && p.estadio.estadioId != filtros.estadioId) return@filter false
            if (query.isNotEmpty()) {
                val local = getEquipo(p.local?.codigoFifa).nombre.lowercase()
                val visitante = getEquipo(p.visitante?.codigoFifa).nombre.lowercase()
                val estadioNombre = p.estadio.nombre.lowercase()
                if (!local.contains(query) && !visitante.contains(query) && !estadioNombre.contains(query)) {
                    return@filter false
                }
            }
            true
        }
    }
}
