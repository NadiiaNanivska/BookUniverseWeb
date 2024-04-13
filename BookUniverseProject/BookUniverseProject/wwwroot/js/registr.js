function redirectToSignUpPage() {
    var username = document.getElementById("username").value.trim();
    var email = document.getElementById("email").value.trim();
    var password = document.getElementById("password").value.trim();

    if (username === "" || email === "" || password === "") {
        alert("Please fill in all fields.");
        return;
    }

    window.location.href = '@Url.Action("HomePage", "Home")';
}