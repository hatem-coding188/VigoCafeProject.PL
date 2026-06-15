// ===============================
//  HAMBURGER MENU
// ===============================
function toggleMenu() {
    const nav = document.querySelector('.nav-links');
    nav.classList.toggle('open');
}

// ===============================
//  TOGGLE PASSWORD
// ===============================
function togglePass(inputId, icon) {
    const input = document.getElementById(inputId);
    if (input.type === 'password') {
        input.type = 'text';
        icon.classList.replace('fa-eye', 'fa-eye-slash');
    } else {
        input.type = 'password';
        icon.classList.replace('fa-eye-slash', 'fa-eye');
    }
}

// ===============================
//  SEARCH PRODUCTS
// ===============================
function searchProducts() {
    const input = document.getElementById('searchInput').value.toLowerCase();
    const cards = document.querySelectorAll('.product-card');
    cards.forEach(card => {
        const name = card.querySelector('h3').textContent.toLowerCase();
        card.style.display = name.includes(input) ? 'block' : 'none';
    });
}

// ===============================
//  UPDATE ORDER STATUS (ADMIN)
// ===============================
function updateStatus(orderId, status) {
    window.location.href = `/Admin/UpdateOrderStatus?id=${orderId}&status=${status}`;
}

// ===============================
//  CLOSE MENU ON OUTSIDE CLICK
// ===============================
document.addEventListener('click', function (e) {
    const nav = document.querySelector('.nav-links');
    const hamburger = document.querySelector('.hamburger');
    if (nav && hamburger) {
        if (!nav.contains(e.target) && !hamburger.contains(e.target)) {
            nav.classList.remove('open');
        }
    }
});