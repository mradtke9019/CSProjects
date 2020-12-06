using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    public static class Utility
    {
        /// <summary>
        /// Runs a given function that expects a path the a directory and returns the final path (if it changed). Any File Function supplied will 
        /// be applied to all files within the directory, but is not necessary.
        /// </summary>
        /// <param name="path">The path to the directory to apply the function on</param>
        /// <param name="DirectoryFunction">A function to apply to a directory</param>
        /// <param name="FileFunction">A function to also apply to files in the directory</param>
        /// <param name="recurse">The flag to decide to apply the function recursively</param>
        public static void DirectoryRun(string path, Func<string, string> DirectoryFunction, Func<string, string> FileFunction = null, bool recurse = false)
        {
            if (!Directory.Exists(path))
                throw new InvalidOperationException();
            var oldPath = path;
            if(DirectoryFunction != null)
                path = DirectoryFunction(path);
            if(!Directory.Exists(path))
            {
                Console.WriteLine(path + " does not exist");
                path = oldPath;
            }
            var files = Directory.GetFiles(path);

            if (FileFunction != null)
            {
                foreach (var file in files)
                {
                    if (!File.Exists(file))
                        throw new InvalidOperationException();
                    FileFunction(file);
                }
            }

            if (recurse)
            {
                var dirs = Directory.GetDirectories(path);
                foreach (var dir in dirs)
                {
                    DirectoryRun(dir, DirectoryFunction, FileFunction, recurse);
                }
            }
        }

        public static void FileRun(string path, Action<string> FileFunction)
        {
            if (!File.Exists(path))
                throw new InvalidOperationException();
            FileFunction(path);
        }

        /// <summary>
        /// Accepts a path to apply a function that returns void onto a directory. Optional args to also apply a function onto the files 
        /// or apply this function recursively.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="DirectoryFunction"></param>
        /// <param name="FileFunction"></param>
        /// <param name="recurse"></param>
        public static void DirectoryRun(string path, Action<string> DirectoryFunction, Action<string> FileFunction = null, bool recurse = false)
        {
            if (!Directory.Exists(path))
                return;
            DirectoryFunction(path);
            var files = Directory.GetFiles(path);

            if (FileFunction != null)
            {
                foreach (var file in files)
                {
                    if (!File.Exists(file))
                        throw new InvalidOperationException();
                    FileFunction(file);
                }
            }

            if (recurse)
            {
                var dirs = Directory.GetDirectories(path);
                foreach (var dir in dirs)
                {
                    DirectoryRun(dir, DirectoryFunction, FileFunction, recurse);
                }
            }
        }
    }
}
