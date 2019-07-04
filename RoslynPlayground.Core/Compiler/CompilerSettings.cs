using System.IO;

namespace RoslynSandbox.Core.Compiler
{
    public static class CompilerSettings
    {
        public static readonly string OutputDirectory = Path.GetDirectoryName(typeof(CompilerSettings).Assembly.Location);
    }
}
