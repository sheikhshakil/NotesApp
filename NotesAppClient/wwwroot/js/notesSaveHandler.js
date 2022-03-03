import getNotes from "./notesGetHandler.js";

//load all notes
document.onload = getNotes();

//get data from the forms by handling submit event
let notesForms = document.getElementsByClassName("notesForm")
for (var i = 0; i < notesForms.length; i++) {
    notesForms[i].addEventListener("submit", (event) => {
        event.preventDefault();
        disableBtns();

        const formData = new FormData(event.target); //this will data of the submitted form
        var data;

        switch (formData.get("type")) {
            case "regular": {
                data = {
                    type: formData.get("type"),
                    title: formData.get("title"),
                    text: formData.get("text")
                };
                break;
            }
            case "reminder": {
                data = {
                    type: formData.get("type"),
                    title: formData.get("title"),
                    text: formData.get("text"),
                    datetime: formData.get("datetime")
                };
                break;
            }
            case "todo": {
                data = {
                    type: formData.get("type"),
                    title: formData.get("title"),
                    text: formData.get("text"),
                    due: formData.get("due")
                };
                break;
            }
            case "bookmark": {
                data = {
                    type: formData.get("type"),
                    title: formData.get("title"),
                    url: formData.get("url")
                };
                break;
            }
        }

        postDataToServer(data);
    })
}

function postDataToServer(data) {
    if (data) {
        //validate note text for not more than 100 characters
        if (data.text) {
            if (data.text.length > 100) {
                alert("Note text should be less than or equal to 100 characters!");
                enableBtns();
                return;
            }
        }

        //POST to API
        $.ajax({
            url: "https://localhost:44345/api/saveNote",
            type: "POST",
            headers: {
                contentType: "application/json",
                Authorization: $("#tokenProvider").val()
            },
            data,
            success: () => {
                enableBtns();

                //to refresh notes list
                getNotes();

                //show toast
                var msgDiv = document.getElementById("msg");
                msgDiv.innerHTML = "Successfully saved your note.";

                var successToast = document.getElementById('liveToast');
                var toast = new bootstrap.Toast(successToast);
                toast.show();
            },
            error: (err) => {
                enableBtns();
                alert("Failed to save note!\nError - " + err.responseText);
                console.log(err);
            }
        })
    }
}

function disableBtns() {
    let saveBtns = document.getElementsByClassName("saveBtn");

    for (var i = 0; i < saveBtns.length; i++) {
        saveBtns[i].classList.add("disabled");
        saveBtns[i].innerHTML = "Saving";
    }
}

function enableBtns() {
    let saveBtns = document.getElementsByClassName("saveBtn");

    for (var i = 0; i < saveBtns.length; i++) {
        saveBtns[i].classList.remove("disabled");
        saveBtns[i].innerHTML = "Save";
    }
}