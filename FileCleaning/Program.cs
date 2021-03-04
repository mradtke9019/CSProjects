using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Extensions;
using FileSystem;

namespace FileCleaning
{
    class Program
    {
        public static List<string> videoExtensions = new List<string>() { ".mkv", ".mp4", ".avi", ".wmv", ".srt", ".m4v" };
        public static List<string> keywords;
        public static List<string> dirs;

        static void Main(string[] args)
        {
            keywords = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/keywords.txt").Replace("\r", "").Split('\n').OrderByDescending(x => x.Length).ToList();

            //Utility.DirectoryRun("D:\\Videos", CleanDirectory, CleanFile, true);
            //Utility.DirectoryRun("H:\\Videos\\Movies", null, MoveFileToParent, false);
            //FileSystem.Utility.DirectoryRun("D:\\Videos\\Movies", CleanDirectory, CleanFile, true);
            //FileSystem.Utility.DirectoryRun("D:\\Videos\\TV", CleanDirectory, CleanFile, true);
            var flaglessArgs = args.Where(x => !x.Contains("-"))?.ToArray();
            FileSystem.Utility.DirectoryRun(flaglessArgs.Length> 0 ? flaglessArgs[0] : Directory.GetCurrentDirectory(), CleanDirectory, CleanFile, args.Length > 0 && args.Contains("-r") ? true : false );


            //CleanDirectory("D:\\Videos", videoExtensions, keywords);
            //MoveFileToParent("D:\\Videos\\Movies\\Monsters Inc.mp4");
        }

        public static void AddDirectory(string path)
        {
            var dir = path.Split('\\').Last();
            if(dir.Contains("  ") || (dir.Contains(".") && !dir.Contains(" .") && !dir.Contains(". ") && !dir.Contains(" . ")))
                dirs.Add(dir);
        }


        public static string CleanDirectory(string path)
        {
            string newPath = path.Strip(keywords).StripPeriod().TrimSpace().Trim();
            string prevPath = null;
            while (true)
            {
                // Keep stripping keywords until they are all gone
                if (newPath == prevPath)
                    break;
                else
                    newPath = newPath.Strip(keywords).StripPeriod().TrimSpace().Trim();
                prevPath = newPath;
            }
            if (newPath != path)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(path.GetIndentation() + path);
                Directory.Move(path, newPath);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(path.GetIndentation() + path);
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            return newPath;
        }

        public static string CleanFile(string path)
        {
            if (!File.Exists(path))
                return path;

            string extension = string.Empty;
            int extensionIndex = -1;
            foreach (var type in videoExtensions)
            {
                extensionIndex = path.LastIndexOf(type);
                if (extensionIndex < 0 || extensionIndex + type.Length != path.Length)
                    continue;
                extension = path.Substring(path.LastIndexOf(type));
                break;
            }

            if (extensionIndex < 0)
                return path;

            //Remove the extension, clean the name, then readd the extension
            string dest = path.Remove(extensionIndex).Strip(keywords).TrimSpace().Trim();
            string prevName = null;
            // Keep stripping keywords until the file isnt changing
            while (true)
            {
                if (dest == prevName)
                    break;
                else
                    dest = dest.Strip(keywords).TrimSpace().Trim();
                prevName = dest;
            }

            dest += extension;
            // Dont allow File.Move if the name has not changed. Exception will be thrown
            if (dest == path)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(dest.GetIndentation() + dest);
                Console.ForegroundColor = ConsoleColor.Gray;
                return dest;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(dest.GetIndentation() + dest);
            File.Move(path, dest);

            return dest;
        }

        public static string MoveFileToParent(string filePath)
        {
            if (!File.Exists(filePath))
                return filePath;
            
            string extension = string.Empty;
            int extensionIndex = -1;
            foreach (var type in videoExtensions)
            {
                extensionIndex = filePath.LastIndexOf(type);
                if (extensionIndex < 0 || extensionIndex + type.Length != filePath.Length)
                    continue;
                extension = filePath.Substring(filePath.LastIndexOf(type));
                break;
            }

            if (extensionIndex < 0)
                return filePath;

            //Remove the extension, clean the name, then readd the extension
            string dest = filePath.Remove(extensionIndex).Strip(keywords).TrimSpace().Trim().Replace("\\ ", "\\");
            var baseName = filePath.Split('\\').Last();
            // Dont allow File.Move if the name has not changed. Exception will be thrown
            if (!Directory.Exists(dest))
            {
                Directory.CreateDirectory(dest);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(dest.GetIndentation() + dest);
            File.Move(filePath, dest + "\\" + baseName.Trim().Replace(" ", "."));

            return dest;
        }

        /// <summary>
        /// Clean the name of the directory first, then its children
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileTypes"></param>
        /// <param name="keywords"></param>
        /// <param name="depth"></param>
        public static void CleanDirectory(string path, List<string> fileTypes, List<string> keywords, int depth = 0)
        {
            var newPath = path.Strip(keywords);
            if (newPath != path)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(GetIndentation(depth) + path);
                Directory.Move(path, newPath);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(GetIndentation(depth) + path);
            }
            path = newPath;

            var dirs = System.IO.Directory.GetDirectories(path).ToList();
            CleanFiles(path, fileTypes, keywords, depth + 1);
            foreach (var dir in dirs)
            {
                CleanDirectory(dir, fileTypes, keywords, depth + 1);
            }
        }

        public static void CleanFiles(string path, List<string> fileTypes, List<string> keywords, int depth = 0)
        {
            var files = Directory.GetFiles(path).Where(x => fileTypes.Any(y => x.EndsWith(y))).ToList();
            foreach (var file in files)
            {
                string extension = string.Empty;
                int extensionIndex = -1;
                foreach (var type in fileTypes)
                {
                    extensionIndex = file.LastIndexOf(type);
                    if (extensionIndex < 0)
                        continue;
                    extension = file.Substring(file.LastIndexOf(type));
                    break;
                }

                //Remove the extension, clean the name, then readd the extension
                string dest = file.Remove(extensionIndex).Strip(keywords);
                dest += extension;
                // Dont allow File.Move if the name has not changed. Exception will be thrown
                if (dest == file)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(GetIndentation(depth) + file);
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(GetIndentation(depth) + file);
                File.Move(file, dest);
            }
        }

        public static string GetIndentation(int depth)
        {
            var temp = "";
            for (int i = 0; i < depth; i++)
                temp += "\t";
            return temp;
        }
    }
}
