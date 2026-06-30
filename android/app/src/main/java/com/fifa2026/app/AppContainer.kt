package com.fifa2026.app

import android.content.Context
import com.fifa2026.app.data.remote.RetrofitClient
import com.fifa2026.app.data.repository.AuthRepository
import com.fifa2026.app.data.repository.FifaRepository
import com.fifa2026.app.data.session.SessionManager

/**
 * Contenedor manual de dependencias (sin Hilt para mantener el proyecto simple).
 * Se crea una sola vez en FifaApplication y vive durante toda la vida de la app.
 */
class AppContainer(context: Context) {
    val sessionManager = SessionManager(context.applicationContext)
    private val apiService = RetrofitClient.create(sessionManager)
    val fifaRepository = FifaRepository(apiService)
    val authRepository = AuthRepository(apiService, sessionManager)
}
