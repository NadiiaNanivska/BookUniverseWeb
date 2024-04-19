document.addEventListener("DOMContentLoaded", function () {
    var table = document.getElementById("myTable");
    var rows = table.getElementsByTagName("tbody")[0].getElementsByTagName("tr");
    var currentPage = 1;
    var rowsPerPage = 7;
    var totalPages = Math.ceil(rows.length / rowsPerPage);

    function showRows(page) {
        var startIndex = (page - 1) * rowsPerPage;
        var endIndex = startIndex + rowsPerPage;
        for (var i = 0; i < rows.length; i++) {
            rows[i].style.display = (i >= startIndex && i < endIndex) ? "" : "none";
        }
    }

    showRows(currentPage);

    document.getElementById("nextBtn").addEventListener("click", function () {
        if (currentPage < totalPages) {
            currentPage++;
            showRows(currentPage);
            document.getElementById("prevBtn").disabled = false;
        }
        if (currentPage === totalPages) {
            this.disabled = true;
        }
    });

    document.getElementById("prevBtn").addEventListener("click", function () {
        if (currentPage > 1) {
            currentPage--;
            showRows(currentPage);
            document.getElementById("nextBtn").disabled = false;
        }
        if (currentPage === 1) {
            this.disabled = true;
        }
    });
});
