package com.fifa2026.app.ui.navigation

import androidx.compose.foundation.layout.padding
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.automirrored.filled.ArrowBack
import androidx.compose.material.icons.automirrored.filled.List
import androidx.compose.material.icons.filled.Home
import androidx.compose.material.icons.filled.Person
import androidx.compose.material.icons.filled.Place
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.material3.NavigationBar
import androidx.compose.material3.NavigationBarItem
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.material3.TopAppBar
import androidx.compose.material3.TopAppBarDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.ui.Modifier
import androidx.navigation.NavDestination.Companion.hierarchy
import androidx.navigation.NavGraph.Companion.findStartDestination
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.currentBackStackEntryAsState
import androidx.navigation.compose.rememberNavController
import androidx.navigation.navArgument
import com.fifa2026.app.ui.screens.auth.LoginScreen
import com.fifa2026.app.ui.screens.auth.RegistroScreen
import com.fifa2026.app.ui.screens.cuenta.CuentaScreen
import com.fifa2026.app.ui.screens.estadiodetalle.EstadioDetailScreen
import com.fifa2026.app.ui.screens.estadios.EstadiosScreen
import com.fifa2026.app.ui.screens.home.HomeScreen
import com.fifa2026.app.ui.screens.partidodetalle.PartidoDetailScreen
import com.fifa2026.app.ui.screens.partidos.PartidosScreen
import com.fifa2026.app.ui.state.AppViewModel

private data class BottomItem(val route: String, val label: String, val icon: androidx.compose.ui.graphics.vector.ImageVector)

private val bottomItems = listOf(
    BottomItem(Routes.HOME, "Inicio", Icons.Default.Home),
    BottomItem(Routes.PARTIDOS, "Partidos", Icons.AutoMirrored.Filled.List),
    BottomItem(Routes.ESTADIOS, "Estadios", Icons.Default.Place),
    BottomItem(Routes.CUENTA, "Cuenta", Icons.Default.Person),
)

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun FifaApp(viewModel: AppViewModel) {
    val navController = rememberNavController()
    val sesion by viewModel.sesion.collectAsState()
    val backStackEntry by navController.currentBackStackEntryAsState()
    val currentRoute = backStackEntry?.destination?.route
    val esRaiz = bottomItems.any { it.route == currentRoute }

    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text(tituloPara(currentRoute)) },
                navigationIcon = {
                    if (!esRaiz) {
                        IconButton(onClick = { navController.popBackStack() }) {
                            Icon(Icons.AutoMirrored.Filled.ArrowBack, contentDescription = "Volver")
                        }
                    }
                },
                colors = TopAppBarDefaults.topAppBarColors(),
            )
        },
        bottomBar = {
            NavigationBar {
                bottomItems.forEach { item ->
                    NavigationBarItem(
                        selected = backStackEntry?.destination?.hierarchy?.any { it.route == item.route } == true,
                        onClick = {
                            navController.navigate(item.route) {
                                popUpTo(navController.graph.findStartDestination().id) { saveState = true }
                                launchSingleTop = true
                                restoreState = true
                            }
                        },
                        icon = { Icon(item.icon, contentDescription = item.label) },
                        label = { Text(item.label) },
                    )
                }
            }
        },
    ) { padding ->
        NavHost(
            navController = navController,
            startDestination = Routes.HOME,
            modifier = Modifier.padding(padding),
        ) {
            composable(Routes.HOME) {
                HomeScreen(
                    viewModel = viewModel,
                    onVerPartidos = { navController.navigate(Routes.PARTIDOS) },
                    onVerEstadios = { navController.navigate(Routes.ESTADIOS) },
                    onPartidoClick = { id -> navController.navigate(Routes.partidoDetalle(id)) },
                )
            }
            composable(Routes.PARTIDOS) {
                PartidosScreen(
                    viewModel = viewModel,
                    onPartidoClick = { id -> navController.navigate(Routes.partidoDetalle(id)) },
                )
            }
            composable(
                route = Routes.PARTIDO_DETALLE,
                arguments = listOf(navArgument("partidoId") { type = androidx.navigation.NavType.StringType }),
            ) { backStack ->
                val partidoId = backStack.arguments?.getString("partidoId").orEmpty()
                PartidoDetailScreen(
                    viewModel = viewModel,
                    partidoId = partidoId,
                    sesion = sesion,
                    onEstadioClick = { id -> navController.navigate(Routes.estadioDetalle(id)) },
                    onLoginRequerido = { navController.navigate(Routes.LOGIN) },
                )
            }
            composable(Routes.ESTADIOS) {
                EstadiosScreen(
                    viewModel = viewModel,
                    onEstadioClick = { id -> navController.navigate(Routes.estadioDetalle(id)) },
                )
            }
            composable(
                route = Routes.ESTADIO_DETALLE,
                arguments = listOf(navArgument("estadioId") { type = androidx.navigation.NavType.StringType }),
            ) { backStack ->
                val estadioId = backStack.arguments?.getString("estadioId").orEmpty()
                EstadioDetailScreen(
                    viewModel = viewModel,
                    estadioId = estadioId,
                    onPartidoClick = { id -> navController.navigate(Routes.partidoDetalle(id)) },
                )
            }
            composable(Routes.LOGIN) {
                LoginScreen(
                    viewModel = viewModel,
                    onLoginExitoso = { navController.popBackStack() },
                    onIrARegistro = { navController.navigate(Routes.REGISTRO) },
                )
            }
            composable(Routes.REGISTRO) {
                RegistroScreen(
                    viewModel = viewModel,
                    onRegistroExitoso = { navController.popBackStack() },
                    onIrALogin = { navController.navigate(Routes.LOGIN) { popUpTo(Routes.LOGIN) { inclusive = true } } },
                )
            }
            composable(Routes.CUENTA) {
                CuentaScreen(
                    sesion = sesion,
                    onIrALogin = { navController.navigate(Routes.LOGIN) },
                    onIrARegistro = { navController.navigate(Routes.REGISTRO) },
                    onCerrarSesion = { viewModel.cerrarSesion() },
                )
            }
        }
    }
}

private fun tituloPara(route: String?): String = when {
    route == Routes.HOME -> "FIFA World Cup 2026"
    route == Routes.PARTIDOS -> "Partidos"
    route == Routes.ESTADIOS -> "Estadios"
    route == Routes.CUENTA -> "Mi Cuenta"
    route == Routes.LOGIN -> "Iniciar Sesión"
    route == Routes.REGISTRO -> "Crear Cuenta"
    route?.startsWith("partido/") == true -> "Detalle del Partido"
    route?.startsWith("estadio/") == true -> "Detalle del Estadio"
    else -> "FIFA 2026"
}
