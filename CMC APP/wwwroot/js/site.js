// cmcs.js - Core JavaScript for CMCS Project

document.addEventListener("DOMContentLoaded", function () {

    // ===========================
    // Lecturer: Auto-calculate Total Payment
    // ===========================
    const hoursInput = document.getElementById("hoursWorked");
    const rateInput = document.getElementById("hourlyRate");
    const totalInput = document.getElementById("totalPayment");

    if (hoursInput && rateInput && totalInput) {
        const calculateTotal = () => {
            const hours = parseFloat(hoursInput.value) || 0;
            const rate = parseFloat(rateInput.value) || 0;
            totalInput.value = (hours * rate).toFixed(2);
        };

        hoursInput.addEventListener("input", calculateTotal);
        rateInput.addEventListener("input", calculateTotal);
    }

    // ===========================
    // Form Validation for Lecturer Claim
    // ===========================
    const claimForm = document.querySelector("form#claimForm");
    if (claimForm) {
        claimForm.addEventListener("submit", function (e) {
            const hours = parseFloat(hoursInput.value) || 0;
            const rate = parseFloat(rateInput.value) || 0;

            if (hours <= 0 || rate <= 0) {
                e.preventDefault();
                alert("Please enter valid hours worked and hourly rate.");
                return false;
            }

            // Optional: Limit hours and rate
            if (hours > 100) {
                e.preventDefault();
                alert("Hours worked cannot exceed 100.");
                return false;
            }

            if (rate > 1000) {
                e.preventDefault();
                alert("Hourly rate cannot exceed 1000.");
                return false;
            }
        });
    }

    // ===========================
    // Coordinator: Confirm Claim Approval / Rejection
    // ===========================
    const approvalForms = document.querySelectorAll(".approveClaimForm");
    approvalForms.forEach(form => {
        form.addEventListener("submit", function (e) {
            const confirmAction = confirm("Are you sure you want to approve/reject this claim?");
            if (!confirmAction) {
                e.preventDefault();
            }
        });
    });

    // ===========================
    // HR: Confirm Report Generation
    // ===========================
    const reportButton = document.getElementById("generateReportBtn");
    if (reportButton) {
        reportButton.addEventListener("click", function (e) {
            const confirmReport = confirm("Generate report for all approved claims?");
            if (!confirmReport) {
                e.preventDefault();
            }
        });
    }

    // ===========================
    // Optional: Highlight Table Row on Hover
    // ===========================
    const claimRows = document.querySelectorAll("table tbody tr");
    claimRows.forEach(row => {
        row.addEventListener("mouseenter", () => {
            row.style.backgroundColor = "#e6f2ff";
        });
        row.addEventListener("mouseleave", () => {
            row.style.backgroundColor = "";
        });
    });

});
