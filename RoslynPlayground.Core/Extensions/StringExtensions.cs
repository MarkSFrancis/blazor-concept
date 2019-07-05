using System;

namespace RoslynSandbox.Core
{
    public static class StringExtensions
    {
        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str.Split(new[] { separator }, options);
        }

        public static string[] Split(this string str, string separator)
        {
            return str.Split(separator, StringSplitOptions.None);
        }
    }
}
