// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("input", function (event) {
    if (!event.target.classList.contains("object-details-filter")) {
        return;
    }

    const filter = event.target.value.toLowerCase().trim();
    const card = event.target.closest(".card");

    if (!card) {
        return;
    }

    const items = Array.from(
        card.querySelectorAll(".object-details-field, .object-details-header")
    );

    let currentHeaderMatches = false;

    items.forEach(function (item) {
        const name = item.dataset.fieldName?.toLowerCase() ?? "";
        const value = item.dataset.fieldValue?.toLowerCase() ?? "";

        const itemMatches =
            !filter ||
            name.includes(filter) ||
            value.includes(filter);

        if (item.classList.contains("object-details-header")) {
            currentHeaderMatches = itemMatches;
            item.classList.toggle("d-none", !itemMatches);
            return;
        }

        const showField =
            !filter ||
            itemMatches ||
            currentHeaderMatches;

        item.classList.toggle("d-none", !showField);
    });
});