using System;
using TurboShop.models;

namespace TurboShop.helpers
{
    internal static class PostInfoHelper
    {
        public static void getInfo(List<Post> posts)
        {
            foreach (var post in posts)
            {
                Console.WriteLine($"Post ID: {post.Id}");
                Console.WriteLine($"Ümumi Növ: {PostsHelper.GetGeneralType(post.GeneralType)}");
                Console.WriteLine($"Marka: {PostsHelper.GetBrand(post.Brand)}");
                Console.WriteLine($"Model: {PostsHelper.GetModel(post.Model)}");
                Console.WriteLine($"Ban Növü: {PostsHelper.GetBanType(post.BanType)}");
                Console.WriteLine($"Şəhər: {PostsHelper.GetCity(post.City)}");
                Console.WriteLine($"Vəziyyət: {PostsHelper.GetCondition(post.Condition)}");
                Console.WriteLine($"İl: {post.Year}");
                Console.WriteLine($"Kilometraj: {post.Kilometraj}");
                Console.WriteLine($"Məlumat: {post.Info}");
                Console.WriteLine($"Paylaşma Tarixi: {post.SharingTime}");
                Console.WriteLine($"Sıra: {post.Order}");
                Console.WriteLine($"Qiymət: {post.Price}");
                Console.WriteLine($"Satıcı Adı: {PostsHelper.GetUserFullName(post.UserID)}");
                Console.WriteLine($"Satıcı Telefonu: {PostsHelper.GetUserPhone(post.UserID)}");
                Console.WriteLine("-------------------------------");
            }
        }
    }
}
