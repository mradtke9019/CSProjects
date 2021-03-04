﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Matrices;
using Vectors;

namespace Main
{
    class Program
    {
        struct Record {
            public string A;
            public string B;
        }
        public class Solution
        {
            public static List<char> possibleValues = new List<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            public static List<int> possibleValuesInt = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            public void SolveSudoku(char[][] board)
            {
                var done = false;
                var stuck = false;
                while (!isGameDone(board))
                {
                    stuck = true;
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (board[i][j] != '.')
                                continue;
                            var possible = GetValuesForPosition(board, i, j);
                            if (possible.Length == 1)
                            {
                                stuck = false;
                                board[i][j] = char.Parse(possible[0].ToString());
                            }
                        }
                    }
                    if (stuck)
                        Console.WriteLine("Stuck");
                }
                foreach(var row in board)
                {
                    foreach (var col in row)
                        Console.Write(col + " ");
                    Console.WriteLine();
                }
                Console.WriteLine(board);
            }

            public bool isGameDone(char[][] board)
            {
                for(int i = 0; i < board.Length; i++)
                {
                    for (int j = 0; j < board[i].Length; j++)
                        if (board[i][j] == '.')
                            return false;
                }
                return true;
            }

            public int[] GetValuesForPosition(char[][] board, int i, int j)
            {
                var rowValues = GetValuesWithinRow(board, i);
                var columnValues = GetValuesWithinColumn(board, j);
                var boxValues = GetValuesWithinSubbox(board, i, j);
                return rowValues.Intersect(columnValues).Intersect(boxValues).ToArray();
            }

            public int[] GetValuesWithinRow(char[][] board, int i)
            {
                var rowNums = new List<int>();
                // Check values 

                for (int columnPosition = 0; columnPosition < board.Length; columnPosition++)
                {
                    if (board[i][columnPosition] == '.')
                        continue;
                    // Get values within a row
                    rowNums.Add(int.Parse(board[i][columnPosition].ToString()));
                }

                return possibleValuesInt.Except(rowNums).ToArray();
            }
            public int[] GetValuesWithinColumn(char[][] board, int j)
            {
                var columnNums = new List<int>();

                for (int row = 0; row < board.Length; row++)
                {

                    if (board[row][j] == '.')
                        continue;
                    // Get values within a row
                    columnNums.Add(int.Parse(board[row][j].ToString()));
                }

                return possibleValuesInt.Except(columnNums).ToArray();
            }
            public int[] GetValuesWithinSubbox(char[][] board,int i, int j)
            {
                int boxRow = i / 3;
                int boxCol = j / 3;

                List<int> numsInBox = new List<int>();

                for (int r = 0; r < 3; r++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        if (board[r + boxRow * 3][c + boxCol * 3] == '.')
                            continue;
                        numsInBox.Add(int.Parse(board[r + boxRow * 3][c + boxCol * 3].ToString()));
                    }
                }

                return possibleValuesInt.Except(numsInBox).ToArray();
            }
        }


        static void Main(string[] args)
        {
            var puzzle = File.ReadAllText("./puzzle.txt").Split("\n");

            char[][] board = new char[9][];
            int i = 0;
            foreach(var row in puzzle)
            {
                board[i] = row.Replace("\r", "").ToCharArray();
                i++;
            }
            Solution s = new Solution();

            s.SolveSudoku(board);



            return;

            var images = Directory.GetFiles(@"D:\Pictures\2016-04-25 001");//\2016-04-25 001
            List<string> badFormats = new List<string>() { "mov", "mp4", "aae"};
            List<Record> dups = new List<Record>();
            List<string> alreadyDone = new List<string>();
            int comparisons = 0;
            foreach(var image in images)
            {
                alreadyDone.Add(image);
                foreach(var img in images)
                {
                    try
                    {
                        if (image == img || alreadyDone.Contains(img))
                            continue;
                        comparisons++;
                        List<bool> iHash1 = GetHash(new Bitmap(image));
                        List<bool> iHash2 = GetHash(new Bitmap(img));

                        int equalElements = iHash1.Zip(iHash2, (i, j) => i == j).Count(eq => eq);
                        if (equalElements >= 255 && !dups.Any(x => (x.A == image && x.B == img) || (x.A == img && x.B == image)))
                            dups.Add(new Record { A = image, B = img });
                    }
                    catch(Exception e)
                    {
                        continue;
                    }
                }
            }
            Console.WriteLine(comparisons + " Comparisons");
            Console.WriteLine("Dups");
            foreach(var dup in dups)
            {
                Console.WriteLine(dup.A + " - " + dup.B);
            }
            //Image img1 = Image.FromFile("D:\\Pictures\\2016-04-25 001\\IMG_0025.PNG");
            //Image img2 = Image.FromFile("D:\\Pictures\\2016-04-25 001\\IMG_0025.PNG");

            //List<bool> iHash1 = GetHash(new Bitmap("D:\\Pictures\\2016-04-25 001\\IMG_0025.PNG"));
            //List<bool> iHash2 = GetHash(new Bitmap("D:\\Pictures\\2016-04-25 001\\IMG_0025.PNG"));

            //determine the number of equal pixel (x of 256)
            //int equalElements = iHash1.Zip(iHash2, (i, j) => i == j).Count(eq => eq);
            //bool equal = equalElements > 256 / 2;

            //bitmap.Save("C:\\Users\\mradt\\Desktop\\Test.jpg", ImageFormat.Jpeg);
            //MyGraphics myG = new MyGraphics();
            //myG.Work();
            //VectorTests();
            //Tests();
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static List<bool> GetHash(Bitmap bmpSource)
        {
            List<bool> lResult = new List<bool>();
            //create new image with 16x16 pixel
            Bitmap bmpMin = new Bitmap(bmpSource, new Size(16, 16));
            for (int j = 0; j < bmpMin.Height; j++)
            {
                for (int i = 0; i < bmpMin.Width; i++)
                {
                    //reduce colors to true / false                
                    lResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
                }
            }
            return lResult;
        }

        public static void VectorTests()
        {
            Vector2D v1 = new Vector2D(1,1);
            Console.WriteLine("Scale test: " + (2 * v1 == new Vector2D(2, 2) ? "passed" : "failed"));
            Console.WriteLine("Addition test: " + (v1 + v1 == new Vector2D(2, 2) ? "passed" : "failed"));
            Console.WriteLine("Identity mult: " + (Matrices.Matrix.IdentityMatrix(2) * v1 == v1 ? "passed" : "failed"));
        }

        public static void Tests()
        {
            float[,] A = { { 0, 1 }, { 1, 2 } };
            float[,] C = { { 0, 1, 2, 2 }, { 1, 2, 2, 2 } };
            float[,] Scale = { { 0, 2 }, {2, 4 } };

            Matrices.Matrix a = new Matrices.Matrix(A);
            Matrices.Matrix b = new Matrices.Matrix(A);
            Matrices.Matrix c = new Matrices.Matrix(C);

            Matrices.Matrix s = new Matrices.Matrix(Scale);
            Console.WriteLine("Equality Test: " +(a == b ? "passed" : "fail"));
            Console.WriteLine("Inequality Test: " + (a != b ? "passed" : "fail"));
            Console.WriteLine("Identity Test: " + (a * Matrices.Matrix.IdentityMatrix(2) == a ? "passed" : "fail"));
            Console.WriteLine("Scale Test: " + ((2 * a) == s  ? "passed" : "fail"));
            Console.WriteLine(a * c);
        }
    }
}
