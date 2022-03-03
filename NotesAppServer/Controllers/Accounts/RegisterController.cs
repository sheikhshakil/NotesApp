using Microsoft.AspNetCore.Mvc;
using NotesAppServer.Models;
using NotesAppServer.Repository;
using System.ComponentModel.DataAnnotations;

namespace NotesAppServer.Controllers.Accounts
{
    [ApiController]
    public class RegisterController : Controller
    {
        private User _user = new User();

        [HttpPost("api/register")]
        public IActionResult Index()
        {
            _user.FullName = Request.Form["fullName"];
            _user.BirthDate = Request.Form["birthDate"];
            _user.Email = Request.Form["email"];
            _user.Password = Request.Form["password"];

            //validate
            if (_user.FullName.Length <= 0 || _user.BirthDate.Length <= 0 || !new EmailAddressAttribute().IsValid(_user.Email) || _user.Password.Length < 5)
            {
                // if invalid return with bad req error
                return StatusCode(400, "All input values are not valid! Please enter all the values correctly.");
            }

            //save user data in local memory
            int status = UsersRepository.SaveUser(_user);

            if (status == 1)
            {
                // save success
                return Ok();
            }
            else if (status == 2)
            {
                //user already exists with given email
                return StatusCode(409, "User already exists!");
            }
            else
            {
                //some error occured in server while saving
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
