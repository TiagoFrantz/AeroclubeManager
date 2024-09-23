let sidebarTextParent = document.querySelector(".asideHeader__data");
let sidebarText = document.querySelector(".asideHeader__data__schoolName");

let menuButton = document.querySelector(".menuButton");
let sidebar = document.querySelector("aside");
let sidebarOverlay = document.querySelector(".asideOverlay");

let sidebarAberta = false;

fecharSidebar()

function abrirSidebar() {
    sidebar.classList.add("open")
    sidebarOverlay.classList.add("open")
    menuButton.classList.add("close")
    sidebarAberta = true;
}

function fecharSidebar() {
    sidebar.classList.remove("open")
    sidebarOverlay.classList.remove("open")
    menuButton.classList.remove("close")
    sidebarAberta = false;
}

menuButton.addEventListener("click", () => {
    if (sidebarAberta) {
        fecharSidebar()
        return
    }

    abrirSidebar()
})

document.addEventListener("click", function (event) {
    if (sidebar.contains(event.target) == false && menuButton.contains(event.target) == false) {
        fecharSidebar()
    }
})

document.addEventListener("DOMContentLoaded", () => {
    verifyWidth()


})


window.addEventListener("resize", verifyWidth)


function verifyWidth() {
    if (sidebarText.clientWidth > sidebarTextParent.clientWidth) {
        sidebarText.classList.add("anime");
        return
    }

    sidebarText.classList.remove("anime");

}


setTimeout(function () {
    verifyWidth()
}, 1000)