using OfficeOpenXml;
using System.IO;
using System.Text;
using TurboShop.controllers;
using TurboShop.helpers;
using TurboShop.models;
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        List<Post> posts = PostsHelper.LoadPostsFromFile();
        bool logged = false;
        int userId = 0;

    Start:
        MessagesHelper.Messages(ref logged);
        var selectedVariant = Console.ReadLine();

        switch (selectedVariant)
        {
            case "1":
                RegisterController.Register(ref logged);
                goto Start;
            case "2":
                logged = LoginController.Login(ref userId, ref logged);
                goto Start;
            case "3":
                PostInfoHelper.getInfo(posts);
                goto Start;
            case "5":
                ProfileHelper.getInfo(userId, ref logged);
                goto Start;
            case "6":
                PostController.Post(userId, ref logged);
                goto Start;
            case "10":
                logged = false;
                goto Start;
            case "22":
                PostController.SearchMenu();
                goto Start;
            case "0":
                Console.WriteLine("Proqram bitir.");
                return; 
            default:
                Console.WriteLine("Yanlış seçim. Yenidən cəhd edin.");
                goto Start;
        }
    }
}

