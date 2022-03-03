import getNotes from "./notesGetHandler.js";

$("#notesDiv").on("click", ".todoUpdaterBtn", function () {
    let noteId = $(this).val();

    //POST to API
    $.ajax({
        url: "https://localhost:44345/api/todoUpdate",
        type: "POST",
        headers: {
            contentType: "application/json",
            Authorization: $("#tokenProvider").val()
        },
        data: {
            noteId
        },
        success: () => {
            //to refresh notes list
            getNotes();

            //show toast
            var msgDiv = document.getElementById("msg");
            msgDiv.innerHTML = "Successfully marked as done.";

            var successToast = document.getElementById('liveToast');
            var toast = new bootstrap.Toast(successToast);
            toast.show();
        },
        error: (err) => {
            alert(err);
            console.log(err);
        }
    })
})