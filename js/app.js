// ============================================================
// FIFA WORLD CUP 2026 — Lógica de la aplicación
// SPA (Single Page Application) con router hash-based
// Datos servidos por la API .NET + SQL Server (ver /backend)
// TUP Inc.
// ============================================================

// ============================================================
// API
// ============================================================
const API_BASE = 'http://localhost:5110/api';

// ============================================================
// ESTADO GLOBAL
// ============================================================
const state = {
  filters: {
    grupo: 'all',
    fase: 'all',
    estadioId: 'all',
    busqueda: ''
  },
  selectedTicket: {
    partidoId: null,
    categoria: 'general',
    cantidad: 1
  },
  // Espejo local del stock devuelto por la API (se sincroniza tras cada compra)
  inventario: {},
  // Sesión del usuario logueado (null si es anónimo)
  auth: null
};

// Caché en memoria de los datos traídos de la API, con la misma forma
// que usaba el antiguo data.js para no tener que reescribir las vistas.
let DATA = { equipos: {}, estadios: [], partidos: [] };

/** Trae equipos, estadios y partidos de la API y los deja listos en DATA */
async function cargarDatos() {
  const [equiposRes, estadiosRes, partidosRes] = await Promise.all([
    fetch(`${API_BASE}/equipos`),
    fetch(`${API_BASE}/estadios`),
    fetch(`${API_BASE}/partidos`)
  ]);

  if (!equiposRes.ok || !estadiosRes.ok || !partidosRes.ok) {
    throw new Error('La API respondió con un error.');
  }

  const [equiposArr, estadiosArr, partidosArr] = await Promise.all([
    equiposRes.json(), estadiosRes.json(), partidosRes.json()
  ]);

  DATA.equipos = Object.fromEntries(
    equiposArr.map(e => [e.codigoFifa, { nombre: e.nombre, bandera: e.banderaEmoji }])
  );

  DATA.estadios = estadiosArr.map(e => ({
    id: e.estadioId, nombre: e.nombre, ciudad: e.ciudad, pais: e.pais,
    banderaPais: e.banderaPaisEmoji, capacidad: e.capacidad, superficie: e.superficie,
    inaugurado: e.inaugurado, descripcion: e.descripcion, datos: e.datos,
    imagen_emoji: e.imagenEmoji
  }));

  DATA.partidos = partidosArr.map(p => ({
    id: p.partidoId, fase: p.fase, grupo: p.grupo, jornada: p.jornada,
    fecha: p.fecha, hora: p.hora, estadioId: p.estadio.estadioId,
    localCod: p.local ? p.local.codigoFifa : null,
    visitanteCod: p.visitante ? p.visitante.codigoFifa : null,
    estado: p.estado, golesLocal: p.golesLocal, golesVisitante: p.golesVisitante,
    entradas: Object.fromEntries(
      p.entradas.map(c => [c.categoria, { precio: c.precio, disponibles: c.disponibles, total: c.total }])
    )
  }));

  state.inventario = {};
  DATA.partidos.forEach(p => {
    state.inventario[p.id] = {
      general:      p.entradas.general?.disponibles ?? 0,
      preferencial: p.entradas.preferencial?.disponibles ?? 0,
      vip:          p.entradas.vip?.disponibles ?? 0
    };
  });
}

// ============================================================
// AUTENTICACIÓN (JWT)
// ============================================================
const AUTH_STORAGE_KEY = 'fifa2026_sesion';

/** Carga la sesión guardada en localStorage (si existe) */
function cargarSesion() {
  try {
    const raw = localStorage.getItem(AUTH_STORAGE_KEY);
    if (!raw) return null;
    const sesion = JSON.parse(raw);
    if (!sesion.token || new Date(sesion.expira) <= new Date()) {
      localStorage.removeItem(AUTH_STORAGE_KEY);
      return null;
    }
    return sesion;
  } catch {
    return null;
  }
}

/** Guarda la sesión (respuesta de /api/auth/login o /registro) y actualiza el estado */
function guardarSesion(auth) {
  state.auth = auth;
  localStorage.setItem(AUTH_STORAGE_KEY, JSON.stringify(auth));
  renderNavAuth();
}

/** Cierra la sesión actual */
function cerrarSesion() {
  state.auth = null;
  localStorage.removeItem(AUTH_STORAGE_KEY);
  renderNavAuth();
  mostrarToast('Sesión cerrada.', 'info');
  navigate('#/');
}

function estaLogueado() { return !!state.auth?.token; }
function esAdmin() { return state.auth?.rol === 'Administrador'; }

/** Corta la navegación si el usuario no es administrador. Devuelve true si puede continuar. */
function requireAdmin() {
  if (esAdmin()) return true;
  mostrarToast(estaLogueado() ? 'Acceso restringido a administradores.' : 'Iniciá sesión como administrador para acceder.', 'error');
  navigate('#/');
  return false;
}

/** fetch con el header Authorization agregado; limpia la sesión si el token venció o es inválido */
async function fetchAuth(url, opts = {}) {
  const headers = { ...(opts.headers || {}) };
  if (state.auth?.token) headers['Authorization'] = `Bearer ${state.auth.token}`;

  const res = await fetch(url, { ...opts, headers });
  if (res.status === 401 && state.auth) {
    state.auth = null;
    localStorage.removeItem(AUTH_STORAGE_KEY);
    renderNavAuth();
    mostrarToast('Tu sesión venció. Iniciá sesión de nuevo.', 'error');
  }
  return res;
}

// ============================================================
// UTILIDADES
// ============================================================

/** Devuelve el objeto equipo por su código */
function getEquipo(cod) {
  if (!cod) return { nombre: 'Por Definir', bandera: '❓' };
  return DATA.equipos[cod] || { nombre: cod, bandera: '🏳' };
}

/** Devuelve el objeto estadio por su ID */
function getEstadio(id) {
  return DATA.estadios.find(e => e.id === id);
}

// Muchos navegadores (sobre todo Windows) no tienen fuente de emoji a color
// para las banderas y muestran las letras del código de país en su lugar.
// Por eso usamos imágenes de banderas reales (flagcdn.com) en vez de emoji Unicode.
const FIFA_TO_ISO = {
  MEX: 'mx', RSA: 'za', KOR: 'kr', CZE: 'cz',
  SUI: 'ch', CAN: 'ca', BIH: 'ba', QAT: 'qa',
  BRA: 'br', MAR: 'ma', SCO: 'gb-sct', HAI: 'ht',
  USA: 'us', AUS: 'au', PAR: 'py', TUR: 'tr',
  GER: 'de', CIV: 'ci', ECU: 'ec', CUW: 'cw',
  NED: 'nl', JAP: 'jp', SWE: 'se', TUN: 'tn',
  BEL: 'be', EGY: 'eg', IRN: 'ir', NZL: 'nz',
  ESP: 'es', CPV: 'cv', URU: 'uy', SAU: 'sa',
  FRA: 'fr', NOR: 'no', SEN: 'sn', IRQ: 'iq',
  ARG: 'ar', AUT: 'at', ALG: 'dz', JOR: 'jo',
  COL: 'co', POR: 'pt', COD: 'cd', UZB: 'uz',
  ENG: 'gb-eng', CRO: 'hr', GHA: 'gh', PAN: 'pa',
};

const PAIS_TO_ISO = { 'México': 'mx', 'Estados Unidos': 'us', 'Canadá': 'ca' };

/** <img> de bandera de un equipo a partir de su código FIFA */
function banderaEquipoImg(codigoFifa, alt, claseExtra) {
  const iso = FIFA_TO_ISO[codigoFifa];
  if (!iso) return '<span class="flag-img flag-img--tbd">🏳️</span>';
  return `<img class="flag-img${claseExtra ? ' ' + claseExtra : ''}" src="https://flagcdn.com/h60/${iso}.png" alt="${alt || codigoFifa}" loading="lazy">`;
}

/** <img> de bandera de un país (usado en estadios) */
function banderaPaisImg(pais, claseExtra) {
  const iso = PAIS_TO_ISO[pais];
  if (!iso) return '';
  return `<img class="flag-img${claseExtra ? ' ' + claseExtra : ''}" src="https://flagcdn.com/h60/${iso}.png" alt="${pais}" loading="lazy">`;
}

/** Formatea "2026-06-11" → "11 JUN 2026" */
function formatFecha(fechaStr) {
  if (!fechaStr) return '-';
  const meses = ['ENE','FEB','MAR','ABR','MAY','JUN','JUL','AGO','SEP','OCT','NOV','DIC'];
  const [anio, mes, dia] = fechaStr.split('-').map(Number);
  return `${dia} ${meses[mes - 1]} ${anio}`;
}

/** Formatea número con separadores de miles */
function formatNum(n) {
  return new Intl.NumberFormat('es-AR').format(n);
}

/** Calcula el % vendido global de un partido */
function calcPctVendido(partidoId) {
  const p = DATA.partidos.find(x => x.id === partidoId);
  if (!p) return 0;
  let total = 0, disponibles = 0;
  ['general', 'preferencial', 'vip'].forEach(cat => {
    total       += p.entradas[cat].total;
    disponibles += state.inventario[partidoId][cat];
  });
  const vendidos = total - disponibles;
  return Math.round((vendidos / total) * 100);
}

/** Devuelve clases y texto del badge según estado del partido */
function getBadgeEstado(estado) {
  const map = {
    programado:  { cls: 'badge--programado',  label: 'Programado' },
    en_juego:    { cls: 'badge--en-juego',    label: '🟢 En Juego' },
    finalizado:  { cls: 'badge--finalizado',  label: 'Finalizado' },
    por_definir: { cls: 'badge--por-definir', label: 'Por Definir' }
  };
  return map[estado] || map.por_definir;
}

// ============================================================
// ROUTER — Navegación hash-based
// ============================================================

/** Navega a un hash dado (ej: '#/partidos') */
function navigate(hash) {
  window.location.hash = hash;
}

