using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileCleaning;

namespace FileSync
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {

                var flagArgs = args.Where(x => x.Contains("-")).ToList();
                var flaglessArgs = args.Where(x => !x.Contains("-")).ToList();
                bool recurse = flagArgs.Contains("-r");

                string source = null;
                string destination = null;
#if DEBUG
                source = @"D:\Videos\TV\";
                destination = @"M:\Videos\TV\";
                recurse = true;

#else
                source = flaglessArgs[0];
                destination = flaglessArgs[1];

#endif


                Console.WriteLine("Cleaning " + source);
                FileCleaning.Program.Main(new string[] { source, recurse ? "-r" : string.Empty });
                if (Directory.Exists(destination))
                {
                    Console.WriteLine("Cleaning " + destination);
                    FileCleaning.Program.Main(new string[] { destination, recurse ? "-r" : string.Empty });
                }
                // Clean source. Clean destination.
                // Process: Get files from source. Get files from destination. Add files that are missing
                var extensions = new List<string>() { ".mkv", ".mp4", ".avi", ".wmv", ".srt", ".m4v" };

                var sourceFiles = GetFiles(source, recurse).Select(x => new FileInfo(x)).Where(x => extensions.Contains(x.Extension)).ToList();
                var destinationFiles = GetFiles(destination, recurse).Select(x => new FileInfo(x)).Where(x => extensions.Contains(x.Extension)).ToList();

                var miscellaneousFiles = GetFiles(source, recurse).Select(x => new FileInfo(x)).Where(x => !extensions.Contains(x.Extension.ToLower())).ToList();
                foreach (var file in miscellaneousFiles)
                {
                    Console.WriteLine(file.Name);
                }

                //var missingFiles = sourceFiles.Select(x => x.Name).Except(destinationFiles.Select(y => y.Name)).ToList();
                var missingFiles = sourceFiles.Where(x => !destinationFiles.Select(y => y.Name).Contains(x.Name)).ToList();
                Console.WriteLine("Copying missing files");
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var file in missingFiles)
                {
                    //Copy the missing file with its structure to the destination
                    var destinationStructure = file.FullName.Replace(source, destination);
                    // Attempt to build the folder structure for the file first since it likely does not exist yet.
                    Directory.CreateDirectory(destinationStructure.Replace(file.Name, ""));

                    Console.WriteLine(file.Name);
                    File.Copy(file.FullName, destinationStructure);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Done Syncing " + source + " | " + destination);
            }
            catch (Exception e)
            {
                Console.WriteLine("Usage: FileSync <source> <destination> <options>");
            }
        }

        public static List<string> GetFiles(string path, bool recurse = false)
        {
            if (!Directory.Exists(path))
                return new List<string>();
            List<string> files = new List<string>();
            files.AddRange(Directory.GetFiles(path));
            if(recurse)
            {
                foreach (var dir in Directory.GetDirectories(path))
                {
                    files.AddRange(GetFiles(dir, recurse));
                }

            }
            return files;
        }

    }
}
