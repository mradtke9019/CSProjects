using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyPass
{
    static class Program
    {
        public static Queue<string> queue;
        public static List<string> visited;
        public static List<string> allChars;
        public static int count = 0;

        public static void Enqueue<T>(this Queue<T> q, List<T> list)
        {
            foreach (var item in list)
                q.Enqueue(item);
        }

        static void Main(string[] args)
        {
            visited = new List<string>();
            queue = new Queue<string>();
            allChars = GetAlphanumeric();
            // Ecrypt a file with a password
            BreadthFirstBrute(string.Empty, "abcd");

            //Develop brute force
        }

        public static List<string> GetAvailableChars()
        {
            List<string> printableChars = new List<string>();
            for (int i = char.MinValue; i <= char.MaxValue; i++)
            {
                char c = Convert.ToChar(i);
                if (!char.IsControl(c))
                {
                    printableChars.Add(c.ToString());
                }
            }
            return printableChars;
        }

        public static List<string> GetAlphanumeric()
        {
            return "qwertyuiopasdfghjklzxcvbnm1234567890 ".Select(x => x.ToString()).OrderBy(x => x).ToList();
        }

        public static void BreadthFirstBrute(string c, string target = null)
        {
            queue.Enqueue(c);
            visited.Add(c.ToString());

            while (queue.Count > 0)
            {
                var v = queue.Dequeue();

                foreach(var nextChar in allChars)
                {
                    count++;
                    var w = v + nextChar;
                    if (w != target && !visited.Contains(w))
                    {
                        if (count % 1 == 0)
                            Console.WriteLine(w);
                        queue.Enqueue(w);
                        visited.Add(w);
                    }
                    else if(w == target)
                    {
                        Console.WriteLine("Success");
                        break;
                    }
                }
            }
        }
    }
}
