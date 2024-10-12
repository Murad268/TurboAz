using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using OfficeOpenXml;
using TurboShop.models;

namespace TurboShop.helpers
{
    internal class BanTypesHelper
    {
        private static string GetFilePath()
        {
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string folderPath = Path.Combine(projectDirectory, "db");
            string filePath = Path.Combine(folderPath, "banTypes.xlsx");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!File.Exists(filePath))
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("BanTypes");
                    package.SaveAs(new FileInfo(filePath));
                }
            }
            return filePath;
        }

        public static List<BanType> LoadBanTypesFromFile()
        {
            List<BanType> banTypes = new List<BanType>();
            string filePath = GetFilePath();

            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                if (package.Workbook.Worksheets.Count == 0)
                {
                    return banTypes;
                }

                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension?.Rows ?? 0;

                if (rowCount == 0)
                {
                    return banTypes;
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    int id = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                    string title = worksheet.Cells[row, 2].Value?.ToString();
                    int generalTypeId = Convert.ToInt32(worksheet.Cells[row, 3].Value);

                    BanType banType = new BanType(id, title, generalTypeId);
                    banTypes.Add(banType);
                }
            }

            return banTypes;
        }

        public static void WriteBanTypesToFile(List<BanType> banTypes)
        {
            string filePath = GetFilePath();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("BanTypes");

                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Title";
                worksheet.Cells[1, 3].Value = "GeneralTypeId";

                for (int i = 0; i < banTypes.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = banTypes[i].Id;
                    worksheet.Cells[i + 2, 2].Value = banTypes[i].Title;
                    worksheet.Cells[i + 2, 3].Value = banTypes[i].GeneralTypeId;
                }

                package.SaveAs(new FileInfo(filePath));
            }
        }
    }
}
