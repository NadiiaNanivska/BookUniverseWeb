function redirectToSignInPage() {
    var username = document.getElementById("username").value.trim();
    var password = document.getElementById("password").value.trim();

    if (username === "" || password === "") {
        alert("Please fill in all fields.");
        return;
    }

    window.location.href = '@Url.Action("HomePage", "Home")';
}