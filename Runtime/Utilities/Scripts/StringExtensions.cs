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
        ///  Eg MY EXAMPLE STRING => MyExampleString
        /// </summary>
        /// <param name="input">to format</param>
        /// <returns></returns>
        public static string ToTitleCase(this string input)
        {
            if (input == null ||input.Length == 0) { return ""; }
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

        /// <summary>
        /// takes string and formats it to SentenceCase
        /// E.G. MyExampleString => My Example String
        /// </summary>
        /// <param name="input">to format</param>
        /// <returns></returns>
        public static string ToSentenceCase(this string input)
        {
            if (input == null ||input.Length == 0) { return ""; }
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
        /// <summary>
        /// takes string and formats it to a shorter Version, acronym if possible or first 3 letters
        /// MyExampleString => MES
        /// </summary>
        /// <param name="input">to format</param>
        /// <returns></returns>
        public static string ToShortVersion(this string input)
        {
            if (input == null ||input.Length == 0) { return ""; }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(System.Char.ToUpper(input[0])); //Always add first char
            bool canAcronym = false;
            for (int index = 1; index < input.Length; ++index)
            {
                char ch = input[index];
                if (char.IsUpper(ch) && index + 1 < input.Length)
                {
                    stringBuilder.Append(ch);
                    canAcronym = true;
                }
            }

            if (!canAcronym)
            {
                //take first 3 letters instead
                stringBuilder.Append(""+input[1] + input[2]);
            }
            return stringBuilder.ToString();
        }
    }
}
