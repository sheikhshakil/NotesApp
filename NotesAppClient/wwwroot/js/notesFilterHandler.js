import getNotes, { parseAndShowData } from "./notesGetHandler.js";

$("#filterNotes").on("change", function () {
    let value = this.value;

    if (value === "null") {
        return;
    }
    if (value === "all") {
        getNotes();
        return;
    }

    $.ajax({
        url: "https://localhost:44345/api/filterNotes",
        type: "POST",
        headers: {
            contentType: "application/json",
            Authorization: $("#tokenProvider").val()
        },
        data: {
            filter: value
        },
        success: (data) => {
            parseAndShowData(data);
        },
        error: (err) => {
            alert("Filter error!");
        }
    });
})
