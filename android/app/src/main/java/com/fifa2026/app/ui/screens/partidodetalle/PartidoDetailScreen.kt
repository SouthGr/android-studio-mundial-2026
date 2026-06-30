package com.fifa2026.app.ui.screens.partidodetalle

import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.selection.selectable
import androidx.compose.foundation.verticalScroll
import androidx.compose.material3.Button
import androidx.compose.material3.Card
import androidx.compose.material3.CardDefaults
import androidx.compose.material3.IconButton
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.RadioButton
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableIntStateOf
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import com.fifa2026.app.data.model.CategoriaEntrada
import com.fifa2026.app.data.session.Sesion
import com.fifa2026.app.ui.components.EmptyState
import com.fifa2026.app.ui.components.FlagImage
import com.fifa2026.app.ui.state.AppViewModel
import com.fifa2026.app.ui.theme.FifaAccent
import com.fifa2026.app.ui.theme.FifaBorder
import com.fifa2026.app.ui.theme.FifaLive
import com.fifa2026.app.ui.theme.FifaPrimary
import com.fifa2026.app.ui.theme.FifaPrimaryDark
import com.fifa2026.app.ui.theme.FifaSuccess
import com.fifa2026.app.ui.theme.FifaTextMuted
import com.fifa2026.app.util.badgeEstado
import com.fifa2026.app.util.banderaEquipoUrl
import com.fifa2026.app.util.formatFecha
import com.fifa2026.app.util.formatNum

private const val MAX_POR_COMPRA = 10

