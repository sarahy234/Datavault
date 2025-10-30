/* =========================
   js/script.js — Integrado
   ========================= */

/* ========== util ========== */
const qs = (sel, root = document) => (root || document).querySelector(sel);
const qsa = (sel, root = document) => Array.from((root || document).querySelectorAll(sel));

/* ========== mobile menu ========== */
const mobileToggle = qs('#mobile-toggle');
const mobileMenu = qs('#mobile-menu');
if (mobileToggle) {
    mobileToggle.addEventListener('click', () => {
        const open = mobileMenu.classList.toggle('open');
        mobileToggle.setAttribute('aria-expanded', open ? 'true' : 'false');
        mobileMenu.setAttribute('aria-hidden', open ? 'false' : 'true');
    });
    document.addEventListener('click', (e) => {
        if (mobileMenu && !mobileMenu.contains(e.target) && !mobileToggle.contains(e.target)) {
            mobileMenu.classList.remove('open');
            mobileMenu.setAttribute('aria-hidden', 'true');
            mobileToggle.setAttribute('aria-expanded', 'false');
        }
    });
}

/* ========== scroll to sections ========== */
qsa('.nav-btn').forEach(btn => {
    btn.addEventListener('click', () => {
        const tgt = btn.getAttribute('data-target');
        if (!tgt) return;
        const el = document.querySelector(tgt);
        if (el) el.scrollIntoView({ behavior: 'smooth', block: 'start' });
        if (mobileMenu) mobileMenu.classList.remove('open');
    });
});

/* ====== CAROUSEL MULTI-SLIDE (imagen + texto + icono) ====== */
(function carouselFull() {
    const slidesData = [
        { title: 'Repositorio Digital de Material Académico', description: 'Centraliza, organiza y preserva los materiales académicos de la carrera de Ingeniería de Sistemas — Universidad del Valle, La Paz.', icon: 'assets/img/libro.png', image: 'assets/img/carrusel1.jpg', overlay: '' },
        { title: 'Acceso Fácil y Rápido', description: 'Busca y encuentra materiales académicos con filtros avanzados por materia, semestre, autor y palabras clave.', icon: 'assets/img/buscar.png', image: 'assets/img/carrusel2.jpg' },
        { title: 'Comparte tu Conocimiento', description: 'Docentes y estudiantes pueden cargar recursos en formatos PDF, PPTX, DOCX, ZIP y código fuente.', icon: 'assets/img/descargar.png', image: 'assets/img/carrusel4.jpg' },
        { title: 'Validación Institucional', description: 'Control de versiones y validación de contenido garantiza la calidad y confiabilidad de los recursos compartidos.', icon: 'assets/img/validacion.png', image: 'assets/img/carrusel3.jpg' }
    ];

    const slidesNodes = qsa('.slide');
    const dotsWrap = qs('.hero-dots');
    const leftBtn = qs('.hero-arrow.left');
    const rightBtn = qs('.hero-arrow.right');
    const heroTitle = document.querySelector('.hero-left h1');
    const heroDesc = document.querySelector('.hero-left .lead');
    const heroIcon = document.querySelector('.hero-icon');

    let idx = 0;
    let timer = null;
    const INTERVAL = 4000;

    slidesNodes.forEach((s, i) => {
        const src = s.getAttribute('data-src') || (slidesData[i] && slidesData[i].image);
        if (src) s.style.backgroundImage = `url('${src}')`;
    });

    if (slidesNodes.length === 0) {
        const slidesContainer = document.getElementById('slides') || document.querySelector('.slides');
        if (slidesContainer) {
            slidesData.forEach(sd => {
                const div = document.createElement('div');
                div.className = 'slide';
                div.style.backgroundImage = `url('${sd.image}')`;
                slidesContainer.appendChild(div);
            });
        }
    }

    function renderDots() {
        if (!dotsWrap) return;
        dotsWrap.innerHTML = '';
        for (let i = 0; i < slidesData.length; i++) {
            const b = document.createElement('button');
            b.className = i === idx ? 'active' : '';
            b.setAttribute('aria-label', `Ir al slide ${i + 1}`);
            b.addEventListener('click', () => { goTo(i); resetTimer(); });
            dotsWrap.appendChild(b);
        }
    }

    function show(i) {
        const slides = qsa('.slide');
        const data = slidesData[i];

        if (heroTitle) heroTitle.classList.add('fade-out');
        if (heroDesc) heroDesc.classList.add('fade-out');
        if (heroIcon) heroIcon.classList.add('fade-out');

        slides.forEach((s, k) => s.classList.toggle('visible', k === i));

        setTimeout(() => {
            if (heroTitle) {
                heroTitle.textContent = data.title;
                heroTitle.classList.remove('fade-out');
                heroTitle.classList.add('fade-in');
            }
            if (heroDesc) {
                heroDesc.textContent = data.description;
                heroDesc.classList.remove('fade-out');
                heroDesc.classList.add('fade-in');
            }
            if (heroIcon) {
                heroIcon.style.transform = 'scale(.96)';
                setTimeout(() => {
                    heroIcon.src = data.icon;
                    heroIcon.alt = data.title + ' icon';
                    heroIcon.style.width = heroIcon.style.width || '56px';
                    heroIcon.style.height = heroIcon.style.height || '56px';
                    heroIcon.style.objectFit = 'contain';
                    heroIcon.style.transform = 'none';
                }, 60);
                heroIcon.classList.remove('fade-out');
                heroIcon.classList.add('fade-in');
            }

            if (dotsWrap) Array.from(dotsWrap.children).forEach((btn, k) => btn.classList.toggle('active', k === i));

            setTimeout(() => {
                if (heroTitle) heroTitle.classList.remove('fade-in');
                if (heroDesc) heroDesc.classList.remove('fade-in');
                if (heroIcon) heroIcon.classList.remove('fade-in');
            }, 700);
        }, 420);
    }

    function next() { idx = (idx + 1) % slidesData.length; show(idx); }
    function prev() { idx = (idx - 1 + slidesData.length) % slidesData.length; show(idx); }
    function goTo(i) { idx = ((i % slidesData.length) + slidesData.length) % slidesData.length; show(idx); }

    if (rightBtn) rightBtn.addEventListener('click', () => { next(); resetTimer(); });
    if (leftBtn) leftBtn.addEventListener('click', () => { prev(); resetTimer(); });

    function resetTimer() {
        if (timer) clearInterval(timer);
        timer = setInterval(() => next(), INTERVAL);
    }

    document.addEventListener('keydown', (e) => {
        if (e.key === 'ArrowRight') { next(); resetTimer(); }
        if (e.key === 'ArrowLeft') { prev(); resetTimer(); }
    });

    if (heroIcon) heroIcon.addEventListener('click', () => { next(); resetTimer(); });

    renderDots();
    show(idx);
    resetTimer();
})();

