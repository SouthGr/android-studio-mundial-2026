package com.fifa2026.app.ui.components

import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.size
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import coil.compose.AsyncImage

/** Igual que banderaEquipoImg()/banderaPaisImg() en app.js: imagen real de flagcdn.com, con emoji de respaldo. */
@Composable
fun FlagImage(url: String?, contentDescription: String?, modifier: Modifier = Modifier) {
    if (url == null) {
        Box(modifier = modifier.size(28.dp, 20.dp), contentAlignment = Alignment.Center) {
            Text("🏳️")
        }
    } else {
        AsyncImage(
            model = url,
            contentDescription = contentDescription,
            modifier = modifier.size(28.dp, 20.dp),
        )
    }
}
