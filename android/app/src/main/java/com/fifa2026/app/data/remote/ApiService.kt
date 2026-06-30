package com.fifa2026.app.data.remote

import com.fifa2026.app.data.model.AuthResponseDto
import com.fifa2026.app.data.model.EquipoDto
import com.fifa2026.app.data.model.EstadioDto
import com.fifa2026.app.data.model.LoginRequestDto
import com.fifa2026.app.data.model.PartidoDto
import com.fifa2026.app.data.model.RegistroRequestDto
import com.fifa2026.app.data.model.ReservaRequestDto
import com.fifa2026.app.data.model.ReservaResponseDto
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Path

/** Refleja 1 a 1 los endpoints de Fifa2026.Api (backend/Fifa2026.Api/Controllers). */
interface ApiService {

    @GET("equipos")
    suspend fun getEquipos(): Response<List<EquipoDto>>

    @GET("estadios")
    suspend fun getEstadios(): Response<List<EstadioDto>>

    @GET("estadios/{id}")
    suspend fun getEstadio(@Path("id") id: String): Response<EstadioDto>

    @GET("partidos")
    suspend fun getPartidos(): Response<List<PartidoDto>>

    @GET("partidos/{id}")
    suspend fun getPartido(@Path("id") id: String): Response<PartidoDto>

    @POST("auth/login")
    suspend fun login(@Body body: LoginRequestDto): Response<AuthResponseDto>

    @POST("auth/registro")
    suspend fun registro(@Body body: RegistroRequestDto): Response<AuthResponseDto>

    @POST("reservas")
    suspend fun crearReserva(@Body body: ReservaRequestDto): Response<ReservaResponseDto>
}
