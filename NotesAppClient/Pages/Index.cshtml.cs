using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NotesAppClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string userData = HttpContext.Session.GetString("userData");
            ViewData["userData"] = userData;
            if (userData != null)
            {
                Response.Redirect("/dashboard");
            }
        }

        public async Task<IActionResult> OnPostLogin()
        {
            JObject jsonData = new JObject();
            jsonData.Add("email", (string)Request.Form["email"]);
            jsonData.Add("password", (string)Request.Form["password"]);

            string url = Configs.Configs.apiEndpointRoot + "login";

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync(url, new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json"));

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //login success
                        //create session with jwt access token
                        string userData = await response.Content.ReadAsStringAsync();
                        HttpContext.Session.SetString("userData", userData);
                        return Redirect("/dashboard");
                    }
                    else
                    {
                        return StatusCode(403, "Login failed! Check your credentials.");
                    }
                }
                catch
                {
                    return StatusCode(500, "Login failed! Internal Server error.");
                }
            }
        }
    }
}