/** Router principal: lee el hash y renderiza la vista */
function router() {
  const raw  = window.location.hash.replace('#', '');
  const path = raw || '/';
  const app  = document.getElementById('app');

  // Mostrar loading brevemente
  app.innerHTML = `
    <div style="display:flex;align-items:center;justify-content:center;height:50vh;">
      <div style="text-align:center;color:var(--c-text-muted)">
        <div style="font-size:2rem;margin-bottom:8px">⚽</div>
        <div>Cargando...</div>
      </div>
    </div>`;

  // Pequeño timeout para que el loader se vea (y simule fetch)
  setTimeout(() => {
    if (path === '/' || path === '') {
      renderHome();
      setNavActivo('home');
    } else if (path === '/partidos') {
      renderPartidos();
      setNavActivo('partidos');
    } else if (path.startsWith('/partido/')) {
      const id = path.split('/')[2];
      renderDetallePartido(id);
      setNavActivo('partidos');
    } else if (path === '/estadios') {
      renderEstadios();
      setNavActivo('estadios');
    } else if (path.startsWith('/estadio/')) {
      const id = path.split('/')[2];
      renderDetalleEstadio(id);
      setNavActivo('estadios');
    } else if (path === '/login') {
      renderLogin();
      setNavActivo('login');
    } else if (path === '/registro') {
      renderRegistro();
      setNavActivo('login');
    } else if (path === '/admin') {
      if (!requireAdmin()) return;
      renderAdminDashboard();
      setNavActivo('admin');
    } else if (path === '/admin/partidos') {
      if (!requireAdmin()) return;
      renderAdminPartidos();
      setNavActivo('admin');
    } else if (path === '/admin/estadios') {
      if (!requireAdmin()) return;
      renderAdminEstadios();
      setNavActivo('admin');
    } else {
      renderNoEncontrado();
    }

    window.scrollTo({ top: 0, behavior: 'smooth' });
    cerrarMenu();
  }, 80);
}

/** Marca el enlace de nav activo (desktop y menú móvil) */
function setNavActivo(key) {
  document.querySelectorAll('.navbar__link').forEach(a => a.classList.remove('navbar__link--active'));
  document.querySelectorAll('.navbar__menu-link').forEach(a => a.classList.remove('navbar__menu-link--active'));

  document.querySelectorAll(`[data-nav="${key}"]`).forEach(link => {
    link.classList.add(link.classList.contains('navbar__link') ? 'navbar__link--active' : 'navbar__menu-link--active');
  });
}

/** Pinta el estado de sesión (login/logout, link de admin) en la navbar */
function renderNavAuth() {
  const desktop = document.getElementById('navAuthDesktop');
  const mobile  = document.getElementById('navAuthMobile');
  if (!desktop || !mobile) return;

  if (!estaLogueado()) {
    desktop.innerHTML = `<a href="#/login" class="navbar__link" data-nav="login">Iniciar Sesión</a>`;
    mobile.innerHTML = `<a href="#/login" class="navbar__menu-link" data-nav="login" role="menuitem">🔐 Iniciar Sesión</a>`;
    return;
  }

  const adminLinkDesktop = esAdmin() ? `<a href="#/admin" class="navbar__link" data-nav="admin">⚙️ Admin</a>` : '';
  const adminLinkMobile  = esAdmin() ? `<a href="#/admin" class="navbar__menu-link" data-nav="admin" role="menuitem">⚙️ Admin</a>` : '';

  desktop.innerHTML = `
    ${adminLinkDesktop}
    <span class="navbar__user">👤 ${state.auth.nombre.split(' ')[0]}</span>
    <button class="navbar__link navbar__logout" onclick="cerrarSesion()">Salir</button>`;
  mobile.innerHTML = `
    ${adminLinkMobile}
    <div class="navbar__menu-link navbar__user--mobile">👤 ${state.auth.nombre}</div>
    <button class="navbar__menu-link navbar__logout" onclick="cerrarSesion()" role="menuitem">🚪 Cerrar sesión</button>`;
}

// ============================================================
// COMPONENTE: MATCH CARD
// ============================================================
function crearMatchCard(partido, darkMode = false) {
  const local     = getEquipo(partido.localCod);
  const visitante = getEquipo(partido.visitanteCod);
  const estadio   = getEstadio(partido.estadioId);
  const badge     = getBadgeEstado(partido.estado);
  const pct       = calcPctVendido(partido.id);

  const isLive      = partido.estado === 'en_juego';
  const isFinished  = partido.estado === 'finalizado';
  const isPorDef    = partido.estado === 'por_definir';
  const isAgotado   = pct >= 100;

  // Centro del marcador
  let centroHTML;
  if (isPorDef) {
    centroHTML = '';
  } else if (isFinished || isLive) {
    centroHTML = `<span class="match-score">${partido.golesLocal} - ${partido.golesVisitante}</span>`;
  } else {
    centroHTML = `<span class="match-vs">VS</span>`;
  }

  // Equipos
  let teamsHTML;
  if (isPorDef) {
    teamsHTML = `<div class="match-teams match-teams--tbd"><span class="match-team__tbd">🏆 Clasificados por determinar</span></div>`;
  } else {
    teamsHTML = `
      <div class="match-teams">
        <div class="match-team match-team--local">
          <span class="match-team__flag">${banderaEquipoImg(partido.localCod, local.nombre)}</span>
          <span class="match-team__name">${local.nombre}</span>
        </div>
        ${centroHTML}
        <div class="match-team match-team--visitante">
          <span class="match-team__name">${visitante.nombre}</span>
          <span class="match-team__flag">${banderaEquipoImg(partido.visitanteCod, visitante.nombre)}</span>
        </div>
      </div>`;
  }

  // Clase de disponibilidad
  let availCls = 'avail--ok';
  if (isAgotado)    availCls = 'avail--agotado';
  else if (pct >= 90) availCls = 'avail--critico';
  else if (pct >= 70) availCls = 'avail--bajo';

  const availTxt = isAgotado
    ? '🚫 Agotado'
    : pct >= 90
      ? `⚡ ${pct}% vendido — ¡Últimas entradas!`
      : `${pct}% vendido`;

  const grupoInfo  = partido.grupo    ? `GRUPO ${partido.grupo} · ` : '';
  const jornadaInfo = partido.jornada ? `JOR. ${partido.jornada} · ` : '';

  // Crear el elemento
  const card = document.createElement('article');
  card.className = `match-card${isLive ? ' match-card--live' : ''}`;
  card.setAttribute('role', 'button');
  card.setAttribute('tabindex', '0');
  card.setAttribute('aria-label', `${local.nombre} vs ${visitante.nombre}, ${formatFecha(partido.fecha)}`);

  card.innerHTML = `
    <div class="match-card__header">
      <div class="match-card__meta-top">
        <span class="match-label">${grupoInfo}${jornadaInfo}${partido.fase}</span>
        <span class="badge ${badge.cls}">${badge.label}</span>
      </div>
    </div>

    <div class="match-card__body">
      ${teamsHTML}
    </div>

    <div class="match-card__footer">
      <div class="match-card__info">
        <span class="match-card__date">📅 ${formatFecha(partido.fecha)} · ${partido.hora}</span>
        <span class="match-card__stadium">📍 ${estadio ? estadio.nombre : ''}, ${estadio ? estadio.ciudad : ''}</span>
      </div>
      <div class="match-card__availability ${availCls}">
        <div class="avail-bar">
          <div class="avail-bar__fill" style="width:${pct}%"></div>
        </div>
        <span class="avail-text">${availTxt}</span>
      </div>
    </div>

    <div class="match-card__actions">
      <button class="btn btn--outline btn--sm btn--full">Ver detalles →</button>
    </div>`;

  // Evento: click en la tarjeta o en el botón → navegar a detalle
  function goDetail(e) {
    navigate(`#/partido/${partido.id}`);
  }

  card.addEventListener('click', goDetail);
  card.addEventListener('keydown', e => { if (e.key === 'Enter' || e.key === ' ') goDetail(e); });

  return card;
}

// ============================================================
// VISTA: HOME
// ============================================================
function renderHome() {
  const app = document.getElementById('app');

  // Fechas importantes
  const inicio = new Date('2026-06-11');
  const hoy    = new Date();
  const diff   = Math.floor((hoy - inicio) / 86400000);

  let estadoMundial;
  if (diff < 0) {
    estadoMundial = `⏳ Faltan <strong>${Math.abs(diff)} días</strong> para el inicio`;
  } else if (diff <= 39) {
    estadoMundial = `🏆 Día <strong>${diff + 1}</strong> del Mundial · En curso`;
  } else {
    estadoMundial = `🏆 Mundial 2026 Finalizado`;
  }

  app.innerHTML = `
    <!-- HERO -->
    <section class="hero">
      <div class="container">
        <div class="hero__inner">
          <div class="hero__content">
            <div class="hero__eyebrow">
              <span class="live-dot"></span>
              Sistema Oficial de Entradas
            </div>
            <h1 class="hero__title">
              FIFA<br>
              <span class="gold">World Cup</span><br>
              2026™
            </h1>
            <p class="hero__subtitle">
              Adquirí tus entradas para los partidos del Mundial de Fútbol más grande de la historia. 48 equipos, 3 países, 104 partidos.
            </p>
            <div class="hero__ctas">
              <button class="btn btn--primary btn--lg" onclick="navigate('#/partidos')">
                🎟️ Ver Partidos
              </button>
              <button class="btn btn--white btn--lg" onclick="navigate('#/estadios')">
                🏟️ Estadios
              </button>
            </div>
          </div>

          <div class="hero__stats">
            <div class="stat-item">
              <div class="stat-item__value">48</div>
              <div class="stat-item__label">Equipos</div>
            </div>
            <div class="stat-item">
              <div class="stat-item__value">104</div>
              <div class="stat-item__label">Partidos</div>
            </div>
            <div class="stat-item">
              <div class="stat-item__value">16</div>
              <div class="stat-item__label">Estadios</div>
            </div>
            <div class="stat-item">
              <div class="stat-item__value">3</div>
              <div class="stat-item__label">Países sede</div>
            </div>
          </div>
        </div>
      </div>
      <div class="hero__ticker">${estadoMundial} · EE.UU. · México · Canadá</div>
    </section>

    <!-- PARTIDOS DE HOY / PRÓXIMOS -->
    <section class="featured-section" id="featured"></section>

    <!-- SECCIÓN RÁPIDA -->
    <section class="section">
      <div class="container">
        <div class="section__header">
          <div>
            <h2 class="section__title">Todas las Fases del Torneo</h2>
            <p class="section__subtitle">Explorá partidos por fase y encontrá tus entradas</p>
          </div>
        </div>
        <div id="fase-cards" style="display:grid;gap:12px;grid-template-columns:repeat(auto-fill,minmax(200px,1fr))"></div>
      </div>
    </section>
  `;

  // Partidos destacados: en_juego primero, luego próximos programados
  const destacados = [
    ...DATA.partidos.filter(p => p.estado === 'en_juego'),
    ...DATA.partidos.filter(p => p.estado === 'programado').slice(0, 6)
  ].slice(0, 6);

  const featured = document.getElementById('featured');
  if (destacados.length > 0) {
    featured.innerHTML = `
      <div class="container">
        <div class="section__header" style="margin-bottom:28px">
          <div>
            <h2 class="section__title" style="color:#fff">
              ${DATA.partidos.some(p => p.estado === 'en_juego') ? '🔴 En Juego Ahora' : '📅 Próximos Partidos'}
            </h2>
            <p class="section__subtitle">No te pierdas el partido</p>
          </div>
          <button class="btn btn--primary" onclick="navigate('#/partidos')">Ver todos →</button>
        </div>
        <div class="matches-grid" id="featured-grid"></div>
      </div>`;
    const grid = document.getElementById('featured-grid');
    destacados.forEach(p => grid.appendChild(crearMatchCard(p, true)));
  }

  // Cards de fases
  const fases = [...new Set(DATA.partidos.map(p => p.fase))];
  const faseIcons = {
    'Fase de Grupos': '⚽',
    'Dieciseisavos de Final': '🎯',
    'Octavos de Final': '🏅',
    'Cuartos de Final': '🥉',
    'Semifinal': '🥈',
    'Tercer Puesto': '🥉',
    'Gran Final': '🏆'
  };
  const faseContainer = document.getElementById('fase-cards');
  fases.forEach(fase => {
    const count = DATA.partidos.filter(p => p.fase === fase).length;
    const div   = document.createElement('div');
    div.className = 'info-card';
    div.style.cursor = 'pointer';
    div.style.transition = 'transform .2s, box-shadow .2s';
    div.innerHTML = `
      <div style="font-size:1.8rem;margin-bottom:8px">${faseIcons[fase] || '🏟️'}</div>
      <div style="font-family:var(--font-display);font-weight:600;font-size:.95rem;color:var(--c-primary);margin-bottom:4px">${fase}</div>
      <div style="font-size:.8rem;color:var(--c-text-muted)">${count} partido${count !== 1 ? 's' : ''}</div>
    `;
    div.addEventListener('mouseenter', () => { div.style.transform = 'translateY(-3px)'; div.style.boxShadow = 'var(--shadow-lg)'; });
    div.addEventListener('mouseleave', () => { div.style.transform = ''; div.style.boxShadow = ''; });
    div.addEventListener('click', () => navigate(`#/partidos`));
    faseContainer.appendChild(div);
  });
}

