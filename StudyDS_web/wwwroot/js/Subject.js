﻿document.addEventListener("DOMContentLoaded", function () {
    // Priority labels for display
    const priorityMap = {
        "0": "Niski",
        "1": "Średni",
        "2": "Wysoki"
    };

    // Update text next to the slider
    function updatePriorityValue(val) {
        document.getElementById("priorityValue").innerText = priorityMap[val] ?? val;
    }


    // Open the "Edit Subject" modal
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

    // Open the "Mark as passed" modal for subject
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

    // Sorting subject cards based on dropdown selection
    const SubjectContainer = document.getElementById("subjectsContainer");
    const SubjectCards = Array.from(SubjectContainer.querySelectorAll(".subject-card"));
    const SubjectSortSelect = document.getElementById("SubjectSortSelect");

    SubjectSortSelect.addEventListener("change", function () {
        const sortBy = this.value;

        const sorted = SubjectCards.slice().sort((a, b) => {
            const valA = a.dataset[sortBy];
            const valB = b.dataset[sortBy];

            if (sortBy === "priority" || sortBy === "grade" || sortBy === "passed") {
                return Number(valB) - Number(valA);
            }

            return 0;
        });

        SubjectContainer.innerHTML = "";
        sorted.forEach(card => SubjectContainer.appendChild(card));
    });
});
