package com.fifa2026.app.ui.navigation

object Routes {
    const val HOME = "home"
    const val PARTIDOS = "partidos"
    const val PARTIDO_DETALLE = "partido/{partidoId}"
    const val ESTADIOS = "estadios"
    const val ESTADIO_DETALLE = "estadio/{estadioId}"
    const val LOGIN = "login"
    const val REGISTRO = "registro"
    const val CUENTA = "cuenta"

    fun partidoDetalle(id: String) = "partido/$id"
    fun estadioDetalle(id: String) = "estadio/$id"
}