/* ========== uniform icon sizing (fallback) ========== */
qsa('.icon-circle img, .badge img, .hero-icon, .logo-img, .small-logo, .search-row img').forEach(img => {
    const ds = img.getAttribute('data-size');
    if (ds) img.style.width = img.style.height = ds;
    else img.style.width = img.style.width || (img.classList.contains('small-logo') ? '72px' : '42px');
    img.style.height = img.style.height || img.style.width;
    img.style.objectFit = 'contain';
});

/* ========== product card flip ========== */
qsa('.product-card').forEach(card => {
    card.addEventListener('click', () => card.classList.toggle('flipped'));
    card.addEventListener('keydown', (e) => { if (e.key === 'Enter' || e.key === ' ') card.classList.toggle('flipped'); });
});

/* ========== quick search mock ========== */
const searchBtn = qs('#search-btn');
const quickSearch = qs('#quick-search');
if (searchBtn && quickSearch) {
    searchBtn.addEventListener('click', () => {
        const q = quickSearch.value.trim();
        if (!q) { quickSearch.focus(); return; }
        searchBtn.classList.add('pulse');
        setTimeout(() => searchBtn.classList.remove('pulse'), 700);
        alert(`Búsqueda demo: "${q}". Implementa API para resultados reales.`);
    });
}

/* ========== contact form (demo) ========== */
const contactForm = qs('#contact-form');
if (contactForm) {
    contactForm.addEventListener('submit', (e) => {
        e.preventDefault();
        const name = (contactForm.name || {}).value?.trim() || '';
        const email = (contactForm.email || {}).value?.trim() || '';
        const message = (contactForm.message || {}).value?.trim() || '';
        if (!name || !email || !message) {
            alert('Completa todos los campos del formulario.');
            return;
        }
        alert('¡Gracias! Mensaje enviado (demo).');
        contactForm.reset();
    });
}

/* ========== pwd toggle ========== */
qsa('.pwd-toggle').forEach(btn => {
    btn.addEventListener('click', () => {
        const tgt = btn.getAttribute('data-target');
        const input = document.getElementById(tgt);
        if (!input) return;
        input.type = input.type === 'password' ? 'text' : 'password';
        btn.classList.toggle('active');
    });
});

/* ========== login validation + signup validation (inline errors) ========== */
const loginForm = qs('#login-form');
if (loginForm) {
    loginForm.addEventListener('submit', (e) => {
        e.preventDefault();
        clearErrors(loginForm);
        const name = (qs('#login-name') || {}).value?.trim() || '';
        const email = (qs('#login-email') || {}).value?.trim() || '';
        const pwd = (qs('#login-password') || {}).value || '';
        const role = (qs('#login-role') || {}).value || '';
        const emailRegex = /^[^\s@]+@est\.univalle\.edu$/;
        const errors = [];
        if (!name) errors.push({ field: 'login-name', msg: 'El nombre es requerido.' });
        if (!emailRegex.test(email)) errors.push({ field: 'login-email', msg: 'Correo institucional: @est.univalle.edu' });
        if (!pwd || pwd.length < 8) errors.push({ field: 'login-password', msg: 'La contraseña debe tener al menos 8 caracteres.' });
        if (!role) errors.push({ field: 'login-role', msg: 'Selecciona un rol.' });
        if (errors.length) { showErrors(loginForm, errors); return; }

        const btn = loginForm.querySelector('button[type="submit"]');
        const old = btn.textContent;
        btn.textContent = 'Iniciando...'; btn.disabled = true;
        setTimeout(() => {
            alert('Inicio de sesión exitoso (demo).');
            window.location.href = 'index.html';
            btn.textContent = old; btn.disabled = false;
        }, 850);
    });
}

const signupForm = qs('#signup-form');
if (signupForm) {
    const hints = {
        length: qs('#h-length'),
        upper: qs('#h-upper'),
        lower: qs('#h-lower'),
        number: qs('#h-number')
    };
    const suPassword = qs('#su-password');
    if (suPassword) {
        suPassword.addEventListener('input', (e) => {
            const v = e.target.value;
            toggleHint(hints.length, v.length >= 8);
            toggleHint(hints.upper, /[A-Z]/.test(v));
            toggleHint(hints.lower, /[a-z]/.test(v));
            toggleHint(hints.number, /[0-9]/.test(v));
        });
    }

    signupForm.addEventListener('submit', (e) => {
        e.preventDefault();
        clearErrors(signupForm);
        const name = (qs('#su-name') || {}).value?.trim() || '';
        const email = (qs('#su-email') || {}).value?.trim() || '';
        const pwd = (qs('#su-password') || {}).value || '';
        const conf = (qs('#su-confirm') || {}).value || '';
        const role = (qs('#su-role') || {}).value || '';
        const terms = (qs('#su-terms') || {}).checked || false;
        const emailRegex = /^[^\s@]+@est\.univalle\.edu$/;
        const errors = [];

        if (!name || name.length < 3) errors.push({ field: 'su-name', msg: 'Nombre mínimo 3 caracteres.' });
        if (!emailRegex.test(email)) errors.push({ field: 'su-email', msg: 'Correo institucional requerido: @est.univalle.edu' });
        if (!pwd || pwd.length < 8) errors.push({ field: 'su-password', msg: 'Contraseña mínimo 8 caracteres.' });
        if (!(/[A-Z]/.test(pwd))) errors.push({ field: 'su-password', msg: 'Debe incluir al menos 1 mayúscula.' });
        if (!(/[a-z]/.test(pwd))) errors.push({ field: 'su-password', msg: 'Debe incluir al menos 1 minúscula.' });
        if (!(/[0-9]/.test(pwd))) errors.push({ field: 'su-password', msg: 'Debe incluir al menos 1 número.' });
        if (pwd !== conf) errors.push({ field: 'su-confirm', msg: 'Las contraseñas no coinciden.' });
        if (!role) errors.push({ field: 'su-role', msg: 'Selecciona un rol.' });
        if (!terms) errors.push({ field: 'su-terms', msg: 'Debes aceptar los términos.' });

        if (errors.length) { showErrors(signupForm, errors); return; }

        const btn = signupForm.querySelector('button[type="submit"]');
        const old = btn.textContent;
        btn.textContent = 'Creando...'; btn.disabled = true;
        setTimeout(() => {
            alert('Registro exitoso (demo). Redirigiendo...');
            window.location.href = 'index.html';
            btn.textContent = old; btn.disabled = false;
        }, 1000);
    });
}

/* ========== helpers ========== */
function toggleHint(elem, ok) { if (!elem) return; elem.classList.toggle('ok', ok); }
function clearErrors(form) { Array.from(form.querySelectorAll('.error')).forEach(el => el.textContent = ''); }
function showErrors(form, errors) { errors.forEach(err => { const span = form.querySelector(`.error[data-for="${err.field}"]`); if (span) span.textContent = err.msg; }); }

/* ========== reveal on scroll ========== */
const revealItems = qsa('.fade-in-up, .fade-in-right, .reveal, .card, .product-card, .news, .about-intro');
if ('IntersectionObserver' in window && revealItems.length) {
    const io = new IntersectionObserver((entries) => {
        for (const ent of entries) {
            if (ent.isIntersecting) {
                ent.target.classList.add('in-view');
                ent.target.classList.add('reveal');
                ent.target.classList.remove('fade-in-up');
                ent.target.classList.remove('fade-in-right');
                io.unobserve(ent.target);
            }
        }
    }, { threshold: 0.12 });
    revealItems.forEach(el => io.observe(el));
} else {
    revealItems.forEach(el => el.classList.add('in-view', 'reveal'));
}

/* ========== logo rotate interactivity ========== */
const logoWrap = document.getElementById('logo-wrap');
if (logoWrap) {
    logoWrap.addEventListener('click', () => {
        logoWrap.classList.add('rotate');
        setTimeout(() => logoWrap.classList.remove('rotate'), 900);
    });
}

qsa('.icon-circle img, .badge img, .hero-icon, .logo-img, .small-logo').forEach(i => {
    if (!i.style.width) i.style.width = '42px';
    if (!i.style.height) i.style.height = i.style.width;
    i.style.objectFit = 'contain';
});
