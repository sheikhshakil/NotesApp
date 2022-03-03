using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NotesAppServer.Models;
using NotesAppServer.Repository;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NotesAppServer.Controllers.Accounts
{
    public class LoginController : Controller
    {
        private User _user = new User();

        [HttpPost("api/login")]
        public async Task<IActionResult> Index()
        {
            try
            {
                using (var sr = new StreamReader(Request.Body))
                {
                    //parse user data from request body
                    string body = await sr.ReadToEndAsync();

                    Hashtable hashtable = JsonConvert.DeserializeObject<Hashtable>(body);

                    // read user data from local memory static list
                    _user = UsersRepository.GetUser(hashtable["email"].ToString(), hashtable["password"].ToString());
                    if (_user != null)
                    {
                        //create jwt
                        string token = Authentication.TokenManager.GenerateToken(_user.Email);

                        //create user data var with token
                        Dictionary<string, string> userData = new Dictionary<string, string>();
                        userData.Add("fullName", _user.FullName);
                        userData.Add("email", _user.Email);
                        userData.Add("birthDate", _user.BirthDate);
                        userData.Add("token", token);

                        //convert data to json string
                        string json = JsonConvert.SerializeObject(userData);

                        return StatusCode(200, json);
                    }
                    else
                    {
                        return StatusCode(403, "Wrong credentials!");
                    }
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error!");
            }
        }
    }
}