/** Igual que renderDetallePartido() + selector de entradas + procesarCompra() en app.js. */
@Composable
fun PartidoDetailScreen(
    viewModel: AppViewModel,
    partidoId: String,
    sesion: Sesion?,
    onEstadioClick: (String) -> Unit,
    onLoginRequerido: () -> Unit,
) {
    val uiState by viewModel.uiState.collectAsState()
    val partido = uiState.partidos.find { it.partidoId == partidoId }

    if (partido == null) {
        EmptyState(icono = "❓", titulo = "Partido no encontrado", descripcion = "Puede que ya no esté disponible.")
        return
    }

    val local = uiState.getEquipo(partido.local?.codigoFifa)
    val visitante = uiState.getEquipo(partido.visitante?.codigoFifa)
    val badge = badgeEstado(partido.estado)
    val isPorDef = partido.estado == "por_definir"
    val isFinished = partido.estado == "finalizado"
    val isLive = partido.estado == "en_juego"

    val categoriasDisponibles = CategoriaEntrada.entries.mapNotNull { cat ->
        partido.entradas.find { it.categoria == cat.id }?.let { cat to it }
    }
    var categoriaSel by remember(partido.partidoId) {
        mutableStateOf(categoriasDisponibles.firstOrNull { it.second.disponibles > 0 }?.first ?: CategoriaEntrada.GENERAL)
    }
    var cantidad by remember(partido.partidoId) { mutableIntStateOf(1) }
    var mostrarPago by remember { mutableStateOf(false) }
    var procesando by remember { mutableStateOf(false) }
    var reservaExitosa by remember { mutableStateOf<com.fifa2026.app.data.model.ReservaResponseDto?>(null) }
    var errorCompra by remember { mutableStateOf<String?>(null) }

    val entradaSel = partido.entradas.find { it.categoria == categoriaSel.id }
    val disponiblesSel = entradaSel?.disponibles ?: 0
    val precioSel = entradaSel?.precio ?: 0.0
    val total = precioSel * cantidad

    Column(modifier = Modifier.verticalScroll(rememberScrollState())) {
        // HERO
        Column(modifier = Modifier.fillMaxWidth().background(FifaPrimaryDark).padding(20.dp)) {
            Row(horizontalArrangement = Arrangement.spacedBy(8.dp)) {
                BadgeChip(badge.label, isLive)
                if (partido.grupo != null) BadgeChip("Grupo ${partido.grupo} · ${partido.fase}", false)
                else BadgeChip(partido.fase, false)
            }
            Spacer(Modifier.height(16.dp))
            if (isPorDef) {
                Text(
                    "🏆 Clasificados por determinar",
                    color = androidx.compose.ui.graphics.Color.White,
                    style = MaterialTheme.typography.titleMedium,
                )
            } else {
                Row(verticalAlignment = Alignment.CenterVertically, modifier = Modifier.fillMaxWidth()) {
                    EquipoHero(nombre = local.nombre, codigo = partido.local?.codigoFifa, modifier = Modifier.weight(1f))
                    val centro = if (isFinished || isLive) "${partido.golesLocal ?: 0} – ${partido.golesVisitante ?: 0}" else "VS"
                    Text(
                        centro,
                        color = androidx.compose.ui.graphics.Color.White,
                        style = MaterialTheme.typography.headlineMedium,
                        fontWeight = FontWeight.Bold,
                        modifier = Modifier.padding(horizontal = 12.dp),
                    )
                    EquipoHero(nombre = visitante.nombre, codigo = partido.visitante?.codigoFifa, modifier = Modifier.weight(1f))
                }
            }
            Spacer(Modifier.height(16.dp))
            Text("📅 ${formatFecha(partido.fecha)}  ·  🕐 ${partido.hora} hs", color = androidx.compose.ui.graphics.Color.White.copy(alpha = 0.8f), style = MaterialTheme.typography.bodySmall)
            Text("📍 ${partido.estadio.nombre} · 🏙 ${partido.estadio.ciudad}", color = androidx.compose.ui.graphics.Color.White.copy(alpha = 0.8f), style = MaterialTheme.typography.bodySmall)
        }

        Column(modifier = Modifier.padding(16.dp)) {
            if (isFinished) {
                InfoBanner(
                    titulo = "✅ Partido Finalizado",
                    texto = "Este partido ya se disputó. El resultado final fue ${local.nombre} ${partido.golesLocal ?: 0} – ${partido.golesVisitante ?: 0} ${visitante.nombre}.",
                    color = FifaAccent,
                )
            }
            if (isLive) {
                InfoBanner(
                    titulo = "🔴 PARTIDO EN VIVO",
                    texto = "El partido está en curso. Marcador actual: ${local.nombre} ${partido.golesLocal ?: 0} – ${partido.golesVisitante ?: 0} ${visitante.nombre}.",
                    color = FifaLive,
                )
            }

            // Card del estadio
            Card(
                modifier = Modifier.fillMaxWidth().padding(top = 8.dp).clickable { onEstadioClick(partido.estadio.estadioId) },
                colors = CardDefaults.cardColors(containerColor = MaterialTheme.colorScheme.surface),
            ) {
                Column(modifier = Modifier.padding(14.dp)) {
                    Text("🏟️ Estadio", style = MaterialTheme.typography.labelMedium, color = FifaPrimary, fontWeight = FontWeight.Bold)
                    Text(partido.estadio.nombre, style = MaterialTheme.typography.titleMedium, fontWeight = FontWeight.SemiBold, modifier = Modifier.padding(top = 4.dp))
                    Text("📍 ${partido.estadio.ciudad}, ${partido.estadio.pais}", style = MaterialTheme.typography.bodySmall, color = FifaTextMuted)
                    Text("Ver más sobre el estadio →", style = MaterialTheme.typography.labelMedium, color = FifaPrimary, modifier = Modifier.padding(top = 8.dp))
                }
            }

            Spacer(Modifier.height(20.dp))

            // Sección de compra
            Text("🎟️ Comprar Entradas", style = MaterialTheme.typography.titleLarge, fontWeight = FontWeight.Bold, color = FifaPrimary)
            Spacer(Modifier.height(10.dp))

            when {
                isFinished -> Text("⛔ La venta de entradas para este partido ya ha cerrado.", color = FifaTextMuted, modifier = Modifier.padding(vertical = 16.dp))
                sesion == null -> Column(horizontalAlignment = Alignment.CenterHorizontally, modifier = Modifier.fillMaxWidth().padding(vertical = 16.dp)) {
                    Text("🔐 Iniciá sesión para comprar tus entradas.", color = FifaTextMuted)
                    Button(onClick = onLoginRequerido, modifier = Modifier.padding(top = 12.dp).fillMaxWidth()) { Text("Iniciar Sesión") }
                }
                else -> {
                    categoriasDisponibles.forEach { (cat, entrada) ->
                        CategoriaSelector(
                            categoria = cat,
                            entrada = entrada,
                            seleccionada = categoriaSel == cat,
                            onSeleccionar = {
                                categoriaSel = cat
                                cantidad = 1
                            },
                        )
                    }

                    Spacer(Modifier.height(14.dp))
                    Row(verticalAlignment = Alignment.CenterVertically, horizontalArrangement = Arrangement.SpaceBetween, modifier = Modifier.fillMaxWidth()) {
                        Text("Cantidad de entradas", style = MaterialTheme.typography.bodyMedium)
                        Row(verticalAlignment = Alignment.CenterVertically) {
                            IconButton(onClick = { if (cantidad > 1) cantidad-- }, enabled = cantidad > 1) { Text("−", style = MaterialTheme.typography.titleLarge) }
                            Text(cantidad.toString(), style = MaterialTheme.typography.titleMedium, modifier = Modifier.padding(horizontal = 8.dp))
                            IconButton(
                                onClick = { if (cantidad < minOf(MAX_POR_COMPRA, disponiblesSel)) cantidad++ },
                                enabled = cantidad < minOf(MAX_POR_COMPRA, disponiblesSel),
                            ) { Text("+", style = MaterialTheme.typography.titleLarge) }
                        }
                    }

                    Spacer(Modifier.height(14.dp))
                    Surface(color = FifaPrimaryDark, modifier = Modifier.fillMaxWidth()) {
                        Row(
                            modifier = Modifier.fillMaxWidth().padding(16.dp),
                            horizontalArrangement = Arrangement.SpaceBetween,
                            verticalAlignment = Alignment.CenterVertically,
                        ) {
                            Column {
                                Text("Total a pagar", color = androidx.compose.ui.graphics.Color.White.copy(alpha = 0.8f), style = MaterialTheme.typography.bodySmall)
                                Text("USD · Precio final", color = androidx.compose.ui.graphics.Color.White.copy(alpha = 0.5f), style = MaterialTheme.typography.labelSmall)
                            }
                            Text("USD ${formatNum(total)}", color = FifaAccent, style = MaterialTheme.typography.headlineMedium, fontWeight = FontWeight.Bold)
                        }
                    }

                    Button(
                        onClick = { mostrarPago = true },
                        enabled = disponiblesSel > 0,
                        modifier = Modifier.fillMaxWidth().padding(top = 14.dp),
                    ) { Text("🎟️ Comprar Ahora") }

                    errorCompra?.let {
                        Text(it, color = MaterialTheme.colorScheme.error, modifier = Modifier.padding(top = 8.dp))
                    }
                }
            }
        }
    }

    if (mostrarPago) {
        PaymentDialog(
            total = total,
            cantidad = cantidad,
            categoriaLabel = categoriaSel.etiqueta,
            procesando = procesando,
            onDismiss = { if (!procesando) mostrarPago = false },
            onConfirmar = {
                procesando = true
                errorCompra = null
                viewModel.comprarEntradas(partido.partidoId, categoriaSel.id, cantidad) { resultado ->
                    procesando = false
                    resultado.onSuccess { reserva ->
                        mostrarPago = false
                        reservaExitosa = reserva
                        cantidad = 1
                    }.onFailure { e ->
                        errorCompra = e.message ?: "No se pudo procesar la compra."
                    }
                }
            },
        )
    }

    reservaExitosa?.let { reserva ->
        val descripcion = if (isPorDef) partido.fase else "${local.nombre} vs ${visitante.nombre}"
        SuccessDialog(reserva = reserva, descripcionPartido = descripcion, onDismiss = { reservaExitosa = null })
    }
}

@Composable
private fun BadgeChip(texto: String, destacado: Boolean) {
    Surface(
        color = if (destacado) FifaLive.copy(alpha = 0.2f) else androidx.compose.ui.graphics.Color.White.copy(alpha = 0.15f),
        contentColor = androidx.compose.ui.graphics.Color.White,
        shape = androidx.compose.foundation.shape.RoundedCornerShape(50),
    ) {
        Text(texto, style = MaterialTheme.typography.labelSmall, modifier = Modifier.padding(horizontal = 10.dp, vertical = 4.dp))
    }
}

@Composable
private fun EquipoHero(nombre: String, codigo: String?, modifier: Modifier = Modifier) {
    Column(modifier = modifier, horizontalAlignment = Alignment.CenterHorizontally) {
        FlagImage(url = banderaEquipoUrl(codigo), contentDescription = nombre, modifier = Modifier.size(40.dp, 28.dp))
        Text(
            nombre,
            color = androidx.compose.ui.graphics.Color.White,
            style = MaterialTheme.typography.bodyMedium,
            fontWeight = FontWeight.SemiBold,
            textAlign = androidx.compose.ui.text.style.TextAlign.Center,
            modifier = Modifier.padding(top = 6.dp),
        )
    }
}

@Composable
private fun InfoBanner(titulo: String, texto: String, color: androidx.compose.ui.graphics.Color) {
    Card(
        modifier = Modifier.fillMaxWidth().padding(bottom = 8.dp),
        colors = CardDefaults.cardColors(containerColor = color.copy(alpha = 0.08f)),
    ) {
        Column(modifier = Modifier.padding(14.dp)) {
            Text(titulo, fontWeight = FontWeight.Bold, color = color, style = MaterialTheme.typography.titleMedium)
            Text(texto, style = MaterialTheme.typography.bodyMedium, modifier = Modifier.padding(top = 6.dp))
        }
    }
}

@Composable
private fun CategoriaSelector(
    categoria: CategoriaEntrada,
    entrada: com.fifa2026.app.data.model.CategoriaEntradaDto,
    seleccionada: Boolean,
    onSeleccionar: () -> Unit,
) {
    val agotado = entrada.disponibles == 0
    Surface(
        modifier = Modifier
            .fillMaxWidth()
            .padding(vertical = 4.dp)
            .selectable(selected = seleccionada, enabled = !agotado, onClick = onSeleccionar),
        color = if (seleccionada) FifaAccent.copy(alpha = 0.1f) else MaterialTheme.colorScheme.surface,
        border = androidx.compose.foundation.BorderStroke(1.dp, if (seleccionada) FifaAccent else FifaBorder),
        shape = androidx.compose.foundation.shape.RoundedCornerShape(10.dp),
    ) {
        Row(modifier = Modifier.padding(12.dp), verticalAlignment = Alignment.CenterVertically) {
            RadioButton(selected = seleccionada, onClick = onSeleccionar, enabled = !agotado)
            Column(modifier = Modifier.weight(1f).padding(start = 6.dp)) {
                Text(categoria.etiqueta, fontWeight = FontWeight.SemiBold, style = MaterialTheme.typography.bodyMedium)
                Text(
                    if (agotado) "AGOTADO" else "${formatNum(entrada.disponibles)} disponibles",
                    style = MaterialTheme.typography.bodySmall,
                    color = if (agotado) MaterialTheme.colorScheme.error else if (entrada.disponibles < 100) FifaAccent else FifaSuccess,
                )
            }
            Text("USD ${formatNum(entrada.precio)}", fontWeight = FontWeight.Bold, color = FifaPrimary)
        }
    }
}
