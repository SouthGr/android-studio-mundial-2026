package com.fifa2026.app.util

// Mismo mapeo que FIFA_TO_ISO en app.js: muchos dispositivos no tienen fuente de emoji
// de banderas, así que usamos imágenes reales de flagcdn.com (igual que la web).
private val FIFA_TO_ISO = mapOf(
    "MEX" to "mx", "RSA" to "za", "KOR" to "kr", "CZE" to "cz",
    "SUI" to "ch", "CAN" to "ca", "BIH" to "ba", "QAT" to "qa",
    "BRA" to "br", "MAR" to "ma", "SCO" to "gb-sct", "HAI" to "ht",
    "USA" to "us", "AUS" to "au", "PAR" to "py", "TUR" to "tr",
    "GER" to "de", "CIV" to "ci", "ECU" to "ec", "CUW" to "cw",
    "NED" to "nl", "JAP" to "jp", "SWE" to "se", "TUN" to "tn",
    "BEL" to "be", "EGY" to "eg", "IRN" to "ir", "NZL" to "nz",
    "ESP" to "es", "CPV" to "cv", "URU" to "uy", "SAU" to "sa",
    "FRA" to "fr", "NOR" to "no", "SEN" to "sn", "IRQ" to "iq",
    "ARG" to "ar", "AUT" to "at", "ALG" to "dz", "JOR" to "jo",
    "COL" to "co", "POR" to "pt", "COD" to "cd", "UZB" to "uz",
    "ENG" to "gb-eng", "CRO" to "hr", "GHA" to "gh", "PAN" to "pa",
)

private val PAIS_TO_ISO = mapOf(
    "México" to "mx", "Estados Unidos" to "us", "Canadá" to "ca",
)

/** URL de bandera (PNG) para un equipo por su código FIFA, o null si no hay mapeo (TBD). */
fun banderaEquipoUrl(codigoFifa: String?): String? {
    val iso = FIFA_TO_ISO[codigoFifa] ?: return null
    return "https://flagcdn.com/h60/$iso.png"
}

/** URL de bandera (PNG) para un país sede (usado en estadios). */
fun banderaPaisUrl(pais: String?): String? {
    val iso = PAIS_TO_ISO[pais] ?: return null
    return "https://flagcdn.com/h60/$iso.png"
}
