package com.fifa2026.app.data.repository

import com.fifa2026.app.data.model.EquipoDto
import com.fifa2026.app.data.model.EstadioDto
import com.fifa2026.app.data.model.PartidoDto
import com.fifa2026.app.data.model.ReservaRequestDto
import com.fifa2026.app.data.model.ReservaResponseDto
import com.fifa2026.app.data.remote.ApiService
import com.fifa2026.app.data.remote.safeApiCall
import kotlinx.coroutines.coroutineScope
import kotlinx.coroutines.async

data class DatosMundial(
    val equipos: List<EquipoDto>,
    val estadios: List<EstadioDto>,
    val partidos: List<PartidoDto>,
)

/** Equivalente a cargarDatos() y procesarCompra() en app.js. */
class FifaRepository(private val api: ApiService) {

    suspend fun cargarDatos(): Result<DatosMundial> = coroutineScope {
        val equiposDef = async { safeApiCall { api.getEquipos() } }
        val estadiosDef = async { safeApiCall { api.getEstadios() } }
        val partidosDef = async { safeApiCall { api.getPartidos() } }

        val equipos = equiposDef.await().getOrElse { return@coroutineScope Result.failure(it) }
        val estadios = estadiosDef.await().getOrElse { return@coroutineScope Result.failure(it) }
        val partidos = partidosDef.await().getOrElse { return@coroutineScope Result.failure(it) }

        Result.success(DatosMundial(equipos, estadios, partidos))
    }

    suspend fun comprarEntradas(partidoId: String, categoria: String, cantidad: Int): Result<ReservaResponseDto> =
        safeApiCall { api.crearReserva(ReservaRequestDto(partidoId, categoria, cantidad)) }
}