// ============================================================
// VISTA: LISTADO DE PARTIDOS
// ============================================================
function renderPartidos() {
  const app = document.getElementById('app');

  const grupos  = [...new Set(DATA.partidos.filter(p => p.grupo).map(p => p.grupo))].sort();
  const fases   = [...new Set(DATA.partidos.map(p => p.fase))];
  const estadios = DATA.estadios;

  app.innerHTML = `
    <section class="section">
      <div class="container">
        <div class="section__header">
          <div>
            <h1 class="section__title">Partidos</h1>
            <p class="section__subtitle">
              Mostrando <strong id="count-visible">${DATA.partidos.length}</strong>
              de ${DATA.partidos.length} partidos
            </p>
          </div>
        </div>

        <!-- FILTROS -->
        <div class="filters">
          <div class="filter-group">
            <label for="f-busqueda">🔍 Buscar equipo</label>
            <input type="text" id="f-busqueda" placeholder="Ej: Argentina, Brasil..." value="${state.filters.busqueda}">
          </div>
          <div class="filter-group">
            <label for="f-grupo">Grupo</label>
            <select id="f-grupo">
              <option value="all">Todos los grupos</option>
              ${grupos.map(g => `<option value="${g}" ${state.filters.grupo === g ? 'selected' : ''}>Grupo ${g}</option>`).join('')}
            </select>
          </div>
          <div class="filter-group">
            <label for="f-fase">Fase</label>
            <select id="f-fase">
              <option value="all">Todas las fases</option>
              ${fases.map(f => `<option value="${f}" ${state.filters.fase === f ? 'selected' : ''}>${f}</option>`).join('')}
            </select>
          </div>
          <div class="filter-group">
            <label for="f-estadio">Estadio</label>
            <select id="f-estadio">
              <option value="all">Todos los estadios</option>
              ${estadios.map(e => `<option value="${e.id}" ${state.filters.estadioId === e.id ? 'selected' : ''}>${e.nombre}</option>`).join('')}
            </select>
          </div>
          <button class="btn btn--ghost filters__reset" onclick="resetFiltros()">✕ Limpiar</button>
        </div>

        <!-- GRID DE PARTIDOS -->
        <div class="matches-grid" id="partidos-grid"></div>

        <!-- ESTADO VACÍO -->
        <div id="empty-state" style="display:none" class="empty-state">
          <div class="empty-state__icon">🔍</div>
          <div class="empty-state__title">No se encontraron partidos</div>
          <div class="empty-state__desc">Intentá con otros filtros de búsqueda</div>
          <br>
          <button class="btn btn--outline" onclick="resetFiltros()">Limpiar filtros</button>
        </div>
      </div>
    </section>`;

  // Conectar eventos de filtros
  document.getElementById('f-busqueda').addEventListener('input', e => {
    state.filters.busqueda = e.target.value;
    aplicarFiltros();
  });
  document.getElementById('f-grupo').addEventListener('change', e => {
    state.filters.grupo = e.target.value;
    aplicarFiltros();
  });
  document.getElementById('f-fase').addEventListener('change', e => {
    state.filters.fase = e.target.value;
    aplicarFiltros();
  });
  document.getElementById('f-estadio').addEventListener('change', e => {
    state.filters.estadioId = e.target.value;
    aplicarFiltros();
  });

  // Renderizado inicial
  aplicarFiltros();
}

/** Filtra y renderiza las tarjetas de partidos */
function aplicarFiltros() {
  const { grupo, fase, estadioId, busqueda } = state.filters;
  const query = busqueda.toLowerCase().trim();

  const filtrados = DATA.partidos.filter(p => {
    // Filtro grupo
    if (grupo !== 'all' && p.grupo !== grupo) return false;
    // Filtro fase
    if (fase !== 'all' && p.fase !== fase) return false;
    // Filtro estadio
    if (estadioId !== 'all' && p.estadioId !== estadioId) return false;
    // Búsqueda de texto
    if (query) {
      const local     = getEquipo(p.localCod).nombre.toLowerCase();
      const visitante = getEquipo(p.visitanteCod).nombre.toLowerCase();
      const estadio   = (getEstadio(p.estadioId) || {}).nombre || '';
      if (!local.includes(query) && !visitante.includes(query) && !estadio.toLowerCase().includes(query)) return false;
    }
    return true;
  });

  const grid  = document.getElementById('partidos-grid');
  const empty = document.getElementById('empty-state');
  const count = document.getElementById('count-visible');

  grid.innerHTML = '';
  if (count) count.textContent = filtrados.length;

  if (filtrados.length === 0) {
    grid.style.display = 'none';
    empty.style.display = 'block';
  } else {
    grid.style.display = 'grid';
    empty.style.display = 'none';
    filtrados.forEach(p => grid.appendChild(crearMatchCard(p)));
  }
}

/** Resetea todos los filtros */
function resetFiltros() {
  state.filters = { grupo: 'all', fase: 'all', estadioId: 'all', busqueda: '' };
  renderPartidos();
}

