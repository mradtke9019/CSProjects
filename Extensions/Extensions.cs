﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Strip a string of any keywords specified starting from largest to smallest
        /// </summary>
        /// <param name="str"></param>
        /// <param name="keywords">The substrings to remove from the string</param>
        /// <returns></returns>
        public static string Strip(this string str, List<string> keywords)
        {
            var newStr = str;
            var lowered = str.ToLower();
            foreach (var keyword in keywords)
            {
                if (string.IsNullOrEmpty(keyword))
                    continue;
                var index = 0;
                // Remove all locations of the substring
                while (index >= 0)
                {
                    index = lowered.IndexOf(keyword.ToLower());
                    if (index < 0)
                        break;
                    lowered = lowered.Remove(index, keyword.Length);
                    newStr = newStr.Remove(index, keyword.Length);
                }

            }
            if (newStr.EndsWith(".") || newStr.EndsWith("-"))
                newStr = newStr.Substring(0, newStr.Length - 1);
            return newStr.Trim();
        }

        public static string StripPeriod(this string str)
        {
            
            if (str.Contains(".") && str.Split('.').Length > 1 && (!str.Contains(" .") && !str.Contains(". ") && !str.Contains(" . ")))
            {
                str = string.Join(' ', str.Split('.'));
            }
            return str;
        }

        public static string TrimSpace(this string str)
        {
            while(str.Contains("  "))
                str = str.Replace("  ", " ");
            return str;
        }

        public static string Capitalize(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            if (str.Length == 1)
                return str.ToUpper();

            return string.Join(' ', str.Split(' ').Select(x => x.Length > 1 ? x.First().ToString().ToUpper() + x.Substring(1, x.Length - 1).ToLower() : x.First().ToString().ToUpper()));
        }

        /// <summary>
        /// Returns x amount of tabs expected based on its position in a file system
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetIndentation(this string str)
        {
            var temp = "";
            for (int i = 0; i < str.Split('\\').Length - 2; i++)
                temp += "\t";
            return temp;
        }

        public static Double ToDouble(this object a)
        {
            double b = Double.Parse(a.ToString());
            return b;
        }

        public static Double ToDouble(this int a)
        {
            double b = Double.Parse(a.ToString());
            return b;
        }

        public static int Min(this int[] nums)
        {
            var min = int.MaxValue;
            foreach (var num in nums)
            {
                if (num < min)
                    min = num;
            }
            return min;
        }

        public static int Min(int e1, int e2, int e3) =>
        Math.Min(Math.Min(e1, e2), e3);
    }
}
