using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace Extensions
{
    public static class ExtensionHelpers
    {
        private static Mutex lck = new Mutex();
        private static Mutex lck2 = new Mutex();
        private static Mutex rowLock = new Mutex();
        private static int curr = 0;
        private static int row = 0;
        public static int max = 0;
        private static object locker = new Object();
        public static int GetNextRow()
        {
            rowLock.WaitOne();
            int curr = row;
            row++;
            rowLock.ReleaseMutex();
            return curr;
        }

        public static void GetAndSetPixelColorForRow(Bitmap Image, Bitmap newImage, List<Point> offsets, int i)
        {
            for (int j = 0; j < Image.Height; j++)
            {
                Color composedColor;
                lock (locker)
                {
                    composedColor = GetPixelColor(Image, Image.Width, Image.Height, offsets, i, j);
                }

                lck2.WaitOne();
                newImage.SetPixel(i, j, composedColor);
                curr++;
                lck2.ReleaseMutex();
                Console.WriteLine((curr.ToDouble() / max.ToDouble()) * 100);
            }
        }
        private static void GetAndSetPixelColor(Bitmap Image, Bitmap newImage, List<Point> offsets, int i, int j)
        {

            Color composedColor = GetPixelColor(Image, Image.Width,Image.Height,offsets, i, j);

            lck2.WaitOne();
            newImage.SetPixel(i, j, composedColor);
            curr++;
            lck2.ReleaseMutex();
            Console.WriteLine((curr.ToDouble() / max.ToDouble()) * 100);
        }


        /// <summary>
        /// Get the average pixel color for all points around a coordinate in an image
        /// </summary>
        /// <param name="Image"></param>
        /// <param name="offsets"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private static Color GetPixelColor(Bitmap Image, int width, int height,List<Point> offsets, int i, int j)
        {
            List<Color> pixels = null;
            // Only consider pixels that are within the range
            var validOffsets = offsets.Where(p => p.X + i >= 0 && p.Y + j >= 0 && p.X + i < width && p.Y + j < height).ToList();
            pixels = validOffsets.Select(p => {

                lock(locker)
                {
                    return Image.GetPixel(p.X + i, p.Y + j);
                }
                
                }).ToList();

            var R = pixels.Sum(x => x.R) / pixels.Count;
            var G = pixels.Sum(x => x.G) / pixels.Count;
            var B = pixels.Sum(x => x.B) / pixels.Count;


            return Color.FromArgb(R, G, B);
        }

    }
}
