using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace OppJar.Common.Helpers
{
    public static class FileHelper
    {
        private const string END_POINT = "/api/files";

        public static string RemoveSpecialCharacter(this string value)
        {
            var result = value.Replace(" ", "-");

            return Regex.Replace(result, @"[^0-9a-zA-Z.-]+", "");
        }

        public static string GetFileTypeByContentType(this string value)
        {
            if (value.StartsWith("video")) return "Video";

            if (value.StartsWith("audio")) return "Audio";

            return "Image";
        }

        public static void CreateFolder(ref string folder, string accountId)
        {
            try
            {
                folder = $"{AppContext.BaseDirectory}\\uploads\\{accountId}\\{DateTime.Now.Year}-{DateTime.Now.Month}";

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }
            catch
            {
                folder = null;
            }
        }

        public static void GenerateFile(ref string diskPath, ref string url, ref string fileName)
        {
            diskPath = $"{diskPath}\\{fileName}";

            url = $"{END_POINT}/{fileName}";
        }

        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void SaveFile(ref string diskPath, ref string url, ref string fileName, string rootFolder, IFormFile file)
        {
            if (file == null) return;

            CreateFolder(ref diskPath, rootFolder);

            GenerateFile(ref diskPath, ref url, ref fileName);

            using var stream = File.Create(diskPath);

            file.CopyToAsync(stream).GetAwaiter().GetResult();
        }

        public static bool CheckImageType(IFormFile file)
        {
            if (file == null) return true;

            string ext = Path.GetExtension(file.FileName);

            return (ext.ToLower()) switch
            {
                ".gif" => true,
                ".jpg" => true,
                ".jpeg" => true,
                ".png" => true,
                _ => false,
            };
        }

        public static void WriteFile(string path, string txt)
        {
            List<string> lines = new List<string>() { txt };

            if (File.Exists(path))
            {
                lines.AddRange(File.ReadAllLines(path));
            }

            using StreamWriter file = new StreamWriter(path);

            foreach (string line in lines)
            {
                file.WriteLine(line);
            }
        }
    }
}
