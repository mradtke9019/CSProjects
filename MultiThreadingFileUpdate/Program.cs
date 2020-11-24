using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadingFileUpdate
{
    class Program
    {
        public static Mutex lck;
        public static int ticker;
        public static string file;
        public static int count;
        public static void WriteToFile()
        {
            lck.WaitOne();
            int curr = ticker;
            Console.WriteLine("Ticker# " + curr);
            File.WriteAllText(file, "What the fuck did you say " + curr);
            ticker++;
            lck.ReleaseMutex();
        }

        public static void UpdateCountCorrect()
        {
            lck.WaitOne();
            for(int i = 0; i < 1000000; i++)
            {
                count++;
            }
            lck.ReleaseMutex();
        }
        public static void UpdateCountWrong()
        {
            for (int i = 0; i < 1000000; i++)
            {
                count++;
            }
        }

        public static void FileWork()
        {
            ticker = 0;
            file = "MyFile.txt";
            List<Thread> jobs = new List<Thread>();
            for (int i = 0; i < 1000; i++)
            {
                jobs.Add(new Thread(new ThreadStart(WriteToFile)));
            }
            //Have the main thread begin the execution of each task
            foreach (var job in jobs)
            {
                job.Start();
            }
            //Have main thread wait till all jobs are done to finish execution
            foreach (var job in jobs)
            {
                job.Join();
            }

            Console.WriteLine("Finished Writing to file");
        }
        public static void CounterWrong()
        {
            List<Thread> jobs = new List<Thread>();
            for (int i = 0; i < 2; i++)
            {
                jobs.Add(new Thread(new ThreadStart(UpdateCountWrong)));
            }
            //Have the main thread begin the execution of each task
            foreach (var job in jobs)
            {
                job.Start();
            }
            //Have main thread wait till all jobs are done to finish execution
            foreach (var job in jobs)
            {
                job.Join();
            }

            Console.WriteLine("Finished Updating Count");
            Console.WriteLine("Expecting 2000000");
            Console.WriteLine("Actual: " + count);
            count = 0;
        }

        public static void CounterRight()
        {
            List<Thread> jobs = new List<Thread>();
            for (int i = 0; i < 2; i++)
            {
                jobs.Add(new Thread(new ThreadStart(UpdateCountCorrect)));
            }
            //Have the main thread begin the execution of each task
            foreach (var job in jobs)
            {
                job.Start();
            }
            //Have main thread wait till all jobs are done to finish execution
            foreach (var job in jobs)
            {
                job.Join();
            }

            Console.WriteLine("Finished Updating Count");
            Console.WriteLine("Expecting 2000000");
            Console.WriteLine("Actual: " + count);
            count = 0;
        }

        static void Main(string[] args)
        {
            lck = new Mutex();
            FileWork();
            CounterRight();
            CounterWrong();

            Console.ReadLine();
        }
    }
}