// ============================================================
// VISTA: DETALLE DE PARTIDO
// ============================================================
function renderDetallePartido(id) {
  const app     = document.getElementById('app');
  const partido = DATA.partidos.find(p => p.id === id);

  if (!partido) { renderNoEncontrado(); return; }

  const local     = getEquipo(partido.localCod);
  const visitante = getEquipo(partido.visitanteCod);
  const estadio   = getEstadio(partido.estadioId);
  const badge     = getBadgeEstado(partido.estado);

  const isPorDef   = partido.estado === 'por_definir';
  const isFinished = partido.estado === 'finalizado';
  const isLive     = partido.estado === 'en_juego';

  // Centro del marcador en el hero
  let centroHero;
  if (isPorDef) {
    centroHero = `<div class="detail-vs">VS</div>`;
  } else if (isFinished || isLive) {
    centroHero = `<div class="detail-scoreboard">${partido.golesLocal} – ${partido.golesVisitante}</div>`;
  } else {
    centroHero = `
      <div class="detail-vs">VS</div>
      <div style="font-size:.82rem;color:rgba(255,255,255,.5);margin-top:4px">${partido.hora} hs</div>`;
  }

  // Equipos del hero
  let teamsHero;
  if (isPorDef) {
    teamsHero = `
      <div class="detail-teams">
        <div style="font-size:3rem;text-align:center;flex:1">🏆<br><small style="font-size:.75rem;color:rgba(255,255,255,.5)">Clasificado</small></div>
        <div class="detail-center">${centroHero}</div>
        <div style="font-size:3rem;text-align:center;flex:1">🏆<br><small style="font-size:.75rem;color:rgba(255,255,255,.5)">Clasificado</small></div>
      </div>`;
  } else {
    teamsHero = `
      <div class="detail-teams">
        <div class="detail-team">
          <div class="detail-team__flag">${banderaEquipoImg(partido.localCod, local.nombre)}</div>
          <div class="detail-team__name">${local.nombre}</div>
        </div>
        <div class="detail-center">${centroHero}</div>
        <div class="detail-team">
          <div class="detail-team__flag">${banderaEquipoImg(partido.visitanteCod, visitante.nombre)}</div>
          <div class="detail-team__name">${visitante.nombre}</div>
        </div>
      </div>`;
  }

  const grupoStr = partido.grupo ? `Grupo ${partido.grupo} · ` : '';

  app.innerHTML = `
    <!-- HERO DEL PARTIDO -->
    <div class="detail-hero">
      <div class="container">
        <button class="back-btn" onclick="navigate('#/partidos')">← Volver a Partidos</button>

        <div class="detail-hero__badge-row">
          <span class="badge ${badge.cls}">${badge.label}</span>
          <span class="badge badge--grupo">${grupoStr}${partido.fase}</span>
          ${partido.jornada ? `<span class="badge badge--grupo">Jornada ${partido.jornada}</span>` : ''}
        </div>

        ${teamsHero}

        <div class="detail-hero__meta">
          <div class="detail-meta-item">📅 ${formatFecha(partido.fecha)}</div>
          <div class="detail-meta-item">🕐 ${partido.hora} hs</div>
          <div class="detail-meta-item">📍 ${estadio ? estadio.nombre : ''}</div>
          <div class="detail-meta-item">🏙 ${estadio ? estadio.ciudad + ', ' + estadio.pais : ''}</div>
        </div>
      </div>
    </div>

    <!-- CONTENIDO PRINCIPAL -->
    <div class="container">
      <div class="detail-layout">

        <!-- COLUMNA PRINCIPAL -->
        <div>
          ${isFinished ? `
            <div class="info-card" style="margin-bottom:24px;border-left:4px solid var(--c-accent)">
              <div style="font-family:var(--font-display);font-weight:700;color:var(--c-primary);font-size:1rem;margin-bottom:8px">
                ✅ Partido Finalizado
              </div>
              <p style="font-size:.9rem;color:var(--c-text-sec)">
                Este partido ya se disputó. El resultado final fue
                <strong>${local.nombre} ${partido.golesLocal} – ${partido.golesVisitante} ${visitante.nombre}</strong>.
              </p>
            </div>` : ''}

          ${isLive ? `
            <div class="info-card" style="margin-bottom:24px;border-left:4px solid var(--c-live)">
              <div style="font-family:var(--font-display);font-weight:700;color:var(--c-live);font-size:1rem;margin-bottom:8px;animation:blink 1s infinite">
                🔴 PARTIDO EN VIVO
              </div>
              <p style="font-size:.9rem;color:var(--c-text-sec)">
                El partido está en curso. Marcador actual:
                <strong>${local.nombre} ${partido.golesLocal} – ${partido.golesVisitante} ${visitante.nombre}</strong>.
              </p>
            </div>` : ''}

          <!-- INFO DEL ESTADIO EN DETALLE -->
          ${estadio ? `
            <div class="info-card" style="margin-bottom:24px;cursor:pointer"
              onclick="navigate('#/estadio/${estadio.id}')">
              <div class="info-card__title">🏟️ Estadio</div>
              <div style="display:flex;align-items:flex-start;gap:16px;flex-wrap:wrap">
                <div style="font-size:3rem">${estadio.imagen_emoji}</div>
                <div style="flex:1">
                  <div style="font-family:var(--font-display);font-size:1.1rem;font-weight:600;color:var(--c-primary)">${estadio.nombre}</div>
                  <div style="font-size:.85rem;color:var(--c-text-sec);margin-top:4px">📍 ${estadio.ciudad}, ${estadio.pais} ${banderaPaisImg(estadio.pais, 'flag-img--inline')}</div>
                  <div style="display:flex;gap:16px;margin-top:12px;flex-wrap:wrap">
                    <div class="stadium-stat">
                      <div class="stadium-stat__value">${formatNum(estadio.capacidad)}</div>
                      <div class="stadium-stat__label">Capacidad</div>
                    </div>
                    <div class="stadium-stat">
                      <div class="stadium-stat__value">${estadio.inaugurado}</div>
                      <div class="stadium-stat__label">Inaugurado</div>
                    </div>
                    <div class="stadium-stat">
                      <div class="stadium-stat__value">${estadio.superficie}</div>
                      <div class="stadium-stat__label">Superficie</div>
                    </div>
                  </div>
                  <p style="font-size:.82rem;color:var(--c-text-sec);margin-top:10px;line-height:1.5">${estadio.descripcion.substring(0, 140)}…</p>
                  <div style="font-size:.8rem;color:var(--c-primary);margin-top:8px;font-weight:500">Ver más sobre el estadio →</div>
                </div>
              </div>
            </div>` : ''}
        </div>

        <!-- SIDEBAR: COMPRA DE ENTRADAS -->
        <div class="detail-sidebar">
          <div class="ticket-section">
            <div class="ticket-section__title">🎟️ Comprar Entradas</div>

            ${isFinished ? `
              <div style="text-align:center;padding:24px;color:var(--c-text-muted)">
                <div style="font-size:2rem;margin-bottom:8px">⛔</div>
                <p style="font-size:.9rem">La venta de entradas para este partido ya ha cerrado.</p>
              </div>` : !estaLogueado() ? `
              <div style="text-align:center;padding:24px;color:var(--c-text-muted)">
                <div style="font-size:2rem;margin-bottom:8px">🔐</div>
                <p style="font-size:.9rem;margin-bottom:16px">Iniciá sesión para comprar tus entradas.</p>
                <a class="btn btn--primary btn--full" href="#/login">Iniciar Sesión</a>
                <p style="font-size:.78rem;margin-top:10px">¿No tenés cuenta? <a href="#/registro" style="color:var(--c-primary);font-weight:600">Registrate</a></p>
              </div>` : `
              <!-- CATEGORÍAS -->
              <div class="ticket-categories" id="ticket-cats"></div>

              <!-- CANTIDAD -->
              <div class="ticket-qty-row">
                <label>Cantidad de entradas</label>
                <div class="qty-control">
                  <button id="qty-menos" onclick="cambiarCantidad(-1)">−</button>
                  <span id="qty-value">1</span>
                  <button id="qty-mas" onclick="cambiarCantidad(1)">+</button>
                </div>
              </div>

              <!-- TOTAL -->
              <div class="ticket-total">
                <div>
                  <div class="ticket-total__label">Total a pagar</div>
                  <div style="font-size:.75rem;color:rgba(255,255,255,.5);margin-top:2px">USD · Precio final</div>
                </div>
                <div class="ticket-total__amount" id="ticket-total">USD 0</div>
              </div>

              <button class="btn btn--primary btn--full btn--lg" id="btn-comprar" onclick="abrirModalPago('${partido.id}')">
                🎟️ Comprar Ahora
              </button>

              <p style="font-size:.72rem;color:var(--c-text-muted);text-align:center;margin-top:12px">
                ✓ Pago seguro · ✓ Entradas digitales · ✓ Transferible
              </p>`}
          </div>
        </div>
      </div>
    </div>`;

  // Inicializar selector de entradas si el partido no finalizó y hay sesión activa
  if (!isFinished && estaLogueado()) {
    state.selectedTicket.partidoId = partido.id;
    state.selectedTicket.cantidad  = 1;
    state.selectedTicket.categoria = 'general';
    inicializarSelectorEntradas(partido);
  }
}

/** Crea las opciones de categoría de entradas */
function inicializarSelectorEntradas(partido) {
  const container = document.getElementById('ticket-cats');
  if (!container) return;

  const categorias = [
    { id: 'general',      nombre: 'General',      desc: 'Acceso a todas las zonas generales' },
    { id: 'preferencial', nombre: 'Preferencial',  desc: 'Ubicación preferencial, mejor visibilidad' },
    { id: 'vip',          nombre: 'VIP',           desc: 'Acceso VIP con servicios premium' }
  ];

  container.innerHTML = '';

  categorias.forEach((cat, idx) => {
    const precio     = partido.entradas[cat.id].precio;
    const disponibles = state.inventario[partido.id][cat.id];
    const agotado    = disponibles === 0;

    const div = document.createElement('label');
    div.className = `ticket-cat${idx === 0 && !agotado ? ' is-selected' : ''}${agotado ? ' is-agotado' : ''}`;

    div.innerHTML = `
      <input type="radio" name="ticket-cat" value="${cat.id}"
        ${idx === 0 && !agotado ? 'checked' : ''}
        ${agotado ? 'disabled' : ''}>
      <div class="ticket-cat__info">
        <div class="ticket-cat__name">${cat.nombre}</div>
        <div class="ticket-cat__avail ${disponibles < 100 && !agotado ? 'low' : ''}">
          ${agotado ? 'AGOTADO' : `${formatNum(disponibles)} disponibles`}
        </div>
      </div>
      <div class="ticket-cat__price">USD ${precio}</div>
      ${agotado ? '<span class="ticket-cat__agotado-tag">AGOTADO</span>' : ''}`;

    if (!agotado) {
      div.querySelector('input').addEventListener('change', e => {
        document.querySelectorAll('.ticket-cat').forEach(el => el.classList.remove('is-selected'));
        div.classList.add('is-selected');
        state.selectedTicket.categoria = e.target.value;
        state.selectedTicket.cantidad  = 1;
        document.getElementById('qty-value').textContent = '1';
        actualizarTotal(partido);
      });
    }

    container.appendChild(div);
  });

  // Si la categoría por defecto está agotada, seleccionar la primera disponible
  const primeraDisp = categorias.find(c => state.inventario[partido.id][c.id] > 0);
  if (primeraDisp) {
    state.selectedTicket.categoria = primeraDisp.id;
    const radios = document.querySelectorAll('input[name="ticket-cat"]');
    radios.forEach(r => {
      if (r.value === primeraDisp.id) {
        r.checked = true;
        r.closest('.ticket-cat').classList.add('is-selected');
      }
    });
  }

  actualizarTotal(partido);
}

/** Cambia la cantidad de entradas (+/-) */
function cambiarCantidad(delta) {
  const partido = DATA.partidos.find(p => p.id === state.selectedTicket.partidoId);
  if (!partido) return;

  const maxDisp = state.inventario[partido.id][state.selectedTicket.categoria];
  const actual  = state.selectedTicket.cantidad;
  const nueva   = Math.max(1, Math.min(maxDisp, actual + delta));

  state.selectedTicket.cantidad = nueva;
  document.getElementById('qty-value').textContent = nueva;

  document.getElementById('qty-menos').disabled = nueva <= 1;
  document.getElementById('qty-mas').disabled   = nueva >= Math.min(maxDisp, 10);

  actualizarTotal(partido);
}

/** Actualiza el total mostrado */
function actualizarTotal(partido) {
  const precio   = partido.entradas[state.selectedTicket.categoria].precio;
  const cantidad = state.selectedTicket.cantidad;
  const total    = precio * cantidad;

  const totalEl = document.getElementById('ticket-total');
  if (totalEl) totalEl.textContent = `USD ${formatNum(total)}`;

  const menos = document.getElementById('qty-menos');
  const mas   = document.getElementById('qty-mas');
  if (menos) menos.disabled = cantidad <= 1;
  if (mas)   mas.disabled   = cantidad >= Math.min(10, state.inventario[partido.id][state.selectedTicket.categoria]);
}

/** Algoritmo de Luhn — validación de formato de número de tarjeta (no verifica fondos ni nada real) */
function pasaLuhn(numero) {
  let suma = 0, alternar = false;
  for (let i = numero.length - 1; i >= 0; i--) {
    let digito = parseInt(numero[i], 10);
    if (alternar) { digito *= 2; if (digito > 9) digito -= 9; }
    suma += digito;
    alternar = !alternar;
  }
  return suma % 10 === 0;
}

function detectarMarcaTarjeta(numero) {
  if (/^4/.test(numero)) return '💳 Visa';
  if (/^5[1-5]/.test(numero)) return '💳 Mastercard';
  if (/^3[47]/.test(numero)) return '💳 American Express';
  return '💳 Tarjeta';
}

