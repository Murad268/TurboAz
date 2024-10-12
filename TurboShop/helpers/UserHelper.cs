using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using TurboShop.models;

namespace TurboShop.helpers
{
    internal class UserHelper
    {
        private static string GetFilePath()
        {
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string folderPath = Path.Combine(projectDirectory, "db");
            string filePath = Path.Combine(folderPath, "users.xlsx");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!File.Exists(filePath))
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Users");
                    package.SaveAs(new FileInfo(filePath));
                }
            }

            return filePath;
        }

        public static List<User> ReadUsersFromExcel()
        {
            List<User> users = new List<User>();
            string filePath = GetFilePath();

            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                if (package.Workbook.Worksheets.Count == 0)
                {
                    return users;
                }

                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension?.Rows ?? 0;

                if (rowCount == 0)
                {
                    return users;
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    int id = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                    string name = worksheet.Cells[row, 2].Value?.ToString();
                    string surname = worksheet.Cells[row, 3].Value?.ToString();
                    string phone = worksheet.Cells[row, 4].Value?.ToString();
                    string email = worksheet.Cells[row, 5].Value?.ToString();
                    string password = worksheet.Cells[row, 6].Value?.ToString();

                    User user = new User(id, name, surname, phone, email, password);
                    users.Add(user);
                }
            }

            return users;
        }

        public static void WriteUsersToFile(List<User> users)
        {
            string filePath = GetFilePath();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Users");

                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Surname";
                worksheet.Cells[1, 4].Value = "Phone";
                worksheet.Cells[1, 5].Value = "Email";
                worksheet.Cells[1, 6].Value = "Password";

                for (int i = 0; i < users.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = users[i].Id;
                    worksheet.Cells[i + 2, 2].Value = users[i].Name;
                    worksheet.Cells[i + 2, 3].Value = users[i].Surname;
                    worksheet.Cells[i + 2, 4].Value = users[i].Phone;
                    worksheet.Cells[i + 2, 5].Value = users[i].Email;
                    worksheet.Cells[i + 2, 6].Value = users[i].Password;
                }

                package.SaveAs(new FileInfo(filePath));
            }
        }

        public static void UpdateUserInFile(User updatedUser)
        {
            List<User> users = ReadUsersFromExcel();  
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == updatedUser.Id)
                {
                    users[i] = updatedUser; 
                    break;
                }
            }
            WriteUsersToFile(users);  
        }
    }
}
