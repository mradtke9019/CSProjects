using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class PhotoSorting
    {

        public void Run()
        {
            string path = @"C:\Users\Matthew Radtke\Pictures\iPhone";
            List<string> files = Directory.GetFiles(path).ToList();
            string targetPrefix = @"C:\Users\Matthew Radtke\Pictures\iPhone";
            Dictionary<string,string> targets = new Dictionary<string,string>();
            foreach(var file in files)
            {
                FileInfo info = new FileInfo(file);
                string destination = targetPrefix + "\\" + info.CreationTime.Year + "\\" + info.CreationTime.Month;// + "\\" + info.Name;
                if(!Directory.Exists(destination))
                {
                    Directory.CreateDirectory(destination);
                }
                File.Move(file, destination + "\\" + info.Name);
            }
        }

    }
}
