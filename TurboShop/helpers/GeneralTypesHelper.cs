using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using TurboShop.models;

namespace TurboShop.helpers
{
    internal class GeneralTypesHelper
    {
        private static string GetFilePath()
        {
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string folderPath = Path.Combine(projectDirectory, "db");
            string filePath = Path.Combine(folderPath, "generalTypes.xlsx");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!File.Exists(filePath))
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("GeneralTypes");
                    package.SaveAs(new FileInfo(filePath));
                }
            }

            return filePath;
        }

        public static List<GeneralType> LoadFromFile()
        {
            List<GeneralType> generalTypes = new List<GeneralType>();
            string filePath = GetFilePath();

            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                if (package.Workbook.Worksheets.Count == 0)
                {
                    return generalTypes;
                }

                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension?.Rows ?? 0;

                if (rowCount == 0)
                {
                    return generalTypes;
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    var idCell = worksheet.Cells[row, 1].Value;
                    var titleCell = worksheet.Cells[row, 2].Value;

                    if (idCell != null && titleCell != null)
                    {
                        try
                        {
                            int id = Convert.ToInt32(idCell);
                            string title = titleCell.ToString();
                            generalTypes.Add(new GeneralType(id, title));
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Düzgün format deyil: Sətir {row}, {ex.Message}");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Boş hüceyrə tapıldı: Sətir {row}");
                    }
                }
            }

            return generalTypes;
        }


        public static void WriteToFile(List<GeneralType> generalTypes)
        {
            string filePath = GetFilePath();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("GeneralTypes");

                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Title";

                for (int i = 0; i < generalTypes.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = generalTypes[i].Id;
                    worksheet.Cells[i + 2, 2].Value = generalTypes[i].Title;
                }

                package.SaveAs(new FileInfo(filePath));
            }
        }
    }
}
