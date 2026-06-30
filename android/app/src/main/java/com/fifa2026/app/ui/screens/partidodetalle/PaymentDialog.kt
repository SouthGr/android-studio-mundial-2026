package com.fifa2026.app.ui.screens.partidodetalle

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.AlertDialog
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.unit.dp
import com.fifa2026.app.util.detectarMarcaTarjeta
import com.fifa2026.app.util.formatNum
import com.fifa2026.app.util.pasaLuhn
import com.fifa2026.app.util.validarVencimiento

/**
 * Igual que abrirModalPago() en app.js: formulario de tarjeta con validación de Luhn,
 * vencimiento MM/AA y CVV. Simulación de pago con fines académicos, no se procesa nada real.
 */
@Composable
fun PaymentDialog(
    total: Double,
    cantidad: Int,
    categoriaLabel: String,
    procesando: Boolean,
    onDismiss: () -> Unit,
    onConfirmar: () -> Unit,
) {
    var titular by remember { mutableStateOf("") }
    var numero by remember { mutableStateOf("") }
    var venc by remember { mutableStateOf("") }
    var cvv by remember { mutableStateOf("") }
    var error by remember { mutableStateOf<String?>(null) }

    AlertDialog(
        onDismissRequest = { if (!procesando) onDismiss() },
        title = { Text("💳 Pago con Tarjeta") },
        text = {
            Column {
                Text(
                    "Total a pagar: USD ${formatNum(total)} ($cantidad entrada${if (cantidad != 1) "s" else ""} · $categoriaLabel)",
                    style = MaterialTheme.typography.bodyMedium,
                )
                OutlinedTextField(
                    value = titular,
                    onValueChange = { titular = it },
                    label = { Text("Nombre del titular") },
                    singleLine = true,
                    modifier = Modifier.fillMaxWidth().padding(top = 12.dp),
                )
                OutlinedTextField(
                    value = numero,
                    onValueChange = { input ->
                        val limpio = input.filter { it.isDigit() }.take(19)
                        numero = limpio.chunked(4).joinToString(" ")
                    },
                    label = { Text("Número de tarjeta") },
                    placeholder = { Text("0000 0000 0000 0000") },
                    singleLine = true,
                    keyboardOptions = androidx.compose.foundation.text.KeyboardOptions(keyboardType = KeyboardType.Number),
                    supportingText = {
                        val limpio = numero.filter { it.isDigit() }
                        if (limpio.isNotEmpty()) Text(detectarMarcaTarjeta(limpio))
                    },
                    modifier = Modifier.fillMaxWidth().padding(top = 8.dp),
                )
                Row(modifier = Modifier.fillMaxWidth().padding(top = 8.dp), horizontalArrangement = Arrangement.spacedBy(8.dp)) {
                    OutlinedTextField(
                        value = venc,
                        onValueChange = { input ->
                            var limpio = input.filter { it.isDigit() }.take(4)
                            if (limpio.length >= 3) limpio = limpio.substring(0, 2) + "/" + limpio.substring(2)
                            venc = limpio
                        },
                        label = { Text("MM/AA") },
                        singleLine = true,
                        keyboardOptions = androidx.compose.foundation.text.KeyboardOptions(keyboardType = KeyboardType.Number),
                        modifier = Modifier.weight(1f),
                    )
                    OutlinedTextField(
                        value = cvv,
                        onValueChange = { cvv = it.filter { c -> c.isDigit() }.take(4) },
                        label = { Text("CVV") },
                        singleLine = true,
                        keyboardOptions = androidx.compose.foundation.text.KeyboardOptions(keyboardType = KeyboardType.Number),
                        modifier = Modifier.weight(1f),
                    )
                }
                error?.let {
                    Text(it, color = MaterialTheme.colorScheme.error, modifier = Modifier.padding(top = 8.dp))
                }
                Text(
                    "Simulación de pago con fines académicos. No se almacenan datos de tarjeta.",
                    style = MaterialTheme.typography.bodySmall,
                    color = MaterialTheme.colorScheme.onSurfaceVariant,
                    modifier = Modifier.padding(top = 10.dp),
                )
            }
        },
        confirmButton = {
            TextButton(
                enabled = !procesando,
                onClick = {
                    val numeroLimpio = numero.filter { it.isDigit() }
                    error = when {
                        titular.trim().length < 3 -> "Ingresá el nombre del titular tal como figura en la tarjeta."
                        numeroLimpio.length < 13 || numeroLimpio.length > 19 || !pasaLuhn(numeroLimpio) -> "El número de tarjeta no es válido."
                        validarVencimiento(venc) != null -> validarVencimiento(venc)
                        cvv.length < 3 || cvv.length > 4 -> "El CVV debe tener 3 o 4 dígitos."
                        else -> null
                    }
                    if (error == null) onConfirmar()
                },
            ) { Text(if (procesando) "Procesando..." else "🔒 Pagar USD ${formatNum(total)}") }
        },
        dismissButton = {
            TextButton(enabled = !procesando, onClick = onDismiss) { Text("Cancelar") }
        },
    )
}
