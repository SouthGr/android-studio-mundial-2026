package com.fifa2026.app.data.repository

import com.fifa2026.app.data.model.AuthResponseDto
import com.fifa2026.app.data.model.LoginRequestDto
import com.fifa2026.app.data.model.RegistroRequestDto
import com.fifa2026.app.data.remote.ApiService
import com.fifa2026.app.data.remote.safeApiCall
import com.fifa2026.app.data.session.SessionManager

/** Equivalente a las funciones de autenticación de app.js (login, registro, guardarSesion, cerrarSesion). */
class AuthRepository(
    private val api: ApiService,
    private val sessionManager: SessionManager,
) {
    suspend fun login(email: String, password: String): Result<AuthResponseDto> =
        safeApiCall { api.login(LoginRequestDto(email, password)) }.onSuccess { sessionManager.guardarSesion(it) }

    suspend fun registro(nombre: String, email: String, password: String): Result<AuthResponseDto> =
        safeApiCall { api.registro(RegistroRequestDto(nombre, email, password)) }.onSuccess { sessionManager.guardarSesion(it) }

    suspend fun cerrarSesion() = sessionManager.cerrarSesion()
}
