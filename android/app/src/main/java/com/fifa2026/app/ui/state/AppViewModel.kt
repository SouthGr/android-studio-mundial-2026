package com.fifa2026.app.ui.state

import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.viewModelScope
import com.fifa2026.app.data.model.AuthResponseDto
import com.fifa2026.app.data.model.CategoriaEntradaDto
import com.fifa2026.app.data.model.PartidoDto
import com.fifa2026.app.data.model.ReservaResponseDto
import com.fifa2026.app.data.repository.AuthRepository
import com.fifa2026.app.data.repository.FifaRepository
import com.fifa2026.app.data.session.Sesion
import com.fifa2026.app.data.session.SessionManager
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.SharingStarted
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.stateIn
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

/**
 * ViewModel compartido por toda la app: equivalente al objeto global `DATA` + `state`
 * de app.js (cargarDatos, aplicarFiltros, procesarCompra, sesión).
 */
class AppViewModel(
    private val fifaRepository: FifaRepository,
    private val authRepository: AuthRepository,
    sessionManager: SessionManager,
) : ViewModel() {

    private val _uiState = MutableStateFlow(AppUiState())
    val uiState: StateFlow<AppUiState> = _uiState.asStateFlow()

    val sesion: StateFlow<Sesion?> = sessionManager.sesion
        .stateIn(viewModelScope, SharingStarted.WhileSubscribed(5_000), null)

    init {
        cargarDatos()
    }

    fun cargarDatos() {
        viewModelScope.launch {
            _uiState.update { it.copy(cargando = true, error = null) }
            fifaRepository.cargarDatos()
                .onSuccess { datos ->
                    _uiState.update {
                        it.copy(
                            cargando = false,
                            error = null,
                            equipos = datos.equipos.associateBy { e -> e.codigoFifa },
                            estadios = datos.estadios,
                            partidos = datos.partidos,
                        )
                    }
                }
                .onFailure { e ->
                    _uiState.update { it.copy(cargando = false, error = e.message) }
                }
        }
    }

    // ---------------------------------------------------------------------
    // Filtros (#/partidos)
    // ---------------------------------------------------------------------
    fun setBusqueda(valor: String) = _uiState.update { it.copy(filtros = it.filtros.copy(busqueda = valor)) }
    fun setGrupo(valor: String) = _uiState.update { it.copy(filtros = it.filtros.copy(grupo = valor)) }
    fun setFase(valor: String) = _uiState.update { it.copy(filtros = it.filtros.copy(fase = valor)) }
    fun setEstadioId(valor: String) = _uiState.update { it.copy(filtros = it.filtros.copy(estadioId = valor)) }
    fun resetFiltros() = _uiState.update { it.copy(filtros = FiltrosPartidos()) }

    // ---------------------------------------------------------------------
    // Autenticación
    // ---------------------------------------------------------------------
    fun login(email: String, password: String, onResult: (Result<AuthResponseDto>) -> Unit) {
        viewModelScope.launch { onResult(authRepository.login(email, password)) }
    }

    fun registro(nombre: String, email: String, password: String, onResult: (Result<AuthResponseDto>) -> Unit) {
        viewModelScope.launch { onResult(authRepository.registro(nombre, email, password)) }
    }

    fun cerrarSesion() {
        viewModelScope.launch { authRepository.cerrarSesion() }
    }

    // ---------------------------------------------------------------------
    // Compra de entradas (equivalente a procesarCompra() en app.js)
    // ---------------------------------------------------------------------
    fun comprarEntradas(
        partidoId: String,
        categoria: String,
        cantidad: Int,
        onResult: (Result<ReservaResponseDto>) -> Unit,
    ) {
        viewModelScope.launch {
            val resultado = fifaRepository.comprarEntradas(partidoId, categoria, cantidad)
            resultado.onSuccess { reserva -> actualizarDisponibles(partidoId, categoria, reserva.disponibles) }
            onResult(resultado)
        }
    }

    /** Sincroniza el stock local con lo que confirmó la API, igual que app.js tras una compra. */
    private fun actualizarDisponibles(partidoId: String, categoria: String, disponibles: Int) {
        _uiState.update { state ->
            val nuevosPartidos = state.partidos.map { partido ->
                if (partido.partidoId != partidoId) return@map partido
                val nuevasEntradas = partido.entradas.map { cat: CategoriaEntradaDto ->
                    if (cat.categoria == categoria) cat.copy(disponibles = disponibles) else cat
                }
                partido.copy(entradas = nuevasEntradas)
            }
            state.copy(partidos = nuevosPartidos)
        }
    }

    fun partido(id: String?): PartidoDto? = uiState.value.partidos.find { it.partidoId == id }

    companion object {
        fun factory(
            fifaRepository: FifaRepository,
            authRepository: AuthRepository,
            sessionManager: SessionManager,
        ): ViewModelProvider.Factory = object : ViewModelProvider.Factory {
            @Suppress("UNCHECKED_CAST")
            override fun <T : ViewModel> create(modelClass: Class<T>): T {
                return AppViewModel(fifaRepository, authRepository, sessionManager) as T
            }
        }
    }
}
