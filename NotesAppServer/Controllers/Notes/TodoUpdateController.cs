using Microsoft.AspNetCore.Mvc;
using NotesAppServer.Authentication;
using NotesAppServer.Repository;

namespace NotesAppServer.Controllers.Notes
{
    [ApiController]
    public class TodoUpdateController : Controller
    {
        [HttpPost("api/todoUpdate")]
        public IActionResult Index()
        {
            try
            {
                string data = Request.Headers["Authorization"];

                if (Authenticator.Authenticate(data))
                {
                    bool isUpdated = NotesRepository.UpdateTodoStatus(Request.Form["noteId"]);
                    if (isUpdated)
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(500, "Internal server error!");
                    }
                }

                return StatusCode(401, "Unauthorized access to API!");
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
