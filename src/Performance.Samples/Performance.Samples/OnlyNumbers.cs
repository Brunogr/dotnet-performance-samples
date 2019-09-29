using System;

namespace Performance.Samples
{
    public static class OnlyNumbers
    {
        public static unsafe string Unsafe(string text)
        {
            char* output = stackalloc char[text.Length];
            char* current = output;
            int count = 0;

            foreach (var @char in text)
            {
                if (!char.IsNumber(@char))
                    continue;

                *current++ = @char;
                count++;
            }

            return new string(output, 0, count);
        }


        public static string Safe(string text)
        {
            string output = string.Empty;

            foreach (var @char in text)
            {
                if (!char.IsNumber(@char))
                    continue;

                output += @char;
            }

            return output;
        }

        public static string Regex(string text)
        {
            return System.Text.RegularExpressions.Regex.Replace(text, "[^0-9]", "");
        }
    }
}
