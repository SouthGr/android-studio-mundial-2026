package com.fifa2026.app.util

import java.time.YearMonth

/** Algoritmo de Luhn — valida el formato de un número de tarjeta (no verifica fondos ni nada real). */
fun pasaLuhn(numero: String): Boolean {
    var suma = 0
    var alternar = false
    for (i in numero.length - 1 downTo 0) {
        var digito = numero[i] - '0'
        if (alternar) {
            digito *= 2
            if (digito > 9) digito -= 9
        }
        suma += digito
        alternar = !alternar
    }
    return suma % 10 == 0
}

fun detectarMarcaTarjeta(numero: String): String = when {
    numero.startsWith("4") -> "💳 Visa"
    Regex("^5[1-5]").containsMatchIn(numero) -> "💳 Mastercard"
    Regex("^3[47]").containsMatchIn(numero) -> "💳 American Express"
    else -> "💳 Tarjeta"
}

/** Valida MM/AA y que no esté vencida. Devuelve null si es válida, o un mensaje de error. */
fun validarVencimiento(venc: String): String? {
    val match = Regex("^(\\d{2})/(\\d{2})$").find(venc) ?: return "Vencimiento inválido. Usá el formato MM/AA."
    val mes = match.groupValues[1].toInt()
    val anio = 2000 + match.groupValues[2].toInt()
    if (mes < 1 || mes > 12) return "Vencimiento inválido. Usá el formato MM/AA."
    val finMesVenc = YearMonth.of(anio, mes)
    if (finMesVenc.isBefore(YearMonth.now())) return "La tarjeta está vencida o la fecha es inválida."
    return null
}