/** Abre el modal de pago con tarjeta. Si el formulario valida, dispara procesarCompra(). */
function abrirModalPago(partidoId) {
  const partido = DATA.partidos.find(p => p.id === partidoId);
  if (!partido) return;

  const { categoria, cantidad } = state.selectedTicket;
  const disponibles = state.inventario[partidoId][categoria];
  if (cantidad > disponibles) {
    mostrarToast(`Solo quedan ${disponibles} entradas disponibles en esa categoría.`, 'error');
    return;
  }

  const precio = partido.entradas[categoria].precio;
  const total  = precio * cantidad;

  abrirModal(`
    <div class="modal__icon" style="font-size:2.2rem">💳</div>
    <div class="modal__title" style="margin-bottom:4px">Pago con Tarjeta</div>
    <p style="font-size:.85rem;color:var(--c-text-sec);margin-bottom:20px">
      Total a pagar: <strong style="color:var(--c-primary)">USD ${formatNum(total)}</strong>
      (${cantidad} entrada${cantidad !== 1 ? 's' : ''} · ${categoria})
    </p>

    <form id="form-pago" class="auth-form" style="text-align:left">
      <div class="filter-group">
        <label for="pago-titular">Nombre del titular</label>
        <input type="text" id="pago-titular" placeholder="Como figura en la tarjeta" required autocomplete="cc-name">
      </div>
      <div class="filter-group">
        <label for="pago-numero">Número de tarjeta <span id="pago-marca" class="card-brand"></span></label>
        <input type="text" id="pago-numero" inputmode="numeric" placeholder="0000 0000 0000 0000" maxlength="19" required autocomplete="cc-number">
      </div>
      <div style="display:flex;gap:10px">
        <div class="filter-group">
          <label for="pago-venc">Vencimiento</label>
          <input type="text" id="pago-venc" inputmode="numeric" placeholder="MM/AA" maxlength="5" required autocomplete="cc-exp">
        </div>
        <div class="filter-group">
          <label for="pago-cvv">CVV</label>
          <input type="text" id="pago-cvv" inputmode="numeric" placeholder="123" maxlength="4" required autocomplete="cc-csc">
        </div>
      </div>

      <div id="pago-error" class="auth-form__error" style="display:none"></div>

      <div style="display:flex;gap:10px;margin-top:8px">
        <button type="button" class="btn btn--ghost btn--full" onclick="cerrarModal()">Cancelar</button>
        <button type="submit" class="btn btn--primary btn--full" id="pago-submit">🔒 Pagar USD ${formatNum(total)}</button>
      </div>
      <p style="font-size:.7rem;color:var(--c-text-muted);text-align:center;margin-top:12px">
        Simulación de pago con fines académicos. No se almacenan datos de tarjeta.
      </p>
    </form>`);

  const numeroInput = document.getElementById('pago-numero');
  const marcaEl = document.getElementById('pago-marca');
  numeroInput.addEventListener('input', () => {
    const limpio = numeroInput.value.replace(/\D/g, '').slice(0, 19);
    numeroInput.value = limpio.replace(/(.{4})/g, '$1 ').trim();
    marcaEl.textContent = limpio ? detectarMarcaTarjeta(limpio) : '';
  });

  const vencInput = document.getElementById('pago-venc');
  vencInput.addEventListener('input', () => {
    let limpio = vencInput.value.replace(/\D/g, '').slice(0, 4);
    if (limpio.length >= 3) limpio = limpio.slice(0, 2) + '/' + limpio.slice(2);
    vencInput.value = limpio;
  });

  document.getElementById('pago-cvv').addEventListener('input', e => {
    e.target.value = e.target.value.replace(/\D/g, '').slice(0, 4);
  });

  document.getElementById('form-pago').addEventListener('submit', e => {
    e.preventDefault();
    const errorEl = document.getElementById('pago-error');
    errorEl.style.display = 'none';

    const titular = document.getElementById('pago-titular').value.trim();
    const numero  = document.getElementById('pago-numero').value.replace(/\s/g, '');
    const venc    = document.getElementById('pago-venc').value.trim();
    const cvv     = document.getElementById('pago-cvv').value.trim();

    if (titular.length < 3) {
      errorEl.textContent = 'Ingresá el nombre del titular tal como figura en la tarjeta.';
      errorEl.style.display = 'block';
      return;
    }
    if (numero.length < 13 || numero.length > 19 || !pasaLuhn(numero)) {
      errorEl.textContent = 'El número de tarjeta no es válido.';
      errorEl.style.display = 'block';
      return;
    }
    const matchVenc = venc.match(/^(\d{2})\/(\d{2})$/);
    if (!matchVenc) {
      errorEl.textContent = 'Vencimiento inválido. Usá el formato MM/AA.';
      errorEl.style.display = 'block';
      return;
    }
    const mes = parseInt(matchVenc[1], 10);
    const anio = 2000 + parseInt(matchVenc[2], 10);
    const hoy = new Date();
    const finMesVenc = new Date(anio, mes, 0); // último día del mes de vencimiento
    if (mes < 1 || mes > 12 || finMesVenc < hoy) {
      errorEl.textContent = 'La tarjeta está vencida o la fecha es inválida.';
      errorEl.style.display = 'block';
      return;
    }
    if (cvv.length < 3 || cvv.length > 4) {
      errorEl.textContent = 'El CVV debe tener 3 o 4 dígitos.';
      errorEl.style.display = 'block';
      return;
    }

    cerrarModal();
    procesarCompra(partidoId);
  });
}

/** Procesa la compra de entradas contra la API */
async function procesarCompra(partidoId) {
  if (!estaLogueado()) {
    mostrarToast('Iniciá sesión para comprar entradas.', 'error');
    navigate('#/login');
    return;
  }

  const partido = DATA.partidos.find(p => p.id === partidoId);
  if (!partido) return;

  const { categoria, cantidad } = state.selectedTicket;
  const disponibles = state.inventario[partidoId][categoria];

  if (cantidad > disponibles) {
    mostrarToast(`Solo quedan ${disponibles} entradas disponibles en esa categoría.`, 'error');
    return;
  }
  if (cantidad < 1) {
    mostrarToast('Seleccioná al menos 1 entrada.', 'error');
    return;
  }

  const btnComprar = document.getElementById('btn-comprar');
  if (btnComprar) { btnComprar.disabled = true; btnComprar.textContent = 'Procesando...'; }

  let data;
  try {
    const res = await fetchAuth(`${API_BASE}/reservas`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ partidoId, categoria, cantidad })
    });
    if (res.status === 401) {
      navigate('#/login');
      return;
    }
    data = await res.json().catch(() => ({}));
    if (!res.ok) {
      mostrarToast(data.mensaje || 'No se pudo procesar la compra.', 'error');
      return;
    }
  } catch (err) {
    mostrarToast('No se pudo conectar con el servidor. ¿Está corriendo la API?', 'error');
    return;
  } finally {
    if (btnComprar) { btnComprar.disabled = false; btnComprar.textContent = '🎟️ Comprar Ahora'; }
  }

  // Sincronizar el stock local con lo que confirmó la API
  state.inventario[partidoId][categoria] = data.disponibles;
  partido.entradas[categoria].disponibles = data.disponibles;

  const local = getEquipo(partido.localCod);
  const visit = getEquipo(partido.visitanteCod);

  // Mostrar modal de éxito
  const overlay = document.createElement('div');
  overlay.className = 'modal-overlay';
  overlay.innerHTML = `
    <div class="modal" role="dialog" aria-modal="true">
      <div class="modal__icon">🎉</div>
      <div class="modal__title">¡Compra Exitosa!</div>
      <div class="modal__desc">
        Tu compra de <strong>${cantidad} entrada${cantidad !== 1 ? 's' : ''} (${categoria.charAt(0).toUpperCase() + categoria.slice(1)})</strong>
        para <strong>${partido.estado === 'por_definir' ? partido.fase : local.nombre + ' vs ' + visit.nombre}</strong>
        fue procesada correctamente.<br><br>
        Total cobrado: <strong>USD ${formatNum(data.total)}</strong>
      </div>
      <div class="modal__code">${data.codigo}</div>
      <p style="font-size:.78rem;color:var(--c-text-muted);margin-bottom:20px">
        Guardá este código de reserva. Te llegará también por email.
      </p>
      <button class="btn btn--primary btn--full btn--lg" onclick="this.closest('.modal-overlay').remove()">
        ✓ Entendido
      </button>
    </div>`;
  document.body.appendChild(overlay);
  overlay.addEventListener('click', e => { if (e.target === overlay) overlay.remove(); });

  // Actualizar la UI de entradas
  inicializarSelectorEntradas(partido);
}

// ============================================================
// VISTA: LISTADO DE ESTADIOS
// ============================================================
function renderEstadios() {
  const app = document.getElementById('app');

  app.innerHTML = `
    <section class="section">
      <div class="container">
        <div class="section__header">
          <div>
            <h1 class="section__title">Estadios</h1>
            <p class="section__subtitle">${DATA.estadios.length} sedes en EE.UU., México y Canadá</p>
          </div>
        </div>
        <div class="stadiums-grid" id="stadiums-grid"></div>
      </div>
    </section>`;

  const grid = document.getElementById('stadiums-grid');
  DATA.estadios.forEach(estadio => {
    grid.appendChild(crearStadiumCard(estadio));
  });
}

/** Crea una tarjeta de estadio */
function crearStadiumCard(estadio) {
  const partidosEstadio = DATA.partidos.filter(p => p.estadioId === estadio.id).length;

  const card = document.createElement('article');
  card.className = 'stadium-card';
  card.setAttribute('role', 'button');
  card.setAttribute('tabindex', '0');

  card.innerHTML = `
    <div class="stadium-card__visual">
      <span class="stadium-card__emoji">${estadio.imagen_emoji}</span>
      <span class="stadium-card__country-flag">${banderaPaisImg(estadio.pais)}</span>
    </div>
    <div class="stadium-card__body">
      <div class="stadium-card__name">${estadio.nombre}</div>
      <div class="stadium-card__city">📍 ${estadio.ciudad}, ${estadio.pais}</div>
      <div class="stadium-card__stats">
        <div class="stadium-stat">
          <div class="stadium-stat__value">${formatNum(estadio.capacidad)}</div>
          <div class="stadium-stat__label">Capacidad</div>
        </div>
        <div class="stadium-stat">
          <div class="stadium-stat__value">${estadio.inaugurado}</div>
          <div class="stadium-stat__label">Inaugurado</div>
        </div>
        <div class="stadium-stat">
          <div class="stadium-stat__value">${partidosEstadio}</div>
          <div class="stadium-stat__label">Partidos</div>
        </div>
      </div>
    </div>
    <div class="stadium-card__footer">
      <button class="btn btn--outline btn--sm">Ver detalles →</button>
    </div>`;

  const goDetail = () => navigate(`#/estadio/${estadio.id}`);
  card.addEventListener('click', goDetail);
  card.addEventListener('keydown', e => { if (e.key === 'Enter' || e.key === ' ') goDetail(); });

  return card;
}

