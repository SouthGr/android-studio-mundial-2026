# ⚽ FIFA 2026 — Sistema de Gestión de Entradas

> **Trabajo Práctico Nº1 | Técnicatura Universitaria en Programación**  
> Materia: Introducción al Desarrollo Web (o equivalente)

---

## 📋 Descripción del Proyecto

Aplicación web de página única (**SPA**) que simula un sistema de compra y gestión de entradas para el **Mundial FIFA 2026**, que se disputará en **Estados Unidos, Canadá y México**.

El usuario puede:
- Ver el listado completo de partidos con estado en tiempo real (finalizado / en juego / próximo)
- Filtrar partidos por grupo, fase, estadio o búsqueda libre
- Consultar el detalle de cada partido y disponibilidad de entradas por categoría
- Reservar entradas (la compra descuenta el stock en memoria y genera un código de reserva)
- Explorar los 8 estadios sede con información completa
- Navegar entre vistas sin recargar la página (router hash-based)

---

## 🏗️ Estructura del Proyecto

```
fifa2026/
│
├── index.html          # Shell HTML: navbar, <main id="app">, footer, modal, toasts
├── css/
│   └── styles.css      # Todos los estilos (mobile-first, FIFA navy + gold)
├── js/
│   ├── data.js         # Dataset: equipos, estadios, partidos con inventario simulado
│   └── app.js          # Lógica SPA: router, vistas, filtros, ticket flow
└── README.md           # Este archivo
```

---

## 🚀 Cómo ejecutar

### Opción 1 — Abrir directo en el navegador
```
Doble clic en index.html  →  Se abre en el navegador
```
> ⚠️ Algunos navegadores bloquean JS con `file://`. Usar la Opción 2 si hay problemas.

### Opción 2 — Servidor local con Python (recomendado)
```bash
cd fifa2026
python3 -m http.server 8080
# Luego abrir: http://localhost:8080
```

### Opción 3 — Extensión Live Server (VS Code)
1. Instalar la extensión **Live Server** en VS Code
2. Click derecho en `index.html` → *Open with Live Server*

---

## 💻 Tecnologías Utilizadas

| Tecnología | Uso |
|---|---|
| HTML5 | Estructura semántica (nav, main, footer, dialog) |
| CSS3 | Diseño responsive, variables CSS, animaciones, grid/flexbox |
| JavaScript ES6+ | DOM manipulation, router hash, módulo de datos, evento handling |
| Google Fonts | Oswald (títulos deportivos) + Inter (cuerpo de texto) |

**Sin frameworks, sin librerías externas, sin backend.**

---

## 🗺️ Navegación (Router Hash)

| Ruta | Vista |
|---|---|
| `#/` | Inicio — hero, stats, partidos destacados |
| `#/partidos` | Listado de todos los partidos con filtros |
| `#/partido/:id` | Detalle de partido + selector de entradas |
| `#/estadios` | Grilla de los 8 estadios sede |
| `#/estadio/:id` | Detalle de estadio + partidos en ese estadio |

---

## ⚙️ Funcionalidades Implementadas

### Manipulación del DOM
- Renderizado dinámico de todas las vistas con `innerHTML` y `createElement`
- Actualización del título del navegador según la ruta
- Marcado activo del ítem de navegación según la sección actual

### Datos Simulados
- **32 selecciones** con nombre y bandera emoji
- **8 estadios** (MetLife, Azteca, Rose Bowl, AT&T Stadium, SoFi, BBVA Monterrey, BC Place, Stade Olympique)
- **31 partidos** con estados realistas al 17 de junio de 2026:
  - Finalizados (Grupos A, B, C, D — Jornadas 1 y 2)
  - En juego (partidos del día)
  - Programados (próximos)
  - Por definir (Cuartos, Semis, Final)

### Sistema de Entradas
- Tres categorías por partido: **General**, **Preferencial**, **VIP**
- Stock inicial configurable, descuento en memoria al comprar
- Barra de disponibilidad con colores (verde / amarillo / rojo)
- Modal de confirmación con **código de reserva único** (`FIFA-XXXXXXXX`)

### UI/UX
- Diseño **mobile-first** totalmente responsive
- Menú hamburger en móvil
- Filtros combinables (búsqueda + grupo + fase + estadio)
- Estados vacíos informativos
- Toasts de notificación con auto-dismiss
- Skeleton loader en carga inicial
- Animación "pulsante" en partidos en vivo

---

## 📱 Compatibilidad

Probado en:
- ✅ Chrome / Chromium (Android y escritorio)
- ✅ Firefox
- ✅ Safari (iOS)
- ✅ Edge

---

## 📸 Capturas de Pantalla

*(Agregar screenshots del proyecto una vez corriendo)*

---

## 👤 Autor

**[Tu Nombre y Apellido]**  
Legajo: `[Tu Legajo]`  
TUP — Tecnicatura Universitaria en Programación  
2026

---

## 📄 Licencia

Proyecto académico — uso educativo exclusivamente.
