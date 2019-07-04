namespace RoslynSandbox.Core
{
    public static class StringExtensions
    {
        public const string WindowsNewLine = "\r\n";
        public const string UnixNewLine = "\n";

        public static string ToUnixString(this string str)
        {
            return str.Replace(WindowsNewLine, UnixNewLine);
        }
    }
}
