using System;
using System.Collections.Generic;
using System.Text;

namespace FileCleaning
{
    public static class Extensions
    {
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
    }
}
