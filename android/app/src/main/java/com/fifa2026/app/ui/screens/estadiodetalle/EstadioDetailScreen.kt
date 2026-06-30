package com.fifa2026.app.ui.screens.estadiodetalle

import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.verticalScroll
import androidx.compose.material3.Card
import androidx.compose.material3.CardDefaults
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import com.fifa2026.app.ui.components.EmptyState
import com.fifa2026.app.ui.state.AppViewModel
import com.fifa2026.app.ui.theme.FifaAccent
import com.fifa2026.app.ui.theme.FifaPrimary
import com.fifa2026.app.ui.theme.FifaPrimaryDark
import com.fifa2026.app.ui.theme.FifaTextMuted
import com.fifa2026.app.util.badgeEstado
import com.fifa2026.app.util.formatFecha
import com.fifa2026.app.util.formatNum

/** Igual que renderDetalleEstadio() en app.js. */
@Composable
fun EstadioDetailScreen(viewModel: AppViewModel, estadioId: String, onPartidoClick: (String) -> Unit) {
    val uiState by viewModel.uiState.collectAsState()
    val estadio = uiState.estadios.find { it.estadioId == estadioId }

    if (estadio == null) {
        EmptyState(icono = "❓", titulo = "Estadio no encontrado", descripcion = "Puede que ya no esté disponible.")
        return
    }

    val partidos = uiState.partidos.filter { it.estadio.estadioId == estadioId }

    Column(modifier = Modifier.verticalScroll(rememberScrollState())) {
        Column(modifier = Modifier.fillMaxWidth().background(FifaPrimaryDark).padding(20.dp)) {
            Text(estadio.imagenEmoji, style = MaterialTheme.typography.headlineLarge)
            Text(
                estadio.pais.uppercase(),
                style = MaterialTheme.typography.labelMedium,
                color = androidx.compose.ui.graphics.Color.White.copy(alpha = 0.6f),
                modifier = Modifier.padding(top = 6.dp),
            )
            Text(
                estadio.nombre,
                style = MaterialTheme.typography.headlineMedium,
                fontWeight = FontWeight.Bold,
                color = androidx.compose.ui.graphics.Color.White,
            )
            Text("📍 ${estadio.ciudad}", style = MaterialTheme.typography.bodyMedium, color = androidx.compose.ui.graphics.Color.White.copy(alpha = 0.8f))
        }

        Column(modifier = Modifier.padding(16.dp)) {
            Card(modifier = Modifier.fillMaxWidth(), colors = CardDefaults.cardColors(containerColor = MaterialTheme.colorScheme.surface)) {
                Column(modifier = Modifier.padding(14.dp)) {
                    Text("📋 Información General", style = MaterialTheme.typography.titleMedium, fontWeight = FontWeight.Bold, color = FifaPrimary)
                    Spacer(Modifier.height(8.dp))
                    InfoRow("Ciudad", "${estadio.ciudad}, ${estadio.pais}")
                    InfoRow("Capacidad", "${formatNum(estadio.capacidad)} espectadores")
                    InfoRow("Superficie", estadio.superficie)
                    InfoRow("Inaugurado", estadio.inaugurado.toString())
                    InfoRow("Partidos 2026", "${partidos.size} partido${if (partidos.size != 1) "s" else ""}")
                }
            }

            Spacer(Modifier.height(14.dp))

            Card(modifier = Modifier.fillMaxWidth(), colors = CardDefaults.cardColors(containerColor = MaterialTheme.colorScheme.surface)) {
                Column(modifier = Modifier.padding(14.dp)) {
                    Text("ℹ️ Sobre el Estadio", style = MaterialTheme.typography.titleMedium, fontWeight = FontWeight.Bold, color = FifaPrimary)
                    Text(estadio.descripcion, style = MaterialTheme.typography.bodyMedium, modifier = Modifier.padding(top = 8.dp))
                    Text(
                        estadio.datos,
                        style = MaterialTheme.typography.bodySmall,
                        color = FifaTextMuted,
                        modifier = Modifier.padding(top = 8.dp),
                    )
                }
            }

            Spacer(Modifier.height(14.dp))

            Text("⚽ Partidos en este Estadio", style = MaterialTheme.typography.titleMedium, fontWeight = FontWeight.Bold, color = FifaPrimary)
            Spacer(Modifier.height(8.dp))

            if (partidos.isEmpty()) {
                Text("No hay partidos asignados aún.", style = MaterialTheme.typography.bodyMedium, color = FifaTextMuted)
            } else {
                partidos.forEach { partido ->
                    val local = uiState.getEquipo(partido.local?.codigoFifa)
                    val visitante = uiState.getEquipo(partido.visitante?.codigoFifa)
                    val badge = badgeEstado(partido.estado)
                    Card(
                        modifier = Modifier.fillMaxWidth().padding(vertical = 4.dp).clickable { onPartidoClick(partido.partidoId) },
                        colors = CardDefaults.cardColors(containerColor = MaterialTheme.colorScheme.surface),
                    ) {
                        Column(modifier = Modifier.padding(12.dp)) {
                            Row(modifier = Modifier.fillMaxWidth(), horizontalArrangement = androidx.compose.foundation.layout.Arrangement.SpaceBetween) {
                                Text(
                                    (partido.grupo?.let { "Grupo $it · " } ?: "") + partido.fase,
                                    style = MaterialTheme.typography.labelSmall,
                                    color = FifaTextMuted,
                                )
                                Text(badge.label, style = MaterialTheme.typography.labelSmall, color = FifaAccent)
                            }
                            Text(
                                if (partido.estado == "por_definir") "🏆 Clasificados por determinar" else "${local.nombre} vs ${visitante.nombre}",
                                style = MaterialTheme.typography.bodyMedium,
                                fontWeight = FontWeight.SemiBold,
                                modifier = Modifier.padding(top = 4.dp),
                            )
                            Text(
                                "📅 ${formatFecha(partido.fecha)} · ${partido.hora} hs",
                                style = MaterialTheme.typography.bodySmall,
                                color = FifaTextMuted,
                                modifier = Modifier.padding(top = 2.dp),
                            )
                        }
                    }
                }
            }
        }
    }
}

@Composable
private fun InfoRow(label: String, valor: String) {
    Row(modifier = Modifier.fillMaxWidth().padding(vertical = 3.dp), horizontalArrangement = androidx.compose.foundation.layout.Arrangement.SpaceBetween) {
        Text(label, style = MaterialTheme.typography.bodySmall, color = FifaTextMuted)
        Text(valor, style = MaterialTheme.typography.bodyMedium, fontWeight = FontWeight.Medium)
    }
}
