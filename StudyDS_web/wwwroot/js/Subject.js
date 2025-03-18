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
                        updatePriorityValue(slider.value);
                        slider.addEventListener("input", function () {
                            updatePriorityValue(this.value);
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
});
