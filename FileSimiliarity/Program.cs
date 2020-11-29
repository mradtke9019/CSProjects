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
            Driver("D:\\Videos\\Movies", 0.80);
        }
        public static void Driver(string path, double threshold)
        {
            var dirs = System.IO.Directory.GetDirectories(path);

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
                        var d1 = dir1.Substring(path.Length + 1);
                        var d2 = dir2.Substring(path.Length + 1);
                        var similar = fuzzyCompare(d1, d2);
                        if (similar > threshold)
                        {
                            DupPair d = new DupPair()
                            {
                                A = d1,
                                B = d2,
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
            foreach (var sim in similars.OrderByDescending(x => x.similarity))
            {
                Console.WriteLine(sim.ToString());
            }
            Console.WriteLine("Done");
        }

        public static bool fuzzyCompare(string a, string b, double percentSimilar)
        {
            var distance = EditDistance(a, b, a.Length, b.Length);
            var min = Extensions.Extensions.Min(new int[] { a.Length, b.Length });
            var similar = min.ToDouble()/ (min + distance).ToDouble();

            if (similar >= percentSimilar)
                return true;
            return false;
        }

        public static double fuzzyCompare(string a, string b)
        {
            // "Some" => "Something" => editdistance 5 => some is 44% similar to something
            var distance = EditDistance(a, b, a.Length, b.Length);
            var min = (new int[] { a.Length, b.Length }).Min();
            var similar = (min).ToDouble() / (min + distance).ToDouble();

            return similar;
        }

        public static int EditDistance(string a, string b, int m, int n)
        {
            if (a.Length == 0)
            {
                return b.Length;
            }

            if (b.Length == 0)
            {
                return a.Length;
            }

            var d = new int[a.Length + 1, b.Length + 1];
            for (var i = 0; i <= a.Length; i++)
            {
                d[i, 0] = i;
            }

            for (var j = 0; j <= b.Length; j++)
            {
                d[0, j] = j;
            }

            for (var i = 1; i <= a.Length; i++)
            {
                for (var j = 1; j <= b.Length; j++)
                {
                    var cost = (b[j - 1] == a[i - 1]) ? 0 : 1;
                    d[i, j] = Extensions.Extensions.Min(
                         d[i - 1, j] + 1,
                         d[i, j - 1] + 1,
                         d[i - 1, j - 1] + cost
                    );
                }
            }
            return d[a.Length, b.Length];
        }

    }
}
