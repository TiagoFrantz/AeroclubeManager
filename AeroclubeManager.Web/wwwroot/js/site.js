var menuButton = document.querySelector('.menuButton')
var manuNav = document.querySelector('nav')

menuButton.addEventListener('click', () => {
    manuNav.classList.toggle('open')
    menuButton.classList.toggle('closeM')
})