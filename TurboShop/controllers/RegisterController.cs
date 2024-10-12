using System;
using System.Collections.Generic;
using TurboShop.models;
using TurboShop.validations;
using TurboShop.helpers;
using TurboShop.enums;
namespace TurboShop.controllers
{
    internal class RegisterController
    {
        static List<User> users = UserHelper.ReadUsersFromExcel();

        public static void Register(ref bool logged)
        {
                  Console.Clear();
            if (!logged)
            {
                Console.WriteLine("Adınızı daxil edin");
                string name = Console.ReadLine();

                while (!Validation.Required(name))
                {
                    Console.WriteLine("Ad mütləq daxil edilməlidir");
                    Console.WriteLine("Adınızı daxil edin");
                    name = Console.ReadLine();
                }

                Console.WriteLine("Soy adınızı daxil edin");
                string surname = Console.ReadLine();

                while (!Validation.Required(surname))
                {
                    Console.WriteLine("Soy ad mütləq daxil edilməlidir");
                    Console.WriteLine("Soy adınızı daxil edin");
                    surname = Console.ReadLine();
                }

                Console.WriteLine("Telefon nömrənizi daxil edin");
                string phone = Console.ReadLine();

                while (!Validation.Phone(phone))
                {
                    Console.WriteLine("Telefon nömrəsi düzgün daxil edilməyib");
                    phone = Console.ReadLine();
                }

                Console.WriteLine("Elektron poçtunuzu daxil edin");
                string email = Console.ReadLine();

                while (!Validation.Email(email) || !Validation.Required(email))
                {
                    Console.WriteLine("Elektron poçt düzgün formatda deyil və ya daxil edilməyib");
                    email = Console.ReadLine();
                }

                while (!Validation.Unique(users, user => user.Email, email))
                {
                    Console.WriteLine("Bu elektron poçt ilə artıq istifadəçi mövcuddur");
                    email = Console.ReadLine();
                }

                Console.WriteLine("Şifrənizi təyin edin");
                string pass = Console.ReadLine();

                while (!Validation.Min(pass))
                {
                    Console.WriteLine("Şifrədə simvol sayı 6-dan böyük olmalıdır");
                    pass = Console.ReadLine();
                }

                Console.WriteLine("Şifrənizi yenidən daxil edin");
                string rePass = Console.ReadLine();

                while (!Validation.Some(rePass, pass))
                {
                    Console.WriteLine("Şifrələr üst-üstə düşmür");
                    rePass = Console.ReadLine();
                }

                string hashedPassword = PasswordHelper.HashPassword(pass);

                int newId = users.Count > 0 ? users[^1].Id + 1 : 1;
                User newUser = new User(newId, name, surname, phone, email, hashedPassword);

                users.Add(newUser);

                UserHelper.WriteUsersToFile(users);

                Console.WriteLine("Qeydiyyat uğurla tamamlandı. Bizə qoşulduğunuz üçün təşəkkür edirik.");
            }
            else
            {
                Console.WriteLine("Siz artıq login olmusunuz");
            }
        }
    }
}
