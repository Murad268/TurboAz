using System;
using System.Collections.Generic;
using TurboShop.models;
using TurboShop.helpers;

namespace TurboShop.controllers
{
    internal class LoginController
    {
        static List<User> users = UserHelper.ReadUsersFromExcel();

        public static bool Login(ref int userId, ref bool logged)
        {
            Console.Clear();
           if(!logged)
            {
                Console.WriteLine("Elektron poçtunuzu daxil edin");
                string email = Console.ReadLine();
                Console.WriteLine("Şifrənizi daxil edin");
                string pass = Console.ReadLine();

                foreach (User user in users)
                {
                    if (user.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && user.Password == PasswordHelper.HashPassword(pass))
                    {
                        userId = user.Id;
                        return true;
                    }
                }

                Console.WriteLine("Email və ya şifrə yanlışdır");
                return false;
            } else
            {
                Console.WriteLine("Siz artıq login olmusuz");

                return true;
            }
        }
    }
}
