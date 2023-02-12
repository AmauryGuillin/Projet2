const menuHamburgerProfile = document.querySelector(
    ".menu-hamburger-profile"
);
const navLinksProfile = document.querySelector(".nav-links-profile");

menuHamburgerProfile.addEventListener("click", () => {
    navLinksProfile.classList.toggle("mobile-menu-profile");
});