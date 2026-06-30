package com.fifa2026.app.ui.screens.cuenta

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Button
import androidx.compose.material3.Card
import androidx.compose.material3.CardDefaults
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import com.fifa2026.app.data.session.Sesion
import com.fifa2026.app.ui.theme.FifaAccent
import com.fifa2026.app.ui.theme.FifaPrimary

/** Pantalla de cuenta: combina lo que en la web son el estado de sesión en la navbar + login/registro. */
@Composable
fun CuentaScreen(
    sesion: Sesion?,
    onIrALogin: () -> Unit,
    onIrARegistro: () -> Unit,
    onCerrarSesion: () -> Unit,
) {
    Column(
        modifier = Modifier.fillMaxSize().padding(24.dp),
        horizontalAlignment = Alignment.CenterHorizontally,
    ) {
        if (sesion == null) {
            Text("🔐", style = MaterialTheme.typography.headlineLarge)
            Text(
                "No iniciaste sesión",
                style = MaterialTheme.typography.titleLarge,
                fontWeight = FontWeight.Bold,
                color = FifaPrimary,
                modifier = Modifier.padding(top = 8.dp),
            )
            Text(
                "Iniciá sesión para comprar entradas y ver tu cuenta.",
                style = MaterialTheme.typography.bodyMedium,
                color = MaterialTheme.colorScheme.onSurfaceVariant,
                modifier = Modifier.padding(top = 4.dp, bottom = 20.dp),
            )
            Button(onClick = onIrALogin, modifier = Modifier.fillMaxWidth()) { Text("Iniciar Sesión") }
            OutlinedButton(onClick = onIrARegistro, modifier = Modifier.fillMaxWidth().padding(top = 10.dp)) { Text("Crear Cuenta") }
        } else {
            Card(modifier = Modifier.fillMaxWidth(), colors = CardDefaults.cardColors(containerColor = MaterialTheme.colorScheme.surface)) {
                Column(modifier = Modifier.padding(20.dp), horizontalAlignment = Alignment.CenterHorizontally) {
                    Text("👤", style = MaterialTheme.typography.headlineLarge)
                    Text(
                        sesion.nombre,
                        style = MaterialTheme.typography.titleLarge,
                        fontWeight = FontWeight.Bold,
                        modifier = Modifier.padding(top = 8.dp),
                    )
                    Text(sesion.email, style = MaterialTheme.typography.bodyMedium, color = MaterialTheme.colorScheme.onSurfaceVariant)
                    if (sesion.esAdmin) {
                        Text(
                            "⚙️ Administrador",
                            style = MaterialTheme.typography.labelMedium,
                            color = FifaAccent,
                            modifier = Modifier.padding(top = 6.dp),
                        )
                    }
                }
            }
            OutlinedButton(onClick = onCerrarSesion, modifier = Modifier.fillMaxWidth().padding(top = 20.dp)) {
                Text("🚪 Cerrar sesión")
            }
        }
    }
}
