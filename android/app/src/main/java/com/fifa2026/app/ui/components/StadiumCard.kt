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
import androidx.compose.material3.Card
import androidx.compose.material3.CardDefaults
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import com.fifa2026.app.data.model.EstadioDto
import com.fifa2026.app.ui.theme.FifaPrimary
import com.fifa2026.app.ui.theme.FifaPrimaryMid
import com.fifa2026.app.ui.theme.FifaTextMuted
import com.fifa2026.app.util.formatNum

/** Igual que crearStadiumCard() en app.js. */
@Composable
fun StadiumCard(estadio: EstadioDto, onClick: () -> Unit, modifier: Modifier = Modifier) {
    Card(
        modifier = modifier.fillMaxWidth().clickable(onClick = onClick),
        colors = CardDefaults.cardColors(containerColor = MaterialTheme.colorScheme.surface),
    ) {
        Box(
            modifier = Modifier.fillMaxWidth().height(90.dp).background(FifaPrimaryMid),
            contentAlignment = Alignment.Center,
        ) {
            Text(estadio.imagenEmoji, style = MaterialTheme.typography.headlineLarge)
        }
        Column(modifier = Modifier.padding(14.dp)) {
            Text(estadio.nombre, style = MaterialTheme.typography.titleMedium, fontWeight = FontWeight.SemiBold, color = FifaPrimary)
            Text(
                "📍 ${estadio.ciudad}, ${estadio.pais}",
                style = MaterialTheme.typography.bodySmall,
                color = FifaTextMuted,
                modifier = Modifier.padding(top = 2.dp),
            )
            Row(
                modifier = Modifier.fillMaxWidth().padding(top = 10.dp),
                horizontalArrangement = Arrangement.SpaceBetween,
            ) {
                StatMini(formatNum(estadio.capacidad), "Capacidad")
                StatMini(estadio.inaugurado.toString(), "Inaugurado")
                StatMini(estadio.cantidadPartidos.toString(), "Partidos")
            }
        }
    }
}

@Composable
fun StatMini(valor: String, etiqueta: String, modifier: Modifier = Modifier) {
    Column(modifier = modifier, horizontalAlignment = Alignment.CenterHorizontally) {
        Text(valor, style = MaterialTheme.typography.titleMedium, fontWeight = FontWeight.Bold, color = FifaPrimary)
        Text(etiqueta, style = MaterialTheme.typography.labelSmall, color = FifaTextMuted)
    }
}
