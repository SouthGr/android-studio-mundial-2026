package com.fifa2026.app.util

import com.fifa2026.app.data.model.PartidoDto
import java.text.NumberFormat
import java.util.Locale

private val MESES = listOf("ENE", "FEB", "MAR", "ABR", "MAY", "JUN", "JUL", "AGO", "SEP", "OCT", "NOV", "DIC")

/** Igual que formatFecha() en app.js: "2026-06-11" -> "11 JUN 2026" */
fun formatFecha(fechaStr: String?): String {
    if (fechaStr.isNullOrBlank()) return "-"
    val partes = fechaStr.split("-").mapNotNull { it.toIntOrNull() }
    if (partes.size != 3) return fechaStr
    val (anio, mes, dia) = partes
    val nombreMes = MESES.getOrNull(mes - 1) ?: ""
    return "$dia $nombreMes $anio"
}

private val numberFormat = NumberFormat.getNumberInstance(Locale.Builder().setLanguage("es").setRegion("AR").build())

/** Igual que formatNum() en app.js: separador de miles. */
fun formatNum(n: Number): String = numberFormat.format(n)

/** Igual que calcPctVendido() en app.js, usando el inventario embebido en el propio PartidoDto. */
fun calcPctVendido(partido: PartidoDto): Int {
    var total = 0
    var disponibles = 0
    partido.entradas.forEach { cat ->
        total += cat.total
        disponibles += cat.disponibles
    }
    if (total == 0) return 0
    val vendidos = total - disponibles
    return Math.round((vendidos.toDouble() / total.toDouble()) * 100).toInt()
}

data class BadgeEstado(val label: String)

/** Igual que getBadgeEstado() en app.js. */
fun badgeEstado(estado: String): BadgeEstado = when (estado) {
    "programado" -> BadgeEstado("Programado")
    "en_juego" -> BadgeEstado("🟢 En Juego")
    "finalizado" -> BadgeEstado("Finalizado")
    else -> BadgeEstado("Por Definir")
}
