package com.fifa2026.app.ui.screens.estadios

import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.grid.GridCells
import androidx.compose.foundation.lazy.grid.LazyVerticalGrid
import androidx.compose.foundation.lazy.grid.items
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import com.fifa2026.app.ui.components.ErrorView
import com.fifa2026.app.ui.components.LoadingView
import com.fifa2026.app.ui.components.StadiumCard
import com.fifa2026.app.ui.state.AppViewModel
import com.fifa2026.app.ui.theme.FifaPrimary

/** Igual que renderEstadios() en app.js. */
@Composable
fun EstadiosScreen(viewModel: AppViewModel, onEstadioClick: (String) -> Unit) {
    val uiState by viewModel.uiState.collectAsState()

    when {
        uiState.cargando -> LoadingView()
        uiState.error != null -> ErrorView(mensaje = uiState.error ?: "Error desconocido", onReintentar = viewModel::cargarDatos)
        else -> {
            LazyVerticalGrid(
                columns = GridCells.Fixed(2),
                contentPadding = PaddingValues(16.dp),
            ) {
                item(span = { androidx.compose.foundation.lazy.grid.GridItemSpan(maxLineSpan) }) {
                    Text(
                        "Estadios",
                        style = MaterialTheme.typography.headlineMedium,
                        fontWeight = FontWeight.Bold,
                        color = FifaPrimary,
                    )
                    Text(
                        "${uiState.estadios.size} sedes en EE.UU., México y Canadá",
                        style = MaterialTheme.typography.bodyMedium,
                        color = MaterialTheme.colorScheme.onSurfaceVariant,
                        modifier = Modifier.padding(bottom = 12.dp),
                    )
                }
                items(uiState.estadios, key = { it.estadioId }) { estadio ->
                    StadiumCard(
                        estadio = estadio,
                        onClick = { onEstadioClick(estadio.estadioId) },
                        modifier = Modifier.padding(6.dp),
                    )
                }
            }
        }
    }
}
