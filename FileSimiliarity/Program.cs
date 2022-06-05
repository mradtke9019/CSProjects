using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Extensions;

namespace FileSimiliarity
{
    class Program
    {
        public static Mutex lck;
        static void Main(string[] args)
        {
            lck = new Mutex();
            Driver("D:\\Videos\\Movies", 0.70, true, new List<string>() { ".mkv", ".mp4", ".avi", ".wmv", ".m4v" }, "sample");
        }
        public static void Driver(string path, double threshold, bool useFiles = false, List<string> extensions = null, string exception = null)
        {
            var dirs = System.IO.Directory.GetDirectories(path).ToList();
            if(useFiles)
            {
                var files = new List<string>();
                foreach (var dir in dirs)
                    foreach (var file in System.IO.Directory.GetFiles(dir).ToList())
                        if (!file.ToLower().Contains(exception.ToLower()) && extensions.Contains(file.Substring(file.LastIndexOf("."))))
                            files.Add(file);
                dirs = files;
            }

            List<DupPair> similars = new List<DupPair>();
            List<Task> jobs = new List<Task>();

            foreach (var dir1 in dirs)
            {
                foreach (var dir2 in dirs)
                {
                    if (dir1 == dir2)
                        continue;
                    jobs.Add(new Task(() =>
                    {
                        var d1 = dir1.Substring(dir1.LastIndexOf("\\") + 1);
                        var d2 = dir2.Substring(dir2.LastIndexOf("\\") + 1);
                        var similar = Math.Max(Utility.FuzzyCompare(string.Join(" " , d1.Split(' ').OrderBy(x => x)), string.Join(" ", d2.Split(' ').OrderBy(x => x))), Utility.FuzzyCompare(d1, d2));
                        if (similar > threshold)
                        {
                            DupPair d = new DupPair()
                            {
                                A = dir1,
                                B = dir2,
                                similarity = similar * 100
                            };
                            lck.WaitOne();
                            if (!similars.Where(x => x.CompareTo(d) == 0).Any())
                                similars.Add(d);
                            lck.ReleaseMutex();
                        }
                    }));
                }
            }
            foreach (var t in jobs)
                t.Start();

            Task.WaitAll(jobs.ToArray());
            int i = 0;
            foreach (var sim in similars.OrderByDescending(x => x.similarity))
            {
                Console.WriteLine(sim.ToString());
                i++;
                if (i % 2 == 0)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.WriteLine("Done");
        }



    }
}
