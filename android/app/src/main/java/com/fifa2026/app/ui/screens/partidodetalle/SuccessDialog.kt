package com.fifa2026.app.ui.screens.partidodetalle

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.AlertDialog
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import com.fifa2026.app.data.model.ReservaResponseDto
import com.fifa2026.app.ui.theme.FifaAccent
import com.fifa2026.app.util.formatNum

/** Igual que el modal "¡Compra Exitosa!" de procesarCompra() en app.js. */
@Composable
fun SuccessDialog(reserva: ReservaResponseDto, descripcionPartido: String, onDismiss: () -> Unit) {
    AlertDialog(
        onDismissRequest = onDismiss,
        title = { Text("🎉 ¡Compra Exitosa!") },
        text = {
            Column(horizontalAlignment = Alignment.CenterHorizontally, modifier = Modifier.fillMaxWidth()) {
                Text(
                    "Tu compra de ${reserva.cantidad} entrada${if (reserva.cantidad != 1) "s" else ""} (${reserva.categoria.replaceFirstChar { it.uppercase() }}) para $descripcionPartido fue procesada correctamente.",
                    style = MaterialTheme.typography.bodyMedium,
                )
                Text(
                    "Total cobrado: USD ${formatNum(reserva.total)}",
                    style = MaterialTheme.typography.bodyMedium,
                    fontWeight = FontWeight.Bold,
                    modifier = Modifier.padding(top = 8.dp),
                )
                Text(
                    reserva.codigo,
                    style = MaterialTheme.typography.titleLarge,
                    fontWeight = FontWeight.Bold,
                    color = FifaAccent,
                    modifier = Modifier.padding(top = 14.dp),
                )
                Text(
                    "Guardá este código de reserva.",
                    style = MaterialTheme.typography.bodySmall,
                    color = MaterialTheme.colorScheme.onSurfaceVariant,
                    modifier = Modifier.padding(top = 4.dp),
                )
            }
        },
        confirmButton = {
            TextButton(onClick = onDismiss) { Text("✓ Entendido") }
        },
    )
}
