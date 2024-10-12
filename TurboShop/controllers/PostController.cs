using System;
using System.Collections.Generic;
using TurboShop.helpers;
using TurboShop.models;
using TurboShop.enums;
using TurboShop.validations;
namespace TurboShop.controllers
{
    internal class PostController
    {
        static List<Post> posts = PostsHelper.LoadPostsFromFile();
        static List<GeneralType> generalTypes = GeneralTypesHelper.LoadFromFile();
        static List<Brand> brands = BrandsHelper.LoadBrandsFromFile();
        static List<Model> models = ModelsHelper.LoadModelsFromFile();
        static List<BanType> banTypes = BanTypesHelper.LoadBanTypesFromFile();

        public static void Post(int userId, ref bool logged)
        {
            if (!logged)
            {
                Console.WriteLine("Əvvəlcə hesabınıza daxil olun.");
                return;
            }

            int selectedGeneralTypeId = SelectGeneralType();
            int selectedBrandId = SelectBrand(selectedGeneralTypeId);
            int selectedModelId = SelectModel(selectedBrandId);
            int selectedBanId = SelectBanType(selectedGeneralTypeId);
            string km = SelectKilometraj();
            string info = SelectInfo();
            string cityAsString = SelectCity().ToString();
            string conditionAsString = SelectCondition().ToString();
            string selectedYear = SelectYear();
            decimal price = SelectPrice();
            DateTime currentTime = DateTime.Now;
            string time = currentTime.ToString("yyyy-MM-dd HH:mm:ss");
            int order = 0;
            int id = 0;

            if (posts.Count > 0)
            {
                var lastPost = posts[posts.Count - 1];
                order = lastPost.Order + 1;
                id = lastPost.Id + 1;
            }

            Post post = new Post(
                id,
                selectedGeneralTypeId,
                selectedBrandId,
                selectedModelId,
                selectedBanId,
                cityAsString,
                conditionAsString,
                selectedYear,
                km,
                info,
                time,
                order,
                userId,
                price
            );

            posts.Add(post);
            PostsHelper.WritePostsToFile(posts);
        }

        public static void UpdatePost(int userId, ref bool logged)
        {
            if (!logged)
            {
                Console.WriteLine("Əvvəlcə hesabınıza daxil olun.");
                return;
            }

            var userPosts = posts.FindAll(p => p.UserID == userId);

            if (userPosts.Count == 0)
            {
                Console.WriteLine("Sizin heç bir postunuz yoxdur.");
                return;
            }

            Console.WriteLine("Yeniləmək istədiyiniz postu seçin:");
            PostInfoHelper.getInfo(userPosts);


            Console.WriteLine("Yeniləmək istədiyiniz postun ID-sini daxil edin:");
            string inputPostId = Console.ReadLine();
            Post post = posts.Find(p => p.Id == int.Parse(inputPostId) && p.UserID == userId);

            if (post == null)
            {
                Console.WriteLine("Bu post sizin deyil və ya mövcud deyil.");
                return;
            }

            Console.WriteLine($"Cari General Type: {post.GeneralType}");
            int selectedGeneralTypeId = SelectGeneralType(post.GeneralType);
            if (selectedGeneralTypeId != post.GeneralType)
            {
                post.GeneralType = selectedGeneralTypeId;
                post.Brand = SelectBrand(selectedGeneralTypeId);
                post.Model = SelectModel(post.Brand);
            }
            else
            {
                Console.WriteLine($"Cari Brand: {post.Brand}");
                post.Brand = SelectBrand(selectedGeneralTypeId);

                if (post.Brand != selectedGeneralTypeId)
                {
                    post.Model = SelectModel(post.Brand);
                }
                else
                {
                    Console.WriteLine($"Cari Model: {post.Model}");
                    post.Model = SelectModel(post.Brand);
                }
            }

            Console.WriteLine($"Cari Ban Type: {post.BanType}");
            post.BanType = SelectBanType(selectedGeneralTypeId);

            Console.WriteLine($"Cari Kilometraj: {post.Kilometraj}");
            string km = Console.ReadLine();
            post.Kilometraj = !string.IsNullOrEmpty(km) ? km : post.Kilometraj;

            Console.WriteLine($"Cari Info: {post.Info}");
            string info = Console.ReadLine();
            post.Info = !string.IsNullOrEmpty(info) ? info : post.Info;

            Console.WriteLine($"Cari City: {post.City}");
            post.City = SelectCity().ToString();

            Console.WriteLine($"Cari Condition: {post.Condition}");
            post.Condition = SelectCondition().ToString();

            Console.WriteLine($"Cari Year: {post.Year}");
            post.Year = SelectYear();

            Console.WriteLine($"Cari Price: {post.Price}");
            post.Price = SelectPrice();

            PostsHelper.UpdatePostToFile(post);

            Console.WriteLine("Post uğurla yeniləndi.");
        }



        public static int SelectGeneralType(int currentGeneralType = 0)
        {
            for (int i = 0; i < generalTypes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {generalTypes[i].Title}");
            }

            string input = Console.ReadLine();
            return string.IsNullOrEmpty(input) ? currentGeneralType : generalTypes[int.Parse(input) - 1].Id;
        }

        public static int SelectBrand(int generalTypeId)
        {
            for (int i = 0; i < brands.Count; i++)
            {
                if (brands[i].GeneralTypeId == generalTypeId)
                {
                    Console.WriteLine($"{i + 1}. {brands[i].Title}");
                }
            }

            string input = Console.ReadLine();
            return string.IsNullOrEmpty(input) ? generalTypeId : brands[int.Parse(input) - 1].Id;
        }

        public static int SelectModel(int brandId)
        {
            for (int i = 0; i < models.Count; i++)
            {
                if (models[i].BrandId == brandId)
                {
                    Console.WriteLine($"{i + 1}. {models[i].Title}");
                }
            }

            string input = Console.ReadLine();
            return string.IsNullOrEmpty(input) ? brandId : models[int.Parse(input) - 1].Id;
        }

        public static int SelectBanType(int generalTypeId)
        {
            for (int i = 0; i < banTypes.Count; i++)
            {
                if (banTypes[i].GeneralTypeId == generalTypeId)
                {
                    Console.WriteLine($"{i + 1}. {banTypes[i].Title}");
                }
            }

            string input = Console.ReadLine();
            return string.IsNullOrEmpty(input) ? generalTypeId : banTypes[int.Parse(input) - 1].Id;
        }

        public static AzerbaijanCities SelectCity()
        {
            Console.WriteLine("Şəhər seçin:");
            foreach (AzerbaijanCities city in Enum.GetValues(typeof(AzerbaijanCities)))
            {
                Console.WriteLine($"{(int)city}. {city}");
            }

            string input = Console.ReadLine();
            return (AzerbaijanCities)Enum.Parse(typeof(AzerbaijanCities), input);
        }

        public static ProductCondition SelectCondition()
        {
            Console.WriteLine("Vəziyyəti seçin:");
            foreach (ProductCondition condition in Enum.GetValues(typeof(ProductCondition)))
            {
                Console.WriteLine($"{(int)condition}. {condition}");
            }

            string input = Console.ReadLine();
            return (ProductCondition)Enum.Parse(typeof(ProductCondition), input);
        }

        public static string SelectYear()
        {
            Console.WriteLine("İl seçin:");
            string input = Console.ReadLine();
            return string.IsNullOrEmpty(input) ? DateTime.Now.Year.ToString() : input;
        }

        public static decimal SelectPrice()
        {
            Console.WriteLine("Qiymət daxil edin:");
            string input = Console.ReadLine();
            return string.IsNullOrEmpty(input) ? 0 : decimal.Parse(input);
        }

        public static string SelectKilometraj()
        {
            Console.WriteLine("Kilometraj daxil edin:");
            string input = Console.ReadLine();
            return input;
        }

        public static string SelectInfo()
        {
            Console.WriteLine("Məlumat daxil edin:");
            string input = Console.ReadLine();
            return input;
        }


        public static void DeletePost(int userId, ref bool logged)
        {
            if (!logged)
            {
                Console.WriteLine("Əvvəlcə hesabınıza daxil olun.");
                return;
            }

            var userPosts = posts.FindAll(p => p.UserID == userId);

            if (userPosts.Count == 0)
            {
                Console.WriteLine("Sizin heç bir postunuz yoxdur.");
                return;
            }

            Console.WriteLine("Silmək istədiyiniz postu seçin:");
            foreach (var userPost in userPosts)
            {
                Console.WriteLine($"Post ID: {userPost.Id}, General Type: {userPost.GeneralType}, Brand: {userPost.Brand}, Model: {userPost.Model}");
            }

            Console.WriteLine("Silinməsini istədiyiniz postun ID-sini daxil edin:");
            string inputPostId = Console.ReadLine();
            Post post = posts.Find(p => p.Id == int.Parse(inputPostId) && p.UserID == userId);

            if (post == null)
            {
                Console.WriteLine("Bu post sizin deyil və ya mövcud deyil.");
                return;
            }

            posts.Remove(post);
            PostsHelper.DeletePostFromFile(post.Id);

            Console.WriteLine("Post uğurla silindi.");
        }




        public static void ShowAllVehicles()
        {
            PostInfoHelper.getInfo(posts);
        }

        public static void SearchByPrice()
        {
            Console.WriteLine("Minimal qiyməti daxil edin:");
            string minPriceInput = Console.ReadLine();
            Console.WriteLine("Maksimal qiyməti daxil edin:");
            string maxPriceInput = Console.ReadLine();

            if (!Validation.Number(minPriceInput) || !Validation.Number(maxPriceInput))
            {
                Console.WriteLine("Yanlış qiymət daxil edilib.");
                return;
            }

            decimal minPrice = decimal.Parse(minPriceInput);
            decimal maxPrice = decimal.Parse(maxPriceInput);

            var filteredPosts = posts.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
            PostInfoHelper.getInfo(filteredPosts);
        }

        public static void SearchByKm()
        {
            Console.WriteLine("Minimal kilometraj daxil edin:");
            string minKmInput = Console.ReadLine();
            Console.WriteLine("Maksimal kilometraj daxil edin:");
            string maxKmInput = Console.ReadLine();

            if (!Validation.Number(minKmInput) || !Validation.Number(maxKmInput))
            {
                Console.WriteLine("Yanlış kilometraj daxil edilib.");
                return;
            }

            var filteredPosts = posts.Where(p => string.Compare(p.Kilometraj, minKmInput) >= 0 && string.Compare(p.Kilometraj, maxKmInput) <= 0).ToList();
            PostInfoHelper.getInfo(filteredPosts);
        }

        public static void SearchByBrand()
        {
            int selectedBrandId = 0;
            bool validBrand = false;

            Console.WriteLine("Mövcud Brendlər:");
            foreach (var brand in brands)
            {
                Console.WriteLine($"{brand.Id}. {brand.Title}");
            }

            while (!validBrand)
            {
                Console.WriteLine("Brend ID-sini daxil edin:");
                string input = Console.ReadLine();

                if (!Validation.Number(input))
                {
                    Console.WriteLine("Yanlış ID daxil etdiniz. Yenidən cəhd edin.");
                }
                else
                {
                    selectedBrandId = int.Parse(input);
                    var selectedBrand = brands.FirstOrDefault(b => b.Id == selectedBrandId);
                    if (selectedBrand == null)
                    {
                        Console.WriteLine("Brend tapılmadı. Yenidən cəhd edin.");
                    }
                    else
                    {
                        validBrand = true;
                    }
                }
            }

            var filteredPosts = posts.Where(p => p.Brand == selectedBrandId).ToList();
            PostInfoHelper.getInfo(filteredPosts);
        }

        public static void SearchByModel()
        {
            int selectedModelId = 0;
            bool validModel = false;

            Console.WriteLine("Mövcud Modeller:");
            foreach (var model in models)
            {
                Console.WriteLine($"{model.Id}. {model.Title}");
            }

            while (!validModel)
            {
                Console.WriteLine("Model ID-sini daxil edin:");
                string input = Console.ReadLine();

                if (!Validation.Number(input))
                {
                    Console.WriteLine("Yanlış ID daxil etdiniz. Yenidən cəhd edin.");
                }
                else
                {
                    selectedModelId = int.Parse(input);
                    var selectedModel = models.FirstOrDefault(m => m.Id == selectedModelId);
                    if (selectedModel == null)
                    {
                        Console.WriteLine("Model tapılmadı. Yenidən cəhd edin.");
                    }
                    else
                    {
                        validModel = true;
                    }
                }
            }

            var filteredPosts = posts.Where(p => p.Model == selectedModelId).ToList();
            PostInfoHelper.getInfo(filteredPosts);
        }

        public static void SearchByGeneralType()
        {
            int selectedGeneralTypeId = 0;
            bool validGeneralType = false;

            Console.WriteLine("Mövcud General Types:");
            foreach (var generalType in generalTypes)
            {
                Console.WriteLine($"{generalType.Id}. {generalType.Title}");
            }

            while (!validGeneralType)
            {
                Console.WriteLine("General Type ID-sini daxil edin:");
                string input = Console.ReadLine();

                if (!Validation.Number(input))
                {
                    Console.WriteLine("Yanlış ID daxil etdiniz. Yenidən cəhd edin.");
                }
                else
                {
                    selectedGeneralTypeId = int.Parse(input);
                    var selectedGeneralType = generalTypes.FirstOrDefault(g => g.Id == selectedGeneralTypeId);
                    if (selectedGeneralType == null)
                    {
                        Console.WriteLine("General Type tapılmadı. Yenidən cəhd edin.");
                    }
                    else
                    {
                        validGeneralType = true;
                    }
                }
            }

            var filteredPosts = posts.Where(p => p.GeneralType == selectedGeneralTypeId).ToList();
            PostInfoHelper.getInfo(filteredPosts);
        }

        public static void SearchByBanType()
        {
            int selectedBanTypeId = 0;
            bool validBanType = false;

            Console.WriteLine("Mövcud Ban Types:");
            foreach (var banType in banTypes)
            {
                Console.WriteLine($"{banType.Id}. {banType.Title}");
            }

            while (!validBanType)
            {
                Console.WriteLine("Ban Type ID-sini daxil edin:");
                string input = Console.ReadLine();

                if (!Validation.Number(input))
                {
                    Console.WriteLine("Yanlış ID daxil etdiniz. Yenidən cəhd edin.");
                }
                else
                {
                    selectedBanTypeId = int.Parse(input);
                    var selectedBanType = banTypes.FirstOrDefault(b => b.Id == selectedBanTypeId);
                    if (selectedBanType == null)
                    {
                        Console.WriteLine("Ban Type tapılmadı. Yenidən cəhd edin.");
                    }
                    else
                    {
                        validBanType = true;
                    }
                }
            }

            var filteredPosts = posts.Where(p => p.BanType == selectedBanTypeId).ToList();
            PostInfoHelper.getInfo(filteredPosts);
        }




        public static void SearchByPriceLow()
        {
            var filteredPosts = posts.OrderBy(p => p.Price).ToList();
            PostInfoHelper.getInfo(filteredPosts);
        }

        public static void SearchByPriceHigh()
        {
            var filteredPosts = posts.OrderByDescending(p => p.Price).ToList();
            PostInfoHelper.getInfo(filteredPosts);
        }

        

        public static void SearchMenu()
        {
            Console.WriteLine("Axtarış seçimlərini seçin:");
            Console.WriteLine("1. Bütün nəqliyyat elanları göstərin");
            Console.WriteLine("2. Qiymət aralığına görə axtarın");
            Console.WriteLine("3. Brendə görə axtarın");
            Console.WriteLine("4. Modelə görə axtarın");
            Console.WriteLine("5. Ən aşağı qiymətə görə sırala");
            Console.WriteLine("6. Ən yüksək qiymətə görə sırala");
            Console.WriteLine("7. Km aralığına görə axtarın");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowAllVehicles();
                    break;
                case "2":
                    SearchByPrice();
                    break;
                case "3":
                    SearchByBrand();
                    break;
                case "4":
                    SearchByModel();
                    break;
                case "5":
                    SearchByPriceLow();
                    break;
                case "6":
                    SearchByPriceHigh();
                    break;
                case "7":
                    SearchByKm();
                    break;
                default:
                    Console.WriteLine("Yanlış seçim.");
                    break;
            }
        }

    }
}
