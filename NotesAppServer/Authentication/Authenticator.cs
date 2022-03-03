using Newtonsoft.Json;
using System.Collections.Generic;

namespace NotesAppServer.Authentication
{
    public class Authenticator
    {
        public static bool Authenticate(string data)
        {
            Dictionary<string, string> userData = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            string decrypted_email = TokenManager.ValidateToken(userData["token"]);

            if (userData["email"].Equals(decrypted_email))
            {
                return true;
            }

            return false;
        }

        public static string GetUserEmail(string data)
        {
            Dictionary<string, string> userData = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            return TokenManager.ValidateToken(userData["token"]);
        }
    }
}
