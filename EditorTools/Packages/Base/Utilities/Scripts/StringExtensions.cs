using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace Game.Base.Utilities
{
    public static class StringExtensions
    {
        /// <summary>
        /// takes string and formats it to title case
        ///  Eg MY EXAMPLE STRING =&gt; MyExampleString
        /// </summary>
        /// <param name="input">to format</param>
        /// <returns></returns>
        public static string ToTitleCase(this string input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(System.Char.ToUpper(input[0])); //first to Upper
            for (int index = 1; index < input.Length; ++index)
            {
                char ch = input[index];
                if (ch == ' ' && index + 1 < input.Length)
                {
                    char upper = input[index + 1];
                    if (char.IsLower(upper))
                        upper = char.ToUpper(upper, CultureInfo.InvariantCulture);
                    stringBuilder.Append(upper);
                    ++index;
                }
                else
                    stringBuilder.Append(ch);
            }
            return stringBuilder.ToString();
        }


        public static string ToSentenceCase(this string input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(System.Char.ToUpper(input[0])); //Ignore first and to Upper
            for (int index = 1; index < input.Length; ++index)
            {
                char ch = input[index];
                if (char.IsUpper(ch) && index + 1 < input.Length)
                {
                    stringBuilder.Append(" ");
                    stringBuilder.Append(ch);
                }
                else
                    stringBuilder.Append(ch);
            }
            return stringBuilder.ToString();
        }
    }
}
