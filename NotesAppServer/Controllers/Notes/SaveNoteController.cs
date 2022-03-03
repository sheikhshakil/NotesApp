using Microsoft.AspNetCore.Mvc;
using NotesAppServer.Models;
using NotesAppServer.Repository;
using System;

namespace NotesAppServer.Controllers.Notes
{
    [ApiController]
    public class SaveNoteController : Controller
    {
        [HttpPost("api/saveNote")]
        public IActionResult Index()
        {
            string data = Request.Headers["Authorization"];

            if (Authentication.Authenticator.Authenticate(data))
            {
                //token is valid | save the note
                string type = Request.Form["type"];

                if (type.Equals("regular"))
                {
                    Note.RegularNote regularNote = new Note.RegularNote();
                    regularNote.Type = type;
                    regularNote.Id = NotesRepository.GetNotesCount() + 1;
                    regularNote.Email = Authentication.Authenticator.GetUserEmail(data);
                    regularNote.Title = Request.Form["title"];
                    regularNote.Text = Request.Form["text"];
                    regularNote.SavedOn = DateTime.Now;

                    bool isSaved = NotesRepository.SaveNote(regularNote);
                    if (isSaved)
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(500, "Internal server error!");
                    }
                }
                else if (type.Equals("reminder"))
                {
                    Note.ReminderNote reminderNote = new Note.ReminderNote();
                    reminderNote.Type = type;
                    reminderNote.Id = NotesRepository.GetNotesCount() + 1;
                    reminderNote.Email = Authentication.Authenticator.GetUserEmail(data);
                    reminderNote.Title = Request.Form["title"];
                    reminderNote.Text = Request.Form["text"];
                    reminderNote.DateTime = Convert.ToDateTime(Request.Form["datetime"]);
                    reminderNote.SavedOn = DateTime.Now;

                    bool isSaved = NotesRepository.SaveNote(reminderNote);
                    if (isSaved)
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(500, "Internal server error!");
                    }
                }
                else if (type.Equals("todo"))
                {
                    Note.TodoNote todoNote = new Note.TodoNote();
                    todoNote.Type = type;
                    todoNote.Id = NotesRepository.GetNotesCount() + 1;
                    todoNote.Email = Authentication.Authenticator.GetUserEmail(data);
                    todoNote.Title = Request.Form["title"];
                    todoNote.Text = Request.Form["text"];
                    todoNote.Due = Convert.ToDateTime(Request.Form["due"]);
                    todoNote.IsDone = false;
                    todoNote.SavedOn = DateTime.Now;

                    bool isSaved = NotesRepository.SaveNote(todoNote);
                    if (isSaved)
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(500, "Internal server error!");
                    }
                }
                else if (type.Equals("bookmark"))
                {
                    Note.BookmarkNote bookmarkNote = new Note.BookmarkNote();
                    bookmarkNote.Type = type;
                    bookmarkNote.Id = NotesRepository.GetNotesCount() + 1;
                    bookmarkNote.Email = Authentication.Authenticator.GetUserEmail(data);
                    bookmarkNote.Title = Request.Form["title"];
                    bookmarkNote.Url = Request.Form["url"];
                    bookmarkNote.SavedOn = DateTime.Now;

                    bool isSaved = NotesRepository.SaveNote(bookmarkNote);
                    if (isSaved)
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(500, "Internal server error!");
                    }
                }
            }

            return StatusCode(401, "Unauthorized access to API");
        }
    }
}
