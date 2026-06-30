package com.fifa2026.app.data.session

import android.content.Context
import androidx.datastore.preferences.core.edit
import androidx.datastore.preferences.core.stringPreferencesKey
import androidx.datastore.preferences.preferencesDataStore
import com.fifa2026.app.data.model.AuthResponseDto
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.first
import kotlinx.coroutines.flow.map
import kotlinx.serialization.Serializable
import kotlinx.serialization.encodeToString
import kotlinx.serialization.json.Json
import java.time.Instant
import java.time.format.DateTimeParseException

private val Context.dataStore by preferencesDataStore(name = "fifa2026_sesion")

@Serializable
data class Sesion(
    val token: String,
    val expira: String,
    val usuarioId: Int,
    val nombre: String,
    val email: String,
    val rol: String,
) {
    val esAdmin: Boolean get() = rol == "Administrador"
}

/**
 * Equivalente a localStorage en la web (cargarSesion/guardarSesion/cerrarSesion en app.js):
 * persiste el JWT y lo expone como Flow para que toda la UI reaccione al login/logout.
 */
class SessionManager(private val context: Context) {

    private val json = Json { ignoreUnknownKeys = true }
    private val keySesion = stringPreferencesKey("sesion_json")

    val sesion: Flow<Sesion?> = context.dataStore.data.map { prefs ->
        val raw = prefs[keySesion] ?: return@map null
        runCatching { json.decodeFromString<Sesion>(raw) }
            .getOrNull()
            ?.takeIf { !estaExpirada(it.expira) }
    }

    suspend fun guardarSesion(auth: AuthResponseDto) {
        val sesion = Sesion(auth.token, auth.expira, auth.usuarioId, auth.nombre, auth.email, auth.rol)
        context.dataStore.edit { prefs ->
            prefs[keySesion] = json.encodeToString(sesion)
        }
    }

    suspend fun cerrarSesion() {
        context.dataStore.edit { prefs -> prefs.remove(keySesion) }
    }

    suspend fun tokenActual(): String? = sesion.first()?.token

    private fun estaExpirada(expira: String): Boolean = try {
        Instant.parse(expira).isBefore(Instant.now())
    } catch (e: DateTimeParseException) {
        false
    }
}
