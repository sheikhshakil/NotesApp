//handle reg form submission
$("#regForm").submit((event) => {
    event.preventDefault();

    let userData = {
        fullName: $("#fullName").val().trim(),
        birthDate: $("#birthDate").val(),
        email: $("#regEmail").val().trim(),
        password: $("#regPass").val(),
    }

    if (userData.fullName.length > 0 && userData.birthDate && userData.email && userData.password.length >= 5) {
        let regBtn = document.getElementById("regBtn");
        regBtn.classList.add("disabled");
        regBtn.innerHTML = "Processing";

        //send ajax post req
        $.ajax({
            url: "https://localhost:44345/api/register",
            type: "POST",
            headers: {
                contentType: "application/json",
            },
            data: userData,
            success: () => {
                regBtn.classList.remove("disabled");
                regBtn.innerHTML = "Register";

                //show toast
                var successToast = document.getElementById('liveToast');
                var toast = new bootstrap.Toast(successToast);
                toast.show();
            },
            error: (err) => {
                //restore btn state
                regBtn.classList.remove("disabled");
                regBtn.innerHTML = "Register";

                alert("Failed to register!\nError - " + err.responseText);
                console.log(err);
            }
        })
    }
    else {
        alert("All fields are required along with a Password of minimum 5 characters!");
    }
})