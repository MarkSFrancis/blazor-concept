using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using RoslynSandbox.Core.Compiler;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoslynSandbox.Core
{
    public interface IAnalyser : IDisposable
    {
        Task<IEnumerable<CompletionItem>> GetAutoCompleteAsync();

        Task<IReadOnlyCollection<Diagnostic>> GetDiagnosticsAsync();

        Task<CompilerResult> CompileAsync();
    }
}
