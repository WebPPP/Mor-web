$(document).ready(function () {

    function myFunction() {
        var x = document.getElementById("contact_password");
        if (x.type === "password") {
            x.type = "text";
        } else {
            x.type = "password";
        }
    }
});
