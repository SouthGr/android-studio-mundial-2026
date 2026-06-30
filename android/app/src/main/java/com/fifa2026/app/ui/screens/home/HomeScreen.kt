package com.fifa2026.app.ui.screens.home

import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import com.fifa2026.app.ui.components.ErrorView
import com.fifa2026.app.ui.components.LoadingView
import com.fifa2026.app.ui.components.MatchCard
import com.fifa2026.app.ui.components.StatMini
import com.fifa2026.app.ui.state.AppViewModel
import com.fifa2026.app.ui.theme.FifaAccent
import com.fifa2026.app.ui.theme.FifaPrimary
import com.fifa2026.app.ui.theme.FifaPrimaryDark

/** Igual que renderHome() en app.js. */
@Composable
fun HomeScreen(
    viewModel: AppViewModel,
    onVerPartidos: () -> Unit,
    onVerEstadios: () -> Unit,
    onPartidoClick: (String) -> Unit,
) {
    val uiState by viewModel.uiState.collectAsState()

    when {
        uiState.cargando -> LoadingView()
        uiState.error != null -> ErrorView(mensaje = uiState.error ?: "Error desconocido", onReintentar = viewModel::cargarDatos)
        else -> {
            val destacados = (
                uiState.partidos.filter { it.estado == "en_juego" } +
                    uiState.partidos.filter { it.estado == "programado" }.take(6)
                ).take(6)

            LazyColumn(contentPadding = PaddingValues(bottom = 24.dp)) {
                item {
                    HeroSection(onVerPartidos = onVerPartidos, onVerEstadios = onVerEstadios)
                }
                item {
                    Row(
                        modifier = Modifier.fillMaxWidth().padding(horizontal = 16.dp, vertical = 16.dp),
                        horizontalArrangement = Arrangement.SpaceBetween,
                    ) {
                        StatMini(valor = "48", etiqueta = "Equipos")
                        StatMini(valor = uiState.partidos.size.toString(), etiqueta = "Partidos")
                        StatMini(valor = uiState.estadios.size.toString(), etiqueta = "Estadios")
                        StatMini(valor = "3", etiqueta = "Países sede")
                    }
                }

                if (destacados.isNotEmpty()) {
                    item {
                        val titulo = if (uiState.partidos.any { it.estado == "en_juego" }) "🔴 En Juego Ahora" else "📅 Próximos Partidos"
                        Text(
                            titulo,
                            style = MaterialTheme.typography.titleLarge,
                            fontWeight = FontWeight.Bold,
                            color = FifaPrimary,
                            modifier = Modifier.padding(horizontal = 16.dp, vertical = 8.dp),
                        )
                    }
                    items(destacados, key = { it.partidoId }) { partido ->
                        MatchCard(
                            partido = partido,
                            getEquipo = uiState::getEquipo,
                            onClick = { onPartidoClick(partido.partidoId) },
                            modifier = Modifier.padding(horizontal = 16.dp, vertical = 6.dp),
                        )
                    }
                }

                item {
                    Text(
                        "Todas las Fases del Torneo",
                        style = MaterialTheme.typography.titleLarge,
                        fontWeight = FontWeight.Bold,
                        color = FifaPrimary,
                        modifier = Modifier.padding(horizontal = 16.dp, vertical = 8.dp),
                    )
                }
                val fases = uiState.partidos.map { it.fase }.distinct()
                items(fases) { fase ->
                    val count = uiState.partidos.count { it.fase == fase }
                    Row(
                        modifier = Modifier
                            .fillMaxWidth()
                            .padding(horizontal = 16.dp, vertical = 4.dp),
                        horizontalArrangement = Arrangement.SpaceBetween,
                        verticalAlignment = Alignment.CenterVertically,
                    ) {
                        Text(fase, style = MaterialTheme.typography.bodyLarge)
                        Text(
                            "$count partido${if (count != 1) "s" else ""}",
                            style = MaterialTheme.typography.bodySmall,
                            color = MaterialTheme.colorScheme.onSurfaceVariant,
                        )
                    }
                }
            }
        }
    }
}

@Composable
private fun HeroSection(onVerPartidos: () -> Unit, onVerEstadios: () -> Unit) {
    Column(
        modifier = Modifier
            .fillMaxWidth()
            .background(FifaPrimaryDark)
            .padding(20.dp),
    ) {
        Text(
            "Sistema Oficial de Entradas",
            style = MaterialTheme.typography.labelMedium,
            color = FifaAccent,
        )
        Text(
            "FIFA World Cup 2026™",
            style = MaterialTheme.typography.headlineLarge,
            fontWeight = FontWeight.Bold,
            color = androidx.compose.ui.graphics.Color.White,
            modifier = Modifier.padding(top = 8.dp),
        )
        Text(
            "Adquirí tus entradas para los partidos del Mundial de Fútbol más grande de la historia. 48 equipos, 3 países, 104 partidos.",
            style = MaterialTheme.typography.bodyMedium,
            color = androidx.compose.ui.graphics.Color.White.copy(alpha = 0.85f),
            modifier = Modifier.padding(top = 12.dp),
        )
        Row(modifier = Modifier.padding(top = 16.dp), horizontalArrangement = Arrangement.spacedBy(10.dp)) {
            Button(onClick = onVerPartidos) { Text("🎟️ Ver Partidos") }
            OutlinedButton(onClick = onVerEstadios) { Text("🏟️ Estadios") }
        }
    }
}
