document.addEventListener("DOMContentLoaded", function () {
    const priorityMap = {
        "0": "Niski",
        "1": "Średni",
        "2": "Wysoki"
    };

    function updatePriorityValue(val) {
        document.getElementById("priorityValue").innerText = priorityMap[val] ?? val;
    }

    const editButtons = document.querySelectorAll(".edit-subject-btn");

    editButtons.forEach(button => {
        button.addEventListener("click", function (e) {
            e.preventDefault();
            const subjectId = this.dataset.id;

            fetch(`/Subjects/UpdateSub?subjectId=${subjectId}`, {
                headers: {
                    "X-Requested-With": "XMLHttpRequest"
                }
            })
                .then(response => response.text())
                .then(html => {
                    const modalContent = document.getElementById("editSubjectModalContent");
                    modalContent.innerHTML = html;

                    const slider = modalContent.querySelector("#priorityRange");
                    const priorityText = modalContent.querySelector("#priorityValue");

                    if (slider && priorityText) {
                        priorityText.innerText = priorityMap[slider.value] ?? slider.value;
                        slider.addEventListener("input", function () {
                            priorityText.innerText = priorityMap[this.value] ?? this.value;
                        });
                    }

                    const modal = new bootstrap.Modal(document.getElementById("editSubjectModal"));
                    modal.show();
                });
        });
    });

    const editPassButtons = document.querySelectorAll(".edit-pass-subject-btn");

    editPassButtons.forEach(button => {
        button.addEventListener("click", function (e) {
            e.preventDefault();
            const subjectId = this.dataset.id;

            fetch(`/Subjects/PassSub?subjectId=${subjectId}`, {
                headers: {
                    "X-Requested-With": "XMLHttpRequest"
                }
            })
                .then(response => response.text())
                .then(html => {
                    const modalContent = document.getElementById("editPassSubjectModalContent");
                    modalContent.innerHTML = html;

                    const modal = new bootstrap.Modal(document.getElementById("editPassSubjectModal"));
                    modal.show();
                });
        });
    });

    const container = document.getElementById("subjectsContainer");
    const cards = Array.from(container.querySelectorAll(".subject-card"));
    const sortSelect = document.getElementById("sortSelect");

    sortSelect.addEventListener("change", function () {
        const sortBy = this.value;

        const sorted = cards.slice().sort((a, b) => {
            const valA = a.dataset[sortBy];
            const valB = b.dataset[sortBy];

            if (sortBy === "priority" || sortBy === "grade" || sortBy === "passed") {
                return Number(valB) - Number(valA);
            }

            return 0;
        });

        container.innerHTML = "";
        sorted.forEach(card => container.appendChild(card));
    });
});
