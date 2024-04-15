function redirectToUserPage() {
    window.location.href = '@Url.Action("UserPage", "Home")';

}

function saveChangesInAccount() {
    var username = document.getElementById("editUsername").value.trim();
    var email = document.getElementById("editEmail").value.trim();

    if (username === "" || email === "" ) {
        alert("Please fill in all fields.");
        return;
    }
}

document.addEventListener('DOMContentLoaded', function () {
    const emailInput = document.getElementById('editEmail');
    const emailValidationMessage = document.getElementById('emailValidationMessage');
    const editUsernameInput = document.getElementById('editUsername');
    const usernameValidationMessage = document.getElementById('usernameValidationMessage');

    emailInput.addEventListener('input', function () {
        emailValidationMessage.textContent = emailInput.validity.valid ? '' : getEmailValidationError();
    });

    editUsernameInput.addEventListener('input', function () {
        usernameValidationMessage.textContent = validateUsernameFormat(editUsernameInput.value.trim()) ? '' : "Use only letters, numbers, '_', or '.'";
    });

    function getEmailValidationError() {
        return emailInput.validity.valueMissing ? 'Email is required' : 'Please enter a valid email address';
    }

    function validateUsernameFormat(username) {
        return /^[a-zA-Z][a-zA-Z0-9._]*$/.test(username);
    }
});


