using System;
using System.Collections.Generic;
using TurboShop.models;
using TurboShop.helpers;
using TurboShop.controllers;
public class ProfileHelper
{
    public static void getInfo(int userId, ref bool logged)
    {
        Console.Clear();
        if (!logged)
        {
            Console.WriteLine("Əvvəlcə hesabınıza daxil olmalısınız.");
            return;
        }

        List<User> users = UserHelper.ReadUsersFromExcel();
        List<Post> posts = PostsHelper.LoadPostsFromFile();

        foreach (User user in users)
        {
            if (user.Id == userId)
            {
                Console.WriteLine($"" +
                    $"Ad: {user.Name} \n" +
                    $"Soyad: {user.Surname} \n" +
                    $"Elektron poçt: {user.Email} \n" +
                    $"Məlumatlarınızı yeniləmək üçün - 11 daxil edin \n" +
                    $"Öz postlarınızı yeniləmək üçün - 12 daxil edin \n" +
                    $"Öz postlarınızdan hər hansısa postu dilmək üçün - 13 daxil edin \n" +
                    $"Geriye dönmək üçün entere basın");
                var selVariant = Console.ReadLine();
                if (selVariant == "11")
                {
                    ProfileController.UpdateProfile(userId, ref logged);
                }
                if (selVariant == "12") {
                    PostController.UpdatePost(userId,  ref logged);
                }
                else if (selVariant == "13")
                {
                    PostController.DeletePost(userId, ref logged);
                }

            }
        }
    }
}
