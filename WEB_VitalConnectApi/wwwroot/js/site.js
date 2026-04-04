// Función para cambiar el tema
function toggleTheme() {
    const htmlElement = document.getElementById("mainHtml");
    const currentTheme = htmlElement.getAttribute("data-bs-theme");

    // Cambiamos entre light y dark
    const newTheme = currentTheme === "light" ? "dark" : "light";
    htmlElement.setAttribute("data-bs-theme", newTheme);

    // Opcional: Guardar la preferencia en el navegador del usuario
    localStorage.setItem("theme", newTheme);
}

// Al cargar la página, verificamos si el usuario ya tenía una preferencia guardada
document.addEventListener("DOMContentLoaded", () => {
    const savedTheme = localStorage.setItem("theme");
    if (savedTheme) {
        document.getElementById("mainHtml").setAttribute("data-bs-theme", savedTheme);
    }
});