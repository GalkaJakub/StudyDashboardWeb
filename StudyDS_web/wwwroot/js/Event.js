const priorityMap = {
    "0": "Niski",
    "1": "Średni",
    "2": "Wysoki"
};

function updatePriorityValue(val) {
    document.getElementById("priorityValue").innerText = priorityMap[val] ?? val;
}

document.addEventListener("DOMContentLoaded", function () {
    const slider = document.getElementById("priorityRange");
    if (slider) {
        updatePriorityValue(slider.value);
        slider.addEventListener("input", function () {
            updatePriorityValue(this.value);
        });
    }

    document.querySelectorAll(".description-preview").forEach(desc => {
        const id = desc.id.split("-")[1];
        const toggleBtn = document.querySelector(`.show-description[data-id="${id}"]`);
        if (desc.scrollHeight > desc.clientHeight && toggleBtn) {
            toggleBtn.classList.remove("d-none");
        }
    });

    document.querySelectorAll(".show-description").forEach(button => {
        button.addEventListener("click", function () {
            const name = this.dataset.name;
            const desc = this.dataset.description;

            const modalTitle = document.getElementById("descriptionModalTitle");
            const modalBody = document.getElementById("descriptionModalBody");

            modalTitle.textContent = `Opis: ${name}`;
            modalBody.textContent = desc || "Brak opisu.";
        });
    });

    const editButtons = document.querySelectorAll(".edit-event-btn");

    editButtons.forEach(button => {
        button.addEventListener("click", function (e) {
            e.preventDefault();
            const eventId = this.dataset.id;

            fetch(`/Events/UpdateEv?eventId=${eventId}`, {
                headers: {
                    "X-Requested-With": "XMLHttpRequest"
                }
            })
                .then(response => response.text())
                .then(html => {
                    const modalContent = document.getElementById("editEventModalContent");
                    modalContent.innerHTML = html;

                    const slider = modalContent.querySelector("#priorityRange");
                    const priorityText = modalContent.querySelector("#priorityValue");

                    if (slider && priorityText) {
                        priorityText.innerText = priorityMap[slider.value] ?? slider.value;
                        slider.addEventListener("input", function () {
                            priorityText.innerText = priorityMap[this.value] ?? this.value;
                        });
                    }

                    const modal = new bootstrap.Modal(document.getElementById("editEventModal"));
                    modal.show();
                });
        });
    });

    const editPassButtons = document.querySelectorAll(".edit-pass-event-btn");
    editPassButtons.forEach(button => {
        button.addEventListener("click", function (e) {
            e.preventDefault();
            const eventId = this.dataset.id;

            fetch(`/Events/UpdatePassEv?eventId=${eventId}`, {
                headers: {
                    "X-Requested-With": "XMLHttpRequest"
                }
            })
                .then(response => response.text())
                .then(html => {
                    const modalContent = document.getElementById("editPassEventModalContent");
                    modalContent.innerHTML = html;

                    const modal = new bootstrap.Modal(document.getElementById("editPassEventModal"));
                    modal.show();
                });
        });
    });

    const container = document.getElementById("eventsContainer");
    const cards = Array.from(container.querySelectorAll(".event-card"));
    const sortSelect = document.getElementById("sortSelect");

    sortSelect.addEventListener("change", function () {
        const sortBy = this.value;

        const sorted = cards.slice().sort((a, b) => {
            const valA = a.dataset[sortBy];
            const valB = b.dataset[sortBy];

            if (sortBy === "date") {
                return new Date(valA) - new Date(valB);
            }
            if (sortBy === "priority" || sortBy === "grade" || sortBy === "passed") {
                return Number(valB) - Number(valA);
            }

            return 0;
        });

        container.innerHTML = "";
        sorted.forEach(card => container.appendChild(card));
    });

});
