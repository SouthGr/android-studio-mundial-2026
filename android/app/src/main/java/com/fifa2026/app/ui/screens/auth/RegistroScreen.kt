package com.fifa2026.app.ui.screens.auth

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.Button
import androidx.compose.material3.CircularProgressIndicator
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
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.compose.ui.unit.dp
import com.fifa2026.app.ui.state.AppViewModel
import com.fifa2026.app.ui.theme.FifaPrimary

/** Igual que renderRegistro() en app.js. */
@Composable
fun RegistroScreen(viewModel: AppViewModel, onRegistroExitoso: () -> Unit, onIrALogin: () -> Unit) {
    var nombre by remember { mutableStateOf("") }
    var email by remember { mutableStateOf("") }
    var password by remember { mutableStateOf("") }
    var error by remember { mutableStateOf<String?>(null) }
    var cargando by remember { mutableStateOf(false) }

    Column(modifier = Modifier.fillMaxWidth().padding(24.dp)) {
        Text("Crear Cuenta", style = MaterialTheme.typography.headlineMedium, fontWeight = FontWeight.Bold, color = FifaPrimary)
        Text(
            "Registrate para comprar tus entradas",
            style = MaterialTheme.typography.bodyMedium,
            color = MaterialTheme.colorScheme.onSurfaceVariant,
            modifier = Modifier.padding(bottom = 24.dp),
        )

        OutlinedTextField(
            value = nombre,
            onValueChange = { nombre = it; error = null },
            label = { Text("Nombre completo") },
            placeholder = { Text("Tu nombre") },
            singleLine = true,
            modifier = Modifier.fillMaxWidth(),
        )
        OutlinedTextField(
            value = email,
            onValueChange = { email = it; error = null },
            label = { Text("Email") },
            placeholder = { Text("vos@email.com") },
            singleLine = true,
            keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Email),
            modifier = Modifier.fillMaxWidth().padding(top = 12.dp),
        )
        OutlinedTextField(
            value = password,
            onValueChange = { password = it; error = null },
            label = { Text("Contraseña") },
            placeholder = { Text("Mínimo 6 caracteres") },
            singleLine = true,
            visualTransformation = PasswordVisualTransformation(),
            keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Password),
            modifier = Modifier.fillMaxWidth().padding(top = 12.dp),
        )

        error?.let {
            Text(it, color = MaterialTheme.colorScheme.error, modifier = Modifier.padding(top = 10.dp))
        }

        Button(
            onClick = {
                if (password.length < 6) {
                    error = "La contraseña debe tener al menos 6 caracteres."
                    return@Button
                }
                cargando = true
                error = null
                viewModel.registro(nombre.trim(), email.trim(), password) { resultado ->
                    cargando = false
                    resultado.onSuccess { onRegistroExitoso() }
                        .onFailure { e -> error = e.message ?: "No se pudo crear la cuenta." }
                }
            },
            enabled = !cargando && nombre.isNotBlank() && email.isNotBlank() && password.isNotBlank(),
            modifier = Modifier.fillMaxWidth().padding(top = 18.dp),
        ) {
            if (cargando) {
                CircularProgressIndicator(modifier = Modifier.size(18.dp), strokeWidth = 2.dp)
            } else {
                Text("Crear cuenta")
            }
        }

        TextButton(onClick = onIrALogin, modifier = Modifier.padding(top = 8.dp)) {
            Text("¿Ya tenés cuenta? Iniciá sesión")
        }
    }
}