// ============================================================
// VISTA: DETALLE DE ESTADIO
// ============================================================
function renderDetalleEstadio(id) {
  const app     = document.getElementById('app');
  const estadio = DATA.estadios.find(e => e.id === id);

  if (!estadio) { renderNoEncontrado(); return; }

  const partidos = DATA.partidos.filter(p => p.estadioId === id);

  app.innerHTML = `
    <!-- HERO ESTADIO -->
    <div class="stadium-detail__header">
      <div class="container">
        <button class="back-btn" onclick="navigate('#/estadios')">← Volver a Estadios</button>
        <div style="display:flex;align-items:center;gap:20px;flex-wrap:wrap">
          <div style="font-size:5rem">${estadio.imagen_emoji}</div>
          <div>
            <div style="font-size:.8rem;color:rgba(255,255,255,.5);margin-bottom:8px;text-transform:uppercase;letter-spacing:.06em">
              ${banderaPaisImg(estadio.pais, 'flag-img--inline')} ${estadio.pais}
            </div>
            <h1 style="font-family:var(--font-display);font-size:clamp(1.5rem,4vw,2.5rem);font-weight:700;line-height:1.1;margin-bottom:8px">
              ${estadio.nombre}
            </h1>
            <div style="font-size:.9rem;color:rgba(255,255,255,.6)">📍 ${estadio.ciudad}</div>
          </div>
        </div>
      </div>
    </div>

    <div class="container">
      <div class="stadium-detail__grid">

        <!-- INFO PRINCIPAL -->
        <div>
          <div class="info-card" style="margin-bottom:20px">
            <div class="info-card__title">📋 Información General</div>
            <table class="info-table">
              <tr><td>Nombre</td><td>${estadio.nombre}</td></tr>
              <tr><td>Ciudad</td><td>${estadio.ciudad}, ${estadio.pais} ${banderaPaisImg(estadio.pais, 'flag-img--inline')}</td></tr>
              <tr><td>Capacidad</td><td>${formatNum(estadio.capacidad)} espectadores</td></tr>
              <tr><td>Superficie</td><td>${estadio.superficie}</td></tr>
              <tr><td>Inaugurado</td><td>${estadio.inaugurado}</td></tr>
              <tr><td>Partidos 2026</td><td>${partidos.length} partido${partidos.length !== 1 ? 's' : ''}</td></tr>
            </table>
          </div>

          <div class="info-card" style="margin-bottom:20px">
            <div class="info-card__title">ℹ️ Sobre el Estadio</div>
            <p style="font-size:.9rem;color:var(--c-text-sec);line-height:1.7;margin-bottom:12px">${estadio.descripcion}</p>
            <p style="font-size:.85rem;color:var(--c-text-muted);line-height:1.6;border-left:3px solid var(--c-accent);padding-left:12px;font-style:italic">${estadio.datos}</p>
          </div>
        </div>

        <!-- PARTIDOS EN ESTE ESTADIO -->
        <div>
          <div class="info-card">
            <div class="info-card__title">⚽ Partidos en este Estadio</div>
            ${partidos.length === 0
              ? `<p style="font-size:.88rem;color:var(--c-text-muted)">No hay partidos asignados aún.</p>`
              : `<div style="display:flex;flex-direction:column;gap:10px">
                  ${partidos.map(p => {
                    const local     = getEquipo(p.localCod);
                    const visitante = getEquipo(p.visitanteCod);
                    const badge     = getBadgeEstado(p.estado);
                    return `
                      <div style="border:1.5px solid var(--c-border);border-radius:var(--r-md);padding:12px;cursor:pointer;transition:all .2s"
                        onclick="navigate('#/partido/${p.id}')"
                        onmouseenter="this.style.borderColor='var(--c-primary)';this.style.background='var(--c-surface-2)'"
                        onmouseleave="this.style.borderColor='var(--c-border)';this.style.background=''">
                        <div style="display:flex;justify-content:space-between;align-items:center;margin-bottom:6px">
                          <span style="font-size:.72rem;font-weight:600;color:var(--c-text-muted);text-transform:uppercase">
                            ${p.grupo ? `Grupo ${p.grupo} · ` : ''}${p.fase}
                          </span>
                          <span class="badge ${badge.cls}" style="font-size:.65rem">${badge.label}</span>
                        </div>
                        <div style="font-weight:600;font-size:.88rem;color:var(--c-text)">
                          ${p.estado === 'por_definir'
                            ? '🏆 Clasificados por determinar'
                            : `${banderaEquipoImg(p.localCod, local.nombre, 'flag-img--inline')} ${local.nombre} vs ${visitante.nombre} ${banderaEquipoImg(p.visitanteCod, visitante.nombre, 'flag-img--inline')}`}
                        </div>
                        <div style="font-size:.78rem;color:var(--c-text-muted);margin-top:4px">
                          📅 ${formatFecha(p.fecha)} · ${p.hora} hs
                        </div>
                      </div>`;
                  }).join('')}
                </div>`}
          </div>
        </div>
      </div>
    </div>`;
}

