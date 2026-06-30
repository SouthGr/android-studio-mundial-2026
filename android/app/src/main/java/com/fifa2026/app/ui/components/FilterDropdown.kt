package com.fifa2026.app.ui.components

import androidx.compose.material3.DropdownMenuItem
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.ExposedDropdownMenuBox
import androidx.compose.material3.ExposedDropdownMenuDefaults
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.style.TextOverflow

data class FiltroOpcion(val valor: String, val etiqueta: String)

/** Selector desplegable simple, usado para los filtros de grupo/fase/estadio (igual que los <select> de app.js). */
@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun FilterDropdown(
    label: String,
    opciones: List<FiltroOpcion>,
    seleccionado: String,
    onSeleccionar: (String) -> Unit,
    modifier: Modifier = Modifier,
) {
    var expanded by remember { mutableStateOf(false) }
    val etiquetaActual = opciones.find { it.valor == seleccionado }?.etiqueta ?: opciones.firstOrNull()?.etiqueta.orEmpty()

    ExposedDropdownMenuBox(expanded = expanded, onExpandedChange = { expanded = it }, modifier = modifier) {
        OutlinedTextField(
            value = etiquetaActual,
            onValueChange = {},
            readOnly = true,
            label = { Text(label) },
            trailingIcon = { ExposedDropdownMenuDefaults.TrailingIcon(expanded = expanded) },
            textStyle = MaterialTheme.typography.bodyMedium,
            maxLines = 1,
            modifier = Modifier.menuAnchor(),
        )
        ExposedDropdownMenu(expanded = expanded, onDismissRequest = { expanded = false }) {
            opciones.forEach { opcion ->
                DropdownMenuItem(
                    text = { Text(opcion.etiqueta, overflow = TextOverflow.Ellipsis, maxLines = 1) },
                    onClick = {
                        onSeleccionar(opcion.valor)
                        expanded = false
                    },
                )
            }
        }
    }
}
