package com.fifa2026.app

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import androidx.lifecycle.viewmodel.compose.viewModel
import com.fifa2026.app.ui.navigation.FifaApp
import com.fifa2026.app.ui.state.AppViewModel
import com.fifa2026.app.ui.theme.Fifa2026Theme

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()

        val container = (application as FifaApplication).container

        setContent {
            Fifa2026Theme {
                val viewModel: AppViewModel = viewModel(
                    factory = AppViewModel.factory(
                        fifaRepository = container.fifaRepository,
                        authRepository = container.authRepository,
                        sessionManager = container.sessionManager,
                    ),
                )
                FifaApp(viewModel = viewModel)
            }
        }
    }
}
