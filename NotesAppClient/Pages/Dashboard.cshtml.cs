using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;

namespace NotesAppClient.Pages
{
    public class DashboardModel : PageModel
    {
        public List<Dictionary<string, string>> Notes { get; set; }

        public DashboardModel()
        {
            Notes = new List<Dictionary<string, string>>();
        }

        public void OnGet()
        {
            string userData = HttpContext.Session.GetString("userData");

            if (userData != null)
            {
                ViewData["userData"] = userData;
            }
            else
            {
                Response.Redirect("/");
            }
        }

        public void OnGetLogout()
        {
            HttpContext.Session.Remove("userData");
            Response.Redirect("/");
        }
    }
}
