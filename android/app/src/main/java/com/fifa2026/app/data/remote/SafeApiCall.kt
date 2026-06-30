package com.fifa2026.app.data.remote

import com.fifa2026.app.data.model.ApiErrorDto
import kotlinx.serialization.json.Json
import retrofit2.Response
import java.io.IOException

class ApiException(message: String) : Exception(message)

private val errorJson = Json { ignoreUnknownKeys = true }

/**
 * Ejecuta una llamada Retrofit y la transforma en Result<T>, igual que el patrón
 * try/fetch/catch que usa procesarCompra() y el login en app.js: si la respuesta
 * no es 2xx, intenta leer { "mensaje": "..." } del body de error.
 */
suspend fun <T> safeApiCall(block: suspend () -> Response<T>): Result<T> = try {
    val response = block()
    val body = response.body()
    if (response.isSuccessful && body != null) {
        Result.success(body)
    } else {
        val mensaje = response.errorBody()?.string()?.let { raw ->
            runCatching { errorJson.decodeFromString<ApiErrorDto>(raw).mensaje }.getOrNull()
        } ?: "Error del servidor (${response.code()})."
        Result.failure(ApiException(mensaje))
    }
} catch (e: IOException) {
    Result.failure(ApiException("No se pudo conectar con el servidor. ¿Está corriendo la API?"))
} catch (e: Exception) {
    Result.failure(ApiException(e.message ?: "Ocurrió un error inesperado."))
}
