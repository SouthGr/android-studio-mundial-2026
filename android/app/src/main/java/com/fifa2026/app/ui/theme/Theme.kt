package com.fifa2026.app.ui.theme

import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.lightColorScheme
import androidx.compose.runtime.Composable
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.platform.LocalView
import androidx.core.view.WindowCompat

private val LightColors = lightColorScheme(
    primary = FifaPrimary,
    onPrimary = Color.White,
    secondary = FifaAccent,
    onSecondary = FifaPrimaryDark,
    background = FifaBg,
    onBackground = FifaText,
    surface = FifaSurface,
    onSurface = FifaText,
    surfaceVariant = FifaSurface2,
    outline = FifaBorder,
    error = FifaDanger,
)

@Composable
fun Fifa2026Theme(content: @Composable () -> Unit) {
    val colorScheme = LightColors
    val view = LocalView.current
    if (!view.isInEditMode) {
        val activity = view.context as? android.app.Activity
        activity?.window?.let { window ->
            WindowCompat.getInsetsController(window, view).isAppearanceLightStatusBars = false
        }
    }

    MaterialTheme(
        colorScheme = colorScheme,
        typography = FifaTypography,
        content = content,
    )
}
