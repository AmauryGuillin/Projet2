const description = document.querySelector("#pop-up-description");
const descriptionButtons = document.querySelectorAll(".preview-description");
const descriptionTextZone = document.querySelector(
    "#pop-up-description-zone-text"
);
const closeButton = document.querySelector("#pop-up-close-button");

descriptionButtons.forEach((button) => {
    button.addEventListener("click", () => {
        const descriptionText = button.textContent;
        description.style.display = "block";
        descriptionTextZone.textContent = descriptionText;
    });
});

document.getElementById("pop-up-close-button").onclick = function () {
    description.style.display = "none";
};