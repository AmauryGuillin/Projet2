const description = document.querySelector("#description");
const descriptionButtons = document.querySelectorAll(".preview-description");
const descriptionTextZone = document.querySelector("#description-text");

descriptionButtons.forEach((button) => {
    button.addEventListener("click", () => {
        const descriptionText = button.textContent;
        description.style.display = "block";
        descriptionTextZone.textContent = descriptionText;
    });
});
