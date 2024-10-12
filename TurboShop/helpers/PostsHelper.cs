using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using TurboShop.models;
using TurboShop.enums;
namespace TurboShop.helpers
{
    internal class PostsHelper
    {
        static List<Post> posts = PostsHelper.LoadPostsFromFile();
        static List<GeneralType> generalTypes = GeneralTypesHelper.LoadFromFile();
        static List<Brand> brands = BrandsHelper.LoadBrandsFromFile();
        static List<Model> models = ModelsHelper.LoadModelsFromFile();
        static List<BanType> banTypes = BanTypesHelper.LoadBanTypesFromFile();
        static List<User> users = UserHelper.ReadUsersFromExcel();
        private static string GetFilePath()
        {
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string folderPath = Path.Combine(projectDirectory, "db");
            string filePath = Path.Combine(folderPath, "posts.xlsx");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!File.Exists(filePath))
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Posts");
                    package.SaveAs(new FileInfo(filePath));
                }
            }

            return filePath;
        }

        public static List<Post> LoadPostsFromFile()
        {
            List<Post> posts = new List<Post>();
            string filePath = GetFilePath();

            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                if (package.Workbook.Worksheets.Count == 0)
                {
                    return posts;
                }

                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension?.Rows ?? 0;

                if (rowCount == 0)
                {
                    return posts;
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    int id = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                    int generalType = Convert.ToInt32(worksheet.Cells[row, 2].Value);
                    int brand = Convert.ToInt32(worksheet.Cells[row, 3].Value);
                    int model = Convert.ToInt32(worksheet.Cells[row, 4].Value);
                    int banType = Convert.ToInt32(worksheet.Cells[row, 5].Value);
                    string city = worksheet.Cells[row, 6].Value?.ToString();
                    string condition = worksheet.Cells[row, 7].Value?.ToString();
                    string year = worksheet.Cells[row, 8].Value?.ToString();
                    string kilometraj = worksheet.Cells[row, 9].Value?.ToString();
                    string info = worksheet.Cells[row, 10].Value?.ToString();
                    string sharingTime = worksheet.Cells[row, 11].Value?.ToString();
                    int order = Convert.ToInt32(worksheet.Cells[row, 12].Value);
                    int userId = Convert.ToInt32(worksheet.Cells[row, 13].Value);
                    decimal price = Convert.ToDecimal(worksheet.Cells[row, 14].Value);

                    posts.Add(new Post(id, generalType, brand, model, banType, city, condition, year, kilometraj, info, sharingTime, order, userId, price));
                }
            }

            return posts;
        }

        public static void WritePostsToFile(List<Post> posts)
        {
            string filePath = GetFilePath();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Posts");

                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "GeneralType";
                worksheet.Cells[1, 3].Value = "Brand";
                worksheet.Cells[1, 4].Value = "Model";
                worksheet.Cells[1, 5].Value = "BanType";
                worksheet.Cells[1, 6].Value = "City";
                worksheet.Cells[1, 7].Value = "Condition";
                worksheet.Cells[1, 8].Value = "Year";
                worksheet.Cells[1, 9].Value = "Kilometraj";
                worksheet.Cells[1, 10].Value = "Info";
                worksheet.Cells[1, 11].Value = "Sharing Time";
                worksheet.Cells[1, 12].Value = "Order";
                worksheet.Cells[1, 13].Value = "User ID";
                worksheet.Cells[1, 14].Value = "Price";

                for (int i = 0; i < posts.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = posts[i].Id;
                    worksheet.Cells[i + 2, 2].Value = posts[i].GeneralType;
                    worksheet.Cells[i + 2, 3].Value = posts[i].Brand;
                    worksheet.Cells[i + 2, 4].Value = posts[i].Model;
                    worksheet.Cells[i + 2, 5].Value = posts[i].BanType;
                    worksheet.Cells[i + 2, 6].Value = posts[i].City;
                    worksheet.Cells[i + 2, 7].Value = posts[i].Condition;
                    worksheet.Cells[i + 2, 8].Value = posts[i].Year;
                    worksheet.Cells[i + 2, 9].Value = posts[i].Kilometraj;
                    worksheet.Cells[i + 2, 10].Value = posts[i].Info;
                    worksheet.Cells[i + 2, 11].Value = posts[i].SharingTime;
                    worksheet.Cells[i + 2, 12].Value = posts[i].Order;
                    worksheet.Cells[i + 2, 13].Value = posts[i].UserID;
                    worksheet.Cells[i + 2, 14].Value = posts[i].Price;
                }

                package.SaveAs(new FileInfo(filePath));
            }
        }

        public static void UpdatePostToFile(Post updatedPost)
        {
            List<Post> posts = LoadPostsFromFile();

            var existingPost = posts.Find(p => p.Id == updatedPost.Id);

            if (existingPost != null)
            {
                existingPost.GeneralType = updatedPost.GeneralType;
                existingPost.Brand = updatedPost.Brand;
                existingPost.Model = updatedPost.Model;
                existingPost.BanType = updatedPost.BanType;
                existingPost.City = updatedPost.City;
                existingPost.Condition = updatedPost.Condition;
                existingPost.Year = updatedPost.Year;
                existingPost.Kilometraj = updatedPost.Kilometraj;
                existingPost.Info = updatedPost.Info;
                existingPost.SharingTime = updatedPost.SharingTime;
                existingPost.Order = updatedPost.Order;
                existingPost.Price = updatedPost.Price;

                WritePostsToFile(posts);
            }
        }

        public static void DeletePostFromFile(int postId)
        {
            List<Post> posts = LoadPostsFromFile();

            Post postToDelete = posts.Find(p => p.Id == postId);

            if (postToDelete != null)
            {
                posts.Remove(postToDelete);
                WritePostsToFile(posts); 
            }
        }

        public static string GetGeneralType(int generalTypeId)
        {
            var generalType = generalTypes.Find(g => g.Id == generalTypeId);
            return generalType != null ? generalType.Title : "Unknown General Type";
        }

        public static string GetBrand(int brandId)
        {
            var brand = brands.Find(b => b.Id == brandId);
            return brand != null ? brand.Title : "Unknown Brand";
        }

        public static string GetModel(int modelId)
        {
            var model = models.Find(m => m.Id == modelId);
            return model != null ? model.Title : "Unknown Model";
        }

        public static string GetCity(string cityCode)
        {
            if (Enum.TryParse(cityCode, out AzerbaijanCities city))
            {
                return city.ToString();
            }
            return "Unknown City";
        }

        public static string GetCondition(string conditionCode)
        {
            if (Enum.TryParse(conditionCode, out ProductCondition condition))
            {
                return condition.ToString();
            }
            return "Unknown Condition";
        }
        public static string GetUserFullName(int userId)
        {
            var user = users.Find(u => u.Id == userId);
            return user != null ? $"{user.Name} {user.Surname}" : "Unknown User";
        }

        public static string GetUserPhone(int userId)
        {
            var user = users.Find(u => u.Id == userId);
            return user != null ? user.Phone : "Unknown Phone Number";
        }


        public static string GetBanType(int banTypeId)
        {
            var banType = banTypes.Find(bt => bt.Id == banTypeId);
            return banType != null ? banType.Title : "Ban Type tapılmadı";
        }
    }
}
