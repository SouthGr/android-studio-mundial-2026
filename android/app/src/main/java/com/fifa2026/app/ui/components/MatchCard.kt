package com.fifa2026.app.ui.components

import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material3.Card
import androidx.compose.material3.CardDefaults
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import com.fifa2026.app.data.model.EquipoDto
import com.fifa2026.app.data.model.PartidoDto
import com.fifa2026.app.ui.theme.FifaAccent
import com.fifa2026.app.ui.theme.FifaBorder
import com.fifa2026.app.ui.theme.FifaDanger
import com.fifa2026.app.ui.theme.FifaLive
import com.fifa2026.app.ui.theme.FifaPrimary
import com.fifa2026.app.ui.theme.FifaSuccess
import com.fifa2026.app.ui.theme.FifaTextMuted
import com.fifa2026.app.ui.theme.FifaWarning
import com.fifa2026.app.util.badgeEstado
import com.fifa2026.app.util.banderaEquipoUrl
import com.fifa2026.app.util.calcPctVendido
import com.fifa2026.app.util.formatFecha

/** Igual que crearMatchCard() en app.js. */
@Composable
fun MatchCard(
    partido: PartidoDto,
    getEquipo: (String?) -> EquipoDto,
    onClick: () -> Unit,
    modifier: Modifier = Modifier,
) {
    val local = getEquipo(partido.local?.codigoFifa)
    val visitante = getEquipo(partido.visitante?.codigoFifa)
    val badge = badgeEstado(partido.estado)
    val pct = calcPctVendido(partido)

    val isLive = partido.estado == "en_juego"
    val isFinished = partido.estado == "finalizado"
    val isPorDef = partido.estado == "por_definir"
    val isAgotado = pct >= 100

    val availColor = when {
        isAgotado -> FifaDanger
        pct >= 90 -> FifaWarning
        pct >= 70 -> FifaAccent
        else -> FifaSuccess
    }
    val availText = when {
        isAgotado -> "🚫 Agotado"
        pct >= 90 -> "⚡ $pct% vendido — ¡Últimas entradas!"
        else -> "$pct% vendido"
    }

    Card(
        modifier = modifier.fillMaxWidth().clickable(onClick = onClick),
        colors = CardDefaults.cardColors(containerColor = MaterialTheme.colorScheme.surface),
        border = if (isLive) androidx.compose.foundation.BorderStroke(1.5.dp, FifaLive) else androidx.compose.foundation.BorderStroke(1.dp, FifaBorder),
        elevation = CardDefaults.cardElevation(defaultElevation = 1.dp),
    ) {
        Column(modifier = Modifier.padding(14.dp)) {
            // Header: fase/grupo + badge
            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.SpaceBetween,
                verticalAlignment = Alignment.CenterVertically,
            ) {
                val grupoInfo = partido.grupo?.let { "GRUPO $it · " } ?: ""
                val jornadaInfo = partido.jornada?.let { "JOR. $it · " } ?: ""
                Text(
                    text = "$grupoInfo$jornadaInfo${partido.fase}".uppercase(),
                    style = MaterialTheme.typography.labelSmall,
                    color = FifaTextMuted,
                )
                EstadoBadge(label = badge.label, isLive = isLive)
            }

            Box(modifier = Modifier.height(8.dp))

            // Equipos
            if (isPorDef) {
                Text(
                    "🏆 Clasificados por determinar",
                    style = MaterialTheme.typography.bodyMedium,
                    color = FifaTextMuted,
                    modifier = Modifier.fillMaxWidth().padding(vertical = 12.dp),
                    textAlign = TextAlign.Center,
                )
            } else {
                Row(
                    modifier = Modifier.fillMaxWidth().padding(vertical = 8.dp),
                    verticalAlignment = Alignment.CenterVertically,
                ) {
                    EquipoRow(equipo = local, codigoFifa = partido.local?.codigoFifa, modifier = Modifier.weight(1f))

                    val centro = when {
                        isFinished || isLive -> "${partido.golesLocal ?: 0} - ${partido.golesVisitante ?: 0}"
                        else -> "VS"
                    }
                    Text(
                        centro,
                        style = MaterialTheme.typography.titleMedium,
                        fontWeight = FontWeight.Bold,
                        color = FifaPrimary,
                        modifier = Modifier.padding(horizontal = 10.dp),
                    )

                    EquipoRow(
                        equipo = visitante,
                        codigoFifa = partido.visitante?.codigoFifa,
                        modifier = Modifier.weight(1f),
                        alinearDerecha = true,
                    )
                }
            }

            Box(modifier = Modifier.height(8.dp))

            Text(
                "📅 ${formatFecha(partido.fecha)} · ${partido.hora}",
                style = MaterialTheme.typography.bodySmall,
                color = FifaTextMuted,
            )
            Text(
                "📍 ${partido.estadio.nombre}, ${partido.estadio.ciudad}",
                style = MaterialTheme.typography.bodySmall,
                color = FifaTextMuted,
            )

            Box(modifier = Modifier.height(10.dp))

            // Barra de disponibilidad
            Box(
                modifier = Modifier
                    .fillMaxWidth()
                    .height(6.dp)
                    .clip(RoundedCornerShape(3.dp))
                    .background(FifaBorder),
            ) {
                Box(
                    modifier = Modifier
                        .fillMaxWidth(fraction = (pct.coerceIn(0, 100)) / 100f)
                        .height(6.dp)
                        .clip(RoundedCornerShape(3.dp))
                        .background(availColor),
                )
            }
            Text(
                availText,
                style = MaterialTheme.typography.labelSmall,
                color = availColor,
                modifier = Modifier.padding(top = 4.dp),
            )
        }
    }
}

@Composable
private fun EstadoBadge(label: String, isLive: Boolean) {
    Surface(
        color = if (isLive) FifaLive.copy(alpha = 0.15f) else FifaBorder,
        contentColor = if (isLive) FifaLive else FifaTextMuted,
        shape = RoundedCornerShape(50),
    ) {
        Text(
            label,
            style = MaterialTheme.typography.labelSmall,
            modifier = Modifier.padding(horizontal = 10.dp, vertical = 4.dp),
        )
    }
}

@Composable
private fun EquipoRow(
    equipo: EquipoDto,
    codigoFifa: String?,
    modifier: Modifier = Modifier,
    alinearDerecha: Boolean = false,
) {
    Row(
        modifier = modifier,
        horizontalArrangement = if (alinearDerecha) Arrangement.End else Arrangement.Start,
        verticalAlignment = Alignment.CenterVertically,
    ) {
        if (alinearDerecha) {
            Text(
                equipo.nombre,
                style = MaterialTheme.typography.bodyMedium,
                fontWeight = FontWeight.Medium,
                maxLines = 1,
                textAlign = TextAlign.End,
                modifier = Modifier.weight(1f, fill = false).padding(end = 6.dp),
            )
            FlagImage(url = banderaEquipoUrl(codigoFifa), contentDescription = equipo.nombre)
        } else {
            FlagImage(url = banderaEquipoUrl(codigoFifa), contentDescription = equipo.nombre)
            Text(
                equipo.nombre,
                style = MaterialTheme.typography.bodyMedium,
                fontWeight = FontWeight.Medium,
                maxLines = 1,
                modifier = Modifier.padding(start = 6.dp),
            )
        }
    }
}
