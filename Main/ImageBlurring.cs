using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Extensions;

namespace Main
{
    class ImageBlurring
    {
        private volatile Bitmap Image;
        public void Run()
        {
            string name = "reeee.png";
            string filePath = @"D:\Pictures\" + name;
            Image = new System.Drawing.Bitmap(filePath);
            int weight = 100;
            Image.Blur(weight,10).Save(@"D:\Pictures\blurred" + weight + name);
        }

        public void PrintPoints()
        {
            int square = 15;
            var points = Extensions.Utility.GetPoints(10);

            for(int i = -square; i < square; i++)
            {
                for(int j = -square; j < square; j++)
                {
                    char character = ' ';
                    if (points.Contains(new System.Drawing.Point(i, j)))
                        character = '+';
                    Console.Write(character);
                }
                Console.WriteLine();
            }
            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
