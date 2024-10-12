using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using TurboShop.models;

namespace TurboShop.helpers
{
    internal class ModelsHelper
    {
        private static string GetFilePath()
        {
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string folderPath = Path.Combine(projectDirectory, "db");
            string filePath = Path.Combine(folderPath, "models.xlsx");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!File.Exists(filePath))
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Models");
                    package.SaveAs(new FileInfo(filePath));
                }
            }

            return filePath;
        }

        public static List<Model> LoadModelsFromFile()
        {
            List<Model> models = new List<Model>();
            string filePath = GetFilePath();

            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                if (package.Workbook.Worksheets.Count == 0)
                {
                    return models;
                }

                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension?.Rows ?? 0;

                if (rowCount == 0)
                {
                    return models;
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    int id = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                    string title = worksheet.Cells[row, 2].Value?.ToString();
                    int brandId = Convert.ToInt32(worksheet.Cells[row, 3].Value);

                    models.Add(new Model(id, title, brandId));
                }
            }

            return models;
        }

        public static void WriteModelsToFile(List<Model> models)
        {
            string filePath = GetFilePath();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Models");

                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Title";
                worksheet.Cells[1, 3].Value = "BrandId";

                for (int i = 0; i < models.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = models[i].Id;
                    worksheet.Cells[i + 2, 2].Value = models[i].Title;
                    worksheet.Cells[i + 2, 3].Value = models[i].BrandId;
                }

                package.SaveAs(new FileInfo(filePath));
            }
        }
    }
}
