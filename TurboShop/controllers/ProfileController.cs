using System;
using System.Collections.Generic;
using TurboShop.helpers;
using TurboShop.models;
using TurboShop.validations;

namespace TurboShop.controllers
{
    internal class ProfileController
    {
        static List<User> users = UserHelper.ReadUsersFromExcel();

        public static void UpdateProfile(int userId, ref bool logged)
        {
            Console.Clear();
            if (!logged)
            {
                Console.WriteLine("Evvelce login olunmalısınız.");
                return;
            }

            User user = users.Find(u => u.Id == userId);
            if (user == null)
            {
                Console.WriteLine("İstifadəçi tapılmadı.");
                return;
            }

            Console.WriteLine($"Cari adınız: {user.Name}");
            Console.WriteLine("Yeni adınızı daxil edin (Enter basınsa dəyişməz qalacaq):");
            string name = Console.ReadLine();
            user.Name = !string.IsNullOrEmpty(name) ? name : user.Name;

            Console.WriteLine($"Cari soyadınız: {user.Surname}");
            Console.WriteLine("Yeni soyadınızı daxil edin (Enter basınsa dəyişməz qalacaq):");
            string surname = Console.ReadLine();
            user.Surname = !string.IsNullOrEmpty(surname) ? surname : user.Surname;

            Console.WriteLine($"Cari email: {user.Email}");
            Console.WriteLine("Yeni emailinizi daxil edin (Enter basınsa dəyişməz qalacaq):");
            string email = Console.ReadLine();
            if (!string.IsNullOrEmpty(email))
            {
                while (!Validation.Email(email) || !Validation.Unique(users, u => u.Email, email))
                {
                    Console.WriteLine("Düzgün formatda olmayan və ya artıq istifadə edilən email.");
                    email = Console.ReadLine();
                }
                user.Email = email;
            }

            Console.WriteLine("Yeni şifrənizi daxil edin (Enter basınsa dəyişməz qalacaq):");
            string pass = Console.ReadLine();
            if (!string.IsNullOrEmpty(pass))
            {
                while (!Validation.Min(pass))
                {
                    Console.WriteLine("Şifrədə simvol sayı 6-dan böyük olmalıdır.");
                    pass = Console.ReadLine();
                }
                string hashedPassword = PasswordHelper.HashPassword(pass);
                user.Password = hashedPassword;
            }

            UserHelper.UpdateUserInFile(user);

            Console.WriteLine("Profiliniz uğurla yeniləndi.");
        }
    }
}
