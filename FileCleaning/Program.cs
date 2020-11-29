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

        static void Main(string[] args)
        {
            keywords = File.ReadAllText("./keywords.txt").Replace("\r", "").Split('\n').OrderByDescending(x => x.Length).ToList();
            Utility.DirectoryRun("D:\\Videos", CleanDirectory, CleanFile, true);
            //CleanDirectory("D:\\Videos", videoExtensions, keywords);
            //CleanDirectory("H:\\Videos", videoExtensions, keywords);
        }

        public static string CleanDirectory(string path)
        {
            var newPath = path.Strip(keywords);
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
            string dest = path.Remove(extensionIndex).Strip(keywords);
            dest += extension;
            // Dont allow File.Move if the name has not changed. Exception will be thrown
            if (dest == path)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(dest.GetIndentation() + dest);
                return dest;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(dest.GetIndentation() + dest);
            File.Move(path, dest);

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