// ============================================================
// VISTA: LOGIN / REGISTRO
// ============================================================
function renderLogin() {
  if (estaLogueado()) { navigate('#/'); return; }

  document.getElementById('app').innerHTML = `
    <section class="section">
      <div class="container" style="max-width:420px">
        <div class="auth-card">
          <h1 class="section__title" style="margin-bottom:4px">Iniciar Sesión</h1>
          <p class="section__subtitle" style="margin-bottom:24px">Ingresá para poder comprar entradas</p>

          <form id="form-login" class="auth-form" novalidate>
            <div class="filter-group">
              <label for="login-email">Email</label>
              <input type="email" id="login-email" required autocomplete="email" placeholder="vos@email.com">
            </div>
            <div class="filter-group">
              <label for="login-password">Contraseña</label>
              <input type="password" id="login-password" required autocomplete="current-password" placeholder="••••••••">
            </div>
            <div id="login-error" class="auth-form__error" style="display:none"></div>
            <button type="submit" class="btn btn--primary btn--full btn--lg" id="login-submit">Ingresar</button>
          </form>

          <p class="auth-form__switch">¿No tenés cuenta? <a href="#/registro">Registrate</a></p>
        </div>
      </div>
    </section>`;

  document.getElementById('form-login').addEventListener('submit', async e => {
    e.preventDefault();
    const email = document.getElementById('login-email').value.trim();
    const password = document.getElementById('login-password').value;
    const errorEl = document.getElementById('login-error');
    const btn = document.getElementById('login-submit');

    errorEl.style.display = 'none';
    btn.disabled = true;
    btn.textContent = 'Ingresando...';

    try {
      const res = await fetch(`${API_BASE}/auth/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password })
      });
      const data = await res.json();
      if (!res.ok) {
        errorEl.textContent = data.mensaje || 'No se pudo iniciar sesión.';
        errorEl.style.display = 'block';
        return;
      }
      guardarSesion(data);
      mostrarToast(`¡Bienvenido, ${data.nombre.split(' ')[0]}!`, 'success');
      navigate('#/');
    } catch (err) {
      errorEl.textContent = 'No se pudo conectar con el servidor.';
      errorEl.style.display = 'block';
    } finally {
      btn.disabled = false;
      btn.textContent = 'Ingresar';
    }
  });
}

function renderRegistro() {
  if (estaLogueado()) { navigate('#/'); return; }

  document.getElementById('app').innerHTML = `
    <section class="section">
      <div class="container" style="max-width:420px">
        <div class="auth-card">
          <h1 class="section__title" style="margin-bottom:4px">Crear Cuenta</h1>
          <p class="section__subtitle" style="margin-bottom:24px">Registrate para comprar tus entradas</p>

          <form id="form-registro" class="auth-form" novalidate>
            <div class="filter-group">
              <label for="reg-nombre">Nombre completo</label>
              <input type="text" id="reg-nombre" required placeholder="Tu nombre">
            </div>
            <div class="filter-group">
              <label for="reg-email">Email</label>
              <input type="email" id="reg-email" required autocomplete="email" placeholder="vos@email.com">
            </div>
            <div class="filter-group">
              <label for="reg-password">Contraseña</label>
              <input type="password" id="reg-password" required minlength="6" autocomplete="new-password" placeholder="Mínimo 6 caracteres">
            </div>
            <div id="reg-error" class="auth-form__error" style="display:none"></div>
            <button type="submit" class="btn btn--primary btn--full btn--lg" id="reg-submit">Crear cuenta</button>
          </form>

          <p class="auth-form__switch">¿Ya tenés cuenta? <a href="#/login">Iniciá sesión</a></p>
        </div>
      </div>
    </section>`;

  document.getElementById('form-registro').addEventListener('submit', async e => {
    e.preventDefault();
    const nombre = document.getElementById('reg-nombre').value.trim();
    const email = document.getElementById('reg-email').value.trim();
    const password = document.getElementById('reg-password').value;
    const errorEl = document.getElementById('reg-error');
    const btn = document.getElementById('reg-submit');

    errorEl.style.display = 'none';
    btn.disabled = true;
    btn.textContent = 'Creando cuenta...';

    try {
      const res = await fetch(`${API_BASE}/auth/registro`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ nombre, email, password })
      });
      const data = await res.json();
      if (!res.ok) {
        errorEl.textContent = data.mensaje || 'No se pudo crear la cuenta.';
        errorEl.style.display = 'block';
        return;
      }
      guardarSesion(data);
      mostrarToast(`¡Cuenta creada! Bienvenido, ${data.nombre.split(' ')[0]}.`, 'success');
      navigate('#/');
    } catch (err) {
      errorEl.textContent = 'No se pudo conectar con el servidor.';
      errorEl.style.display = 'block';
    } finally {
      btn.disabled = false;
      btn.textContent = 'Crear cuenta';
    }
  });
}

// ============================================================
// PANEL DE ADMINISTRACIÓN
// ============================================================
function renderAdminDashboard() {
  document.getElementById('app').innerHTML = `
    <section class="section">
      <div class="container">
        <div class="section__header">
          <div>
            <h1 class="section__title">⚙️ Panel de Administración</h1>
            <p class="section__subtitle">Hola ${state.auth.nombre} — gestioná los partidos y estadios del torneo</p>
          </div>
        </div>
        <div style="display:grid;gap:20px;grid-template-columns:repeat(auto-fit,minmax(240px,1fr))">
          <div class="info-card" style="cursor:pointer" onclick="navigate('#/admin/partidos')">
            <div style="font-size:2rem;margin-bottom:8px">⚽</div>
            <div class="info-card__title" style="margin-bottom:4px">Partidos</div>
            <p style="font-size:.85rem;color:var(--c-text-sec);margin-bottom:12px">${DATA.partidos.length} partidos cargados — crear, editar o eliminar</p>
            <span class="btn btn--outline btn--sm">Gestionar →</span>
          </div>
          <div class="info-card" style="cursor:pointer" onclick="navigate('#/admin/estadios')">
            <div style="font-size:2rem;margin-bottom:8px">🏟️</div>
            <div class="info-card__title" style="margin-bottom:4px">Estadios</div>
            <p style="font-size:.85rem;color:var(--c-text-sec);margin-bottom:12px">${DATA.estadios.length} estadios cargados — crear, editar o eliminar</p>
            <span class="btn btn--outline btn--sm">Gestionar →</span>
          </div>
        </div>
      </div>
    </section>`;
}

/** Cierra cualquier modal abierto */
function cerrarModal() {
  document.querySelectorAll('.modal-overlay').forEach(o => o.remove());
}

/** Abre un modal genérico con el HTML provisto (usado por los formularios admin) */
function abrirModal(innerHtml, clase = '') {
  cerrarModal();
  const overlay = document.createElement('div');
  overlay.className = 'modal-overlay';
  overlay.innerHTML = `<div class="modal modal--admin ${clase}" role="dialog" aria-modal="true">${innerHtml}</div>`;
  overlay.addEventListener('click', e => { if (e.target === overlay) overlay.remove(); });
  document.body.appendChild(overlay);
  return overlay;
}

// ---------- ADMIN: PARTIDOS ----------
function renderAdminPartidos() {
  const filas = [...DATA.partidos].sort((a, b) => a.fecha.localeCompare(b.fecha)).map(p => {
    const local = getEquipo(p.localCod).nombre;
    const visit = getEquipo(p.visitanteCod).nombre;
    return `
      <tr>
        <td>${p.id}</td>
        <td>${formatFecha(p.fecha)} · ${p.hora}</td>
        <td>${p.fase}${p.grupo ? ` (${p.grupo})` : ''}</td>
        <td>${local} vs ${visit}</td>
        <td><span class="badge ${getBadgeEstado(p.estado).cls}">${getBadgeEstado(p.estado).label}</span></td>
        <td class="admin-table__actions">
          <button class="btn btn--outline btn--sm" onclick="abrirFormPartido('${p.id}')">Editar</button>
          <button class="btn btn--danger btn--sm" onclick="eliminarPartido('${p.id}')">Eliminar</button>
        </td>
      </tr>`;
  }).join('');

  document.getElementById('app').innerHTML = `
    <section class="section">
      <div class="container">
        <button class="back-btn" style="color:var(--c-text-sec);background:var(--c-surface);border-color:var(--c-border)" onclick="navigate('#/admin')">← Volver al panel</button>
        <div class="section__header">
          <div>
            <h1 class="section__title">Partidos</h1>
            <p class="section__subtitle">${DATA.partidos.length} partidos</p>
          </div>
          <button class="btn btn--primary" onclick="abrirFormPartido()">+ Nuevo Partido</button>
        </div>
        <div class="admin-table-wrap">
          <table class="admin-table">
            <thead><tr><th>ID</th><th>Fecha</th><th>Fase</th><th>Partido</th><th>Estado</th><th>Acciones</th></tr></thead>
            <tbody>${filas}</tbody>
          </table>
        </div>
      </div>
    </section>`;
}

/** Abre el formulario de alta/edición de partido. Sin id = alta. */
function abrirFormPartido(id) {
  const partido = id ? DATA.partidos.find(p => p.id === id) : null;
  const cats = ['general', 'preferencial', 'vip'];
  const catLabels = { general: 'General', preferencial: 'Preferencial', vip: 'VIP' };

  const estadioOptions = DATA.estadios.map(e => `<option value="${e.id}" ${partido?.estadioId === e.id ? 'selected' : ''}>${e.nombre}</option>`).join('');
  const equipoOptions = `<option value="">— Por definir —</option>` + Object.entries(DATA.equipos)
    .sort((a, b) => a[1].nombre.localeCompare(b[1].nombre))
    .map(([cod, eq]) => `<option value="${cod}">${eq.nombre}</option>`).join('');

  const entradasActuales = partido?.entradas || {};

  abrirModal(`
    <div class="modal__icon" style="font-size:2rem">${partido ? '✏️' : '➕'}</div>
    <div class="modal__title" style="margin-bottom:20px">${partido ? `Editar ${partido.id}` : 'Nuevo Partido'}</div>
    <form id="form-partido" class="auth-form admin-form" style="text-align:left">
      <div class="filter-group">
        <label for="p-id">ID del partido</label>
        <input type="text" id="p-id" value="${partido?.id || ''}" ${partido ? 'disabled' : ''} placeholder="Ej: M105" required>
      </div>
      <div class="filter-group">
        <label for="p-fase">Fase</label>
        <select id="p-fase" required>
          ${['Fase de Grupos', 'Dieciseisavos de Final', 'Octavos de Final', 'Cuartos de Final', 'Semifinal', 'Tercer Puesto', 'Gran Final']
            .map(f => `<option value="${f}" ${partido?.fase === f ? 'selected' : ''}>${f}</option>`).join('')}
        </select>
      </div>
      <div style="display:flex;gap:10px">
        <div class="filter-group"><label for="p-grupo">Grupo (opcional)</label><input type="text" id="p-grupo" maxlength="1" value="${partido?.grupo || ''}"></div>
        <div class="filter-group"><label for="p-jornada">Jornada (opcional)</label><input type="number" id="p-jornada" min="1" max="3" value="${partido?.jornada ?? ''}"></div>
      </div>
      <div style="display:flex;gap:10px">
        <div class="filter-group"><label for="p-fecha">Fecha</label><input type="date" id="p-fecha" value="${partido?.fecha || ''}" required></div>
        <div class="filter-group"><label for="p-hora">Hora</label><input type="time" id="p-hora" value="${partido?.hora || ''}" required></div>
      </div>
      <div class="filter-group">
        <label for="p-estadio">Estadio</label>
        <select id="p-estadio" required>${estadioOptions}</select>
      </div>
      <div style="display:flex;gap:10px">
        <div class="filter-group"><label for="p-local">Local</label><select id="p-local">${equipoOptions}</select></div>
        <div class="filter-group"><label for="p-visitante">Visitante</label><select id="p-visitante">${equipoOptions}</select></div>
      </div>
      <div class="filter-group">
        <label for="p-estado">Estado</label>
        <select id="p-estado" required>
          ${[['programado', 'Programado'], ['en_juego', 'En Juego'], ['finalizado', 'Finalizado'], ['por_definir', 'Por Definir']]
            .map(([v, l]) => `<option value="${v}" ${partido?.estado === v ? 'selected' : ''}>${l}</option>`).join('')}
        </select>
      </div>
      <div style="display:flex;gap:10px">
        <div class="filter-group"><label for="p-goles-local">Goles local</label><input type="number" id="p-goles-local" min="0" value="${partido?.golesLocal ?? ''}"></div>
        <div class="filter-group"><label for="p-goles-visitante">Goles visitante</label><input type="number" id="p-goles-visitante" min="0" value="${partido?.golesVisitante ?? ''}"></div>
      </div>
      <div class="filter-group">
        <label for="p-nota">Nota de resultado (opcional)</label>
        <input type="text" id="p-nota" placeholder="Ej: Paraguay avanza 4-3 en penales" value="${partido?.notaResultado || ''}">
      </div>

      <div class="admin-form__seccion">Entradas por categoría</div>
      ${cats.map(c => `
        <div class="admin-form__cat-row">
          <span class="admin-form__cat-label">${catLabels[c]}</span>
          <input type="number" id="p-precio-${c}" min="0" step="1" placeholder="Precio USD" value="${entradasActuales[c]?.precio ?? 0}">
          <input type="number" id="p-total-${c}" min="0" step="1" placeholder="Total" value="${entradasActuales[c]?.total ?? 0}">
          <input type="number" id="p-disp-${c}" min="0" step="1" placeholder="Disponibles" value="${entradasActuales[c]?.disponibles ?? 0}">
        </div>`).join('')}

      <div id="form-partido-error" class="auth-form__error" style="display:none"></div>
      <div style="display:flex;gap:10px;margin-top:8px">
        <button type="button" class="btn btn--ghost btn--full" onclick="cerrarModal()">Cancelar</button>
        <button type="submit" class="btn btn--primary btn--full" id="form-partido-submit">${partido ? 'Guardar cambios' : 'Crear partido'}</button>
      </div>
    </form>`, 'modal--ancho');

  document.getElementById('form-partido').addEventListener('submit', async e => {
    e.preventDefault();
    const cats = ['general', 'preferencial', 'vip'];
    const body = {
      partidoId: document.getElementById('p-id').value.trim().toUpperCase(),
      fase: document.getElementById('p-fase').value,
      grupo: document.getElementById('p-grupo').value.trim() || null,
      jornada: document.getElementById('p-jornada').value ? Number(document.getElementById('p-jornada').value) : null,
      fecha: document.getElementById('p-fecha').value,
      hora: document.getElementById('p-hora').value,
      estadioId: document.getElementById('p-estadio').value,
      localCod: document.getElementById('p-local').value || null,
      visitanteCod: document.getElementById('p-visitante').value || null,
      estado: document.getElementById('p-estado').value,
      golesLocal: document.getElementById('p-goles-local').value !== '' ? Number(document.getElementById('p-goles-local').value) : null,
      golesVisitante: document.getElementById('p-goles-visitante').value !== '' ? Number(document.getElementById('p-goles-visitante').value) : null,
      notaResultado: document.getElementById('p-nota').value.trim() || null,
      entradas: cats.map(c => ({
        categoria: c,
        precio: Number(document.getElementById(`p-precio-${c}`).value || 0),
        total: Number(document.getElementById(`p-total-${c}`).value || 0),
        disponibles: Number(document.getElementById(`p-disp-${c}`).value || 0),
      })),
    };

    const errorEl = document.getElementById('form-partido-error');
    const btn = document.getElementById('form-partido-submit');
    errorEl.style.display = 'none';
    btn.disabled = true;

    try {
      const res = await fetchAuth(`${API_BASE}/partidos${partido ? '/' + partido.id : ''}`, {
        method: partido ? 'PUT' : 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
      });
      const data = await res.json().catch(() => ({}));
      if (!res.ok) {
        errorEl.textContent = data.mensaje || 'No se pudo guardar el partido.';
        errorEl.style.display = 'block';
        return;
      }
      mostrarToast(partido ? 'Partido actualizado.' : 'Partido creado.', 'success');
      cerrarModal();
      await cargarDatos();
      renderAdminPartidos();
    } catch (err) {
      errorEl.textContent = 'No se pudo conectar con el servidor.';
      errorEl.style.display = 'block';
    } finally {
      btn.disabled = false;
    }
  });
}

async function eliminarPartido(id) {
  if (!confirm(`¿Eliminar el partido ${id}? Esta acción no se puede deshacer.`)) return;
  try {
    const res = await fetchAuth(`${API_BASE}/partidos/${id}`, { method: 'DELETE' });
    if (!res.ok) {
      const data = await res.json().catch(() => ({}));
      mostrarToast(data.mensaje || 'No se pudo eliminar el partido.', 'error');
      return;
    }
    mostrarToast('Partido eliminado.', 'success');
    await cargarDatos();
    renderAdminPartidos();
  } catch (err) {
    mostrarToast('No se pudo conectar con el servidor.', 'error');
  }
}

// ---------- ADMIN: ESTADIOS ----------
function renderAdminEstadios() {
  const filas = [...DATA.estadios].sort((a, b) => a.nombre.localeCompare(b.nombre)).map(e => `
    <tr>
      <td>${e.id}</td>
      <td>${e.nombre}</td>
      <td>${e.ciudad}, ${e.pais}</td>
      <td>${formatNum(e.capacidad)}</td>
      <td class="admin-table__actions">
        <button class="btn btn--outline btn--sm" onclick="abrirFormEstadio('${e.id}')">Editar</button>
        <button class="btn btn--danger btn--sm" onclick="eliminarEstadio('${e.id}')">Eliminar</button>
      </td>
    </tr>`).join('');

  document.getElementById('app').innerHTML = `
    <section class="section">
      <div class="container">
        <button class="back-btn" style="color:var(--c-text-sec);background:var(--c-surface);border-color:var(--c-border)" onclick="navigate('#/admin')">← Volver al panel</button>
        <div class="section__header">
          <div>
            <h1 class="section__title">Estadios</h1>
            <p class="section__subtitle">${DATA.estadios.length} estadios</p>
          </div>
          <button class="btn btn--primary" onclick="abrirFormEstadio()">+ Nuevo Estadio</button>
        </div>
        <div class="admin-table-wrap">
          <table class="admin-table">
            <thead><tr><th>ID</th><th>Nombre</th><th>Ciudad / País</th><th>Capacidad</th><th>Acciones</th></tr></thead>
            <tbody>${filas}</tbody>
          </table>
        </div>
      </div>
    </section>`;
}

function abrirFormEstadio(id) {
  const estadio = id ? DATA.estadios.find(e => e.id === id) : null;

  abrirModal(`
    <div class="modal__icon" style="font-size:2rem">${estadio ? '✏️' : '➕'}</div>
    <div class="modal__title" style="margin-bottom:20px">${estadio ? `Editar ${estadio.id}` : 'Nuevo Estadio'}</div>
    <form id="form-estadio" class="auth-form admin-form" style="text-align:left">
      <div class="filter-group">
        <label for="e-id">ID del estadio</label>
        <input type="text" id="e-id" value="${estadio?.id || ''}" ${estadio ? 'disabled' : ''} placeholder="Ej: EST17" required>
      </div>
      <div class="filter-group"><label for="e-nombre">Nombre</label><input type="text" id="e-nombre" value="${estadio?.nombre || ''}" required></div>
      <div style="display:flex;gap:10px">
        <div class="filter-group"><label for="e-ciudad">Ciudad</label><input type="text" id="e-ciudad" value="${estadio?.ciudad || ''}" required></div>
        <div class="filter-group"><label for="e-pais">País</label><input type="text" id="e-pais" value="${estadio?.pais || ''}" required></div>
      </div>
      <div style="display:flex;gap:10px">
        <div class="filter-group"><label for="e-capacidad">Capacidad</label><input type="number" id="e-capacidad" min="0" value="${estadio?.capacidad ?? ''}" required></div>
        <div class="filter-group"><label for="e-inaugurado">Inaugurado</label><input type="number" id="e-inaugurado" min="1800" max="2100" value="${estadio?.inaugurado ?? ''}" required></div>
      </div>
      <div class="filter-group"><label for="e-superficie">Superficie</label><input type="text" id="e-superficie" value="${estadio?.superficie || ''}" required></div>
      <div class="filter-group"><label for="e-descripcion">Descripción</label><input type="text" id="e-descripcion" value="${estadio?.descripcion || ''}"></div>
      <div class="filter-group"><label for="e-datos">Dato curioso</label><input type="text" id="e-datos" value="${estadio?.datos || ''}"></div>

      <div id="form-estadio-error" class="auth-form__error" style="display:none"></div>
      <div style="display:flex;gap:10px;margin-top:8px">
        <button type="button" class="btn btn--ghost btn--full" onclick="cerrarModal()">Cancelar</button>
        <button type="submit" class="btn btn--primary btn--full" id="form-estadio-submit">${estadio ? 'Guardar cambios' : 'Crear estadio'}</button>
      </div>
    </form>`, 'modal--ancho');

  document.getElementById('form-estadio').addEventListener('submit', async e => {
    e.preventDefault();
    const body = {
      estadioId: document.getElementById('e-id').value.trim().toUpperCase(),
      nombre: document.getElementById('e-nombre').value.trim(),
      ciudad: document.getElementById('e-ciudad').value.trim(),
      pais: document.getElementById('e-pais').value.trim(),
      banderaPaisEmoji: '',
      capacidad: Number(document.getElementById('e-capacidad').value || 0),
      superficie: document.getElementById('e-superficie').value.trim(),
      inaugurado: Number(document.getElementById('e-inaugurado').value || 0),
      descripcion: document.getElementById('e-descripcion').value.trim(),
      datos: document.getElementById('e-datos').value.trim(),
      imagenEmoji: '🏟️',
    };

    const errorEl = document.getElementById('form-estadio-error');
    const btn = document.getElementById('form-estadio-submit');
    errorEl.style.display = 'none';
    btn.disabled = true;

    try {
      const res = await fetchAuth(`${API_BASE}/estadios${estadio ? '/' + estadio.id : ''}`, {
        method: estadio ? 'PUT' : 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
      });
      const data = await res.json().catch(() => ({}));
      if (!res.ok) {
        errorEl.textContent = data.mensaje || 'No se pudo guardar el estadio.';
        errorEl.style.display = 'block';
        return;
      }
      mostrarToast(estadio ? 'Estadio actualizado.' : 'Estadio creado.', 'success');
      cerrarModal();
      await cargarDatos();
      renderAdminEstadios();
    } catch (err) {
      errorEl.textContent = 'No se pudo conectar con el servidor.';
      errorEl.style.display = 'block';
    } finally {
      btn.disabled = false;
    }
  });
}

async function eliminarEstadio(id) {
  if (!confirm(`¿Eliminar el estadio ${id}? Esta acción no se puede deshacer.`)) return;
  try {
    const res = await fetchAuth(`${API_BASE}/estadios/${id}`, { method: 'DELETE' });
    if (!res.ok) {
      const data = await res.json().catch(() => ({}));
      mostrarToast(data.mensaje || 'No se pudo eliminar el estadio.', 'error');
      return;
    }
    mostrarToast('Estadio eliminado.', 'success');
    await cargarDatos();
    renderAdminEstadios();
  } catch (err) {
    mostrarToast('No se pudo conectar con el servidor.', 'error');
  }
}

// ============================================================
// VISTA: 404
// ============================================================
function renderNoEncontrado() {
  document.getElementById('app').innerHTML = `
    <div class="empty-state" style="padding:100px 20px">
      <div class="empty-state__icon">😕</div>
      <div class="empty-state__title">Página no encontrada</div>
      <div class="empty-state__desc">La página que buscás no existe.</div>
      <br>
      <button class="btn btn--primary" onclick="navigate('#/')">Ir al Inicio</button>
    </div>`;
}

// ============================================================
// SISTEMA DE TOASTS (notificaciones)
// ============================================================
function mostrarToast(mensaje, tipo = 'info') {
  const container = document.getElementById('toastContainer');
  const toast     = document.createElement('div');
  toast.className = `toast toast--${tipo}`;
  toast.innerHTML = `
    <span style="font-size:1.2rem">${tipo === 'success' ? '✅' : tipo === 'error' ? '❌' : 'ℹ️'}</span>
    <span>${mensaje}</span>`;

  container.appendChild(toast);

  setTimeout(() => {
    toast.classList.add('is-leaving');
    toast.addEventListener('animationend', () => toast.remove());
  }, 4000);
}

// ============================================================
// NAVBAR MÓVIL
// ============================================================
function cerrarMenu() {
  const menu   = document.getElementById('navMenu');
  const toggle = document.getElementById('navToggle');
  if (menu)   { menu.classList.remove('is-open'); menu.setAttribute('aria-hidden', 'true'); }
  if (toggle) { toggle.classList.remove('is-open'); toggle.setAttribute('aria-expanded', 'false'); }
}

/** Pantalla de error cuando la API no responde */
function renderErrorCarga() {
  document.getElementById('app').innerHTML = `
    <div class="empty-state" style="padding:100px 20px">
      <div class="empty-state__icon">🔌</div>
      <div class="empty-state__title">No se pudo conectar con la API</div>
      <div class="empty-state__desc">
        Verificá que el backend esté corriendo en <code>${API_BASE}</code>.<br>
        (En /backend/Fifa2026.Api: <code>dotnet run</code>)
      </div>
      <br>
      <button class="btn btn--primary" onclick="location.reload()">Reintentar</button>
    </div>`;
}

// ============================================================
// INICIALIZACIÓN
// ============================================================
document.addEventListener('DOMContentLoaded', async () => {
  // Hamburger toggle
  const toggle = document.getElementById('navToggle');
  const menu   = document.getElementById('navMenu');
  if (toggle && menu) {
    toggle.addEventListener('click', () => {
      const isOpen = menu.classList.toggle('is-open');
      toggle.classList.toggle('is-open', isOpen);
      toggle.setAttribute('aria-expanded', String(isOpen));
      menu.setAttribute('aria-hidden', String(!isOpen));
    });
  }

  // Cerrar menú al hacer click fuera
  document.addEventListener('click', e => {
    if (!e.target.closest('.navbar__inner') && !e.target.closest('#navMenu')) cerrarMenu();
  });

  // Restaurar sesión guardada (si hay token vigente)
  state.auth = cargarSesion();
  renderNavAuth();

  // Cargar datos reales desde la API antes de arrancar el router
  try {
    await cargarDatos();
  } catch (err) {
    renderErrorCarga();
    return;
  }

  window.addEventListener('hashchange', router);
  router();
});
