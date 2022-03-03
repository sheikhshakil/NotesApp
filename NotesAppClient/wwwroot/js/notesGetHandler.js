export default function getNotes() {
    $.ajax({
        url: "https://localhost:44345/api/getNotes",
        type: "GET",
        headers: {
            contentType: "application/json",
            Authorization: $("#tokenProvider").val()
        },
        success: (data) => {
            parseAndShowData(data);
        },
        error: (err) => {
            alert("Read error!");
        }
    })

}

export function parseAndShowData(data) {
    $("#notesDiv").empty();
    let notes = JSON.parse(data);
    for (var i = notes.length - 1; i >= 0; --i) {
        var template;
        if (notes[i].Type === "regular") {
            template = `<div class="note-shadow mt-2">
                            <div class="row mb-1">
                                <div class="col-md-6">
                                    <div><b class="text-primary">Title : </b>${notes[i].Title}</div>
                                </div>
                                <div class="col-md-6">
                                    <div class="text-end">
                                        <button class="btn btn-primary btn-sm">${notes[i].Type}</button>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <span><b>Note text : </b>${notes[i].Text}</span>
                            </div>
                            <div>
                                <span><b>Saved on : </b>${notes[i].SavedOn}</span>
                            </div>
                        </div>`;
        }
        else if (notes[i].Type === "reminder") {
            template = `<div class="note-shadow mt-2">
                            <div class="row mb-1">
                                <div class="col-md-6">
                                    <div><b class="text-primary">Title : </b>${notes[i].Title}</div>
                                </div>
                                <div class="col-md-6">
                                    <div class="text-end">
                                        <button class="btn btn-primary btn-sm">${notes[i].Type}</button>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <span><b>Note text : </b>${notes[i].Text}</span>
                            </div>
                            <div>
                                <span><b>Reminder datetime : </b>${notes[i].DateTime}</span>
                            </div>
                            <div>
                                <span><b>Saved on : </b>${notes[i].SavedOn}</span>
                            </div>
                        </div>`;
        }

        else if (notes[i].Type === "todo") {
            template = `<div class="note-shadow mt-2">
                        <div class="row mb-1">
                            <div class="col-md-6">
                                <div><b class="text-primary">Title : </b>${notes[i].Title}</div>
                            </div>
                            <div class="col-md-6">
                                <div class="text-end">
                                    <button class="btn btn-primary btn-sm">${notes[i].Type}</button>
                                </div>
                            </div>
                        </div>
                        <div>
                            <span><b>Note text : </b>${notes[i].Text}</span>
                        </div>
                        <div>
                            <span><b>Due datetime : </b>${notes[i].Due}</span>
                        </div>
                        <div>
                            <span><b>Is done : </b>${notes[i].IsDone}</span>
                        </div>
                        <div>
                            <span><b>Saved on : </b>${notes[i].SavedOn}</span>
                        </div>
                        <div class="text-center mt-1">
                            <button value="${notes[i].Id}" class="${notes[i].IsDone === 'true' ? 'disabled' : ''} btn btn-sm btn-danger todoUpdaterBtn">Mark as done</button>
                        </div>
                        </div>`;
        }
        else if (notes[i].Type === "bookmark") {
            template = `<div class="note-shadow mt-2">
                            <div class="row mb-1">
                                <div class="col-md-6">
                                    <div><b class="text-primary">Title : </b> ${notes[i].Title}</div>
                                </div>
                                <div class="col-md-6">
                                    <div class="text-end">
                                        <button class="btn btn-primary btn-sm">${notes[i].Type}</button>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <span><b>Web URL : </b><a href="${notes[i].Url}">${notes[i].Url}</a></span>
                            </div>
                            <div>
                                <span><b>Saved on : </b>${notes[i].SavedOn}</span>
                            </div>
                        </div>`;
        }

        $("#notesDiv").append(template);
    }
}

