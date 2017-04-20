using System;
using System.IO;

namespace Trojan
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string currentPath = Directory.GetCurrentDirectory();

            string targetPath = currentPath + @"\Secret";
            string receivePath = currentPath + @"\Free";

            bool success = cloneDirectory(targetPath, receivePath);

            // Для пущей четкости:
            if (success) File.Create(receivePath + @"\Шалость удалась (ставь класс за отсылку).txt");
        }

        static bool cloneDirectory(string targetPath, string receivePath)
        {
            // Если папки нет, выходим из метода:
            if (!Directory.Exists(targetPath)) return false;

            // Перебираем и копируем файлы:
            foreach (string filePath in Directory.GetFiles(targetPath))
            {
                string fileName = filePath.Remove(0, filePath.LastIndexOf(@"\"));

                File.Copy(filePath, receivePath + fileName, true);
            }

            // Перебираем подпапки, клонируем их рекурсивно:
            foreach (string subDirPath in Directory.GetDirectories(targetPath))
            {
                string newReceivePath = 
                    receivePath + subDirPath.Remove(0, subDirPath.LastIndexOf(@"\"));

                Directory.CreateDirectory(newReceivePath);
                cloneDirectory(subDirPath, newReceivePath);
            }

            return true;
        }
    }
}
