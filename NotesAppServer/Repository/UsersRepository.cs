using NotesAppServer.Models;
using System.Collections.Generic;

namespace NotesAppServer.Repository
{
    public class UsersRepository
    {
        //local memory storage for storing users
        private static List<User> Users = new List<User>();

        public static int SaveUser(User newUser)
        {
            for (int i = 0; i < Users.Count; i++)
            {
                //search for existing account with given email
                if (newUser.Email.Equals(Users[i].Email))
                {
                    return 2;
                }
            }
            //else save the user
            Users.Add(newUser);
            return 1;
        }

        public static User GetUser(string email, string password)
        {
            for (int i = 0; i < Users.Count; i++)
            {
                //credentials matched | return the user
                if (Users[i].Email.Equals(email) && Users[i].Password.Equals(password))
                {
                    return Users[i];
                }
            }

            return null; //user not found
        }
    }
}
