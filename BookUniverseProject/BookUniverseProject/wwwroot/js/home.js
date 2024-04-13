document.addEventListener("DOMContentLoaded", function () {
    var table = document.getElementById("myTable");
    var rows = table.getElementsByTagName("tr");
    for (var i = 1; i < rows.length; i++) { // Start from index 1 to skip header row
        rows[i].addEventListener("click", function () {
            // Handle row click event
            // For example, you can get the data of the clicked row and perform actions
            var cells = this.getElementsByTagName("td");
            var rowData = {
                title: cells[0].innerText,
                author: cells[1].innerText,
                numberOfPages: cells[2].innerText,
                rating: cells[3].innerText
            };
            console.log("Clicked row data:", rowData);
            // Add your desired actions here
        });
    }
});

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
