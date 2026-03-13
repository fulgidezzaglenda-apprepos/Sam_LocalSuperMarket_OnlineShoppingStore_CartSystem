// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

///Add an event listener to the window that will run once it finished loading.
window.addEventListener("load", () => {
    //Find the element called btnTheme and add a click event listener to it.
    document.getElementById("btnTheme").addEventListener('click', () => switchTheme());
})

//Method that changes our colour theme
async function switchTheme() {
    //Get our current theme setting form the localstorage of the PC. If it is null set it to Dark.
    let currentTheme = localStorage.getItem("Theme") || "Light"
    var url = window.location.origin;

    if (currentTheme == "Dark") {

        localStorage.setItem("Theme", "Light")


        var result = await fetch(url + "/api/ThemeSetting/SetTheme", {
            method: "POST",
            headers: {
                "content-type": "application/json"
            },
            body: JSON.stringify({ Theme: "Light" })
        })

        document.getElementById("themeStyle").setAttribute("href", "/css/themes/light-theme.css")
    }
    else {
        localStorage.setItem("Theme", "Dark")

        var rersult = await fetch(url + "/api/ThemeSetting/SetTheme", {
            method: "POST",
            headers: {
                "content-type": "application/json"
            },
            body: JSON.stringify({ Theme: "Dark" })
        })

        document.getElementById("themeStyle").setAttribute("href", "/css/themes/dark-theme.css")
    }
}
