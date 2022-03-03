using Microsoft.AspNetCore.Mvc;
using NotesAppServer.Authentication;
using NotesAppServer.Repository;

namespace NotesAppServer.Controllers.Notes
{
    [ApiController]
    public class GetNotesController : Controller
    {
        [HttpGet("api/getNotes")]
        public IActionResult Index()
        {
            try
            {
                string data = Request.Headers["Authorization"];

                if (Authenticator.Authenticate(data))
                {
                    //return data
                    string notes = NotesRepository.GetNotes(Authenticator.GetUserEmail(data));

                    return Content(notes);
                }

                return StatusCode(401, "Unauthorized access to API!");
            }
            catch
            {
                return StatusCode(500, "Internal server error!");
            }
        }
    }
}
