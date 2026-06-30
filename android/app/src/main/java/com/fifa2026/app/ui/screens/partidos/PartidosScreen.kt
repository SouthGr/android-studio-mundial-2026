package com.fifa2026.app.ui.screens.partidos

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Clear
import androidx.compose.material.icons.filled.Search
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import com.fifa2026.app.ui.components.EmptyState
import com.fifa2026.app.ui.components.ErrorView
import com.fifa2026.app.ui.components.FilterDropdown
import com.fifa2026.app.ui.components.FiltroOpcion
import com.fifa2026.app.ui.components.LoadingView
import com.fifa2026.app.ui.components.MatchCard
import com.fifa2026.app.ui.state.AppViewModel
import com.fifa2026.app.ui.theme.FifaPrimary

/** Igual que renderPartidos() + aplicarFiltros() en app.js. */
@Composable
fun PartidosScreen(viewModel: AppViewModel, onPartidoClick: (String) -> Unit) {
    val uiState by viewModel.uiState.collectAsState()

    when {
        uiState.cargando -> LoadingView()
        uiState.error != null -> ErrorView(mensaje = uiState.error ?: "Error desconocido", onReintentar = viewModel::cargarDatos)
        else -> {
            val grupos = uiState.partidos.mapNotNull { it.grupo }.distinct().sorted()
            val fases = uiState.partidos.map { it.fase }.distinct()
            val filtrados = uiState.partidosFiltrados()

            LazyColumn(contentPadding = PaddingValues(16.dp)) {
                item {
                    Text(
                        "Partidos",
                        style = MaterialTheme.typography.headlineMedium,
                        fontWeight = FontWeight.Bold,
                        color = FifaPrimary,
                    )
                    Text(
                        "Mostrando ${filtrados.size} de ${uiState.partidos.size} partidos",
                        style = MaterialTheme.typography.bodyMedium,
                        color = MaterialTheme.colorScheme.onSurfaceVariant,
                        modifier = Modifier.padding(bottom = 12.dp),
                    )

                    OutlinedTextField(
                        value = uiState.filtros.busqueda,
                        onValueChange = viewModel::setBusqueda,
                        label = { Text("Buscar equipo") },
                        placeholder = { Text("Ej: Argentina, Brasil...") },
                        leadingIcon = { Icon(Icons.Default.Search, contentDescription = null) },
                        trailingIcon = {
                            if (uiState.filtros.busqueda.isNotEmpty()) {
                                IconButton(onClick = { viewModel.setBusqueda("") }) {
                                    Icon(Icons.Default.Clear, contentDescription = "Limpiar búsqueda")
                                }
                            }
                        },
                        singleLine = true,
                        modifier = Modifier.fillMaxWidth().padding(bottom = 10.dp),
                    )

                    FilterDropdown(
                        label = "Grupo",
                        opciones = listOf(FiltroOpcion("all", "Todos los grupos")) + grupos.map { FiltroOpcion(it, "Grupo $it") },
                        seleccionado = uiState.filtros.grupo,
                        onSeleccionar = viewModel::setGrupo,
                        modifier = Modifier.fillMaxWidth().padding(bottom = 10.dp),
                    )
                    FilterDropdown(
                        label = "Fase",
                        opciones = listOf(FiltroOpcion("all", "Todas las fases")) + fases.map { FiltroOpcion(it, it) },
                        seleccionado = uiState.filtros.fase,
                        onSeleccionar = viewModel::setFase,
                        modifier = Modifier.fillMaxWidth().padding(bottom = 10.dp),
                    )
                    FilterDropdown(
                        label = "Estadio",
                        opciones = listOf(FiltroOpcion("all", "Todos los estadios")) + uiState.estadios.map { FiltroOpcion(it.estadioId, it.nombre) },
                        seleccionado = uiState.filtros.estadioId,
                        onSeleccionar = viewModel::setEstadioId,
                        modifier = Modifier.fillMaxWidth().padding(bottom = 6.dp),
                    )

                    if (uiState.filtros != com.fifa2026.app.ui.state.FiltrosPartidos()) {
                        TextButton(onClick = viewModel::resetFiltros) { Text("✕ Limpiar filtros") }
                    }
                }

                if (filtrados.isEmpty()) {
                    item {
                        Column(horizontalAlignment = androidx.compose.ui.Alignment.CenterHorizontally) {
                            EmptyState(
                                icono = "🔍",
                                titulo = "No se encontraron partidos",
                                descripcion = "Intentá con otros filtros de búsqueda",
                            )
                            TextButton(onClick = viewModel::resetFiltros) { Text("Limpiar filtros") }
                        }
                    }
                } else {
                    items(filtrados, key = { it.partidoId }) { partido ->
                        MatchCard(
                            partido = partido,
                            getEquipo = uiState::getEquipo,
                            onClick = { onPartidoClick(partido.partidoId) },
                            modifier = Modifier.fillMaxWidth().padding(vertical = 6.dp),
                        )
                    }
                }
            }
        }
    }
}
