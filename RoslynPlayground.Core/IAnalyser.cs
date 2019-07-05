using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using RoslynSandbox.Core.Compiler;
using RoslynSandbox.Core.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoslynSandbox.Core
{
    public interface IAnalyser : IDisposable
    {
        Task<IEnumerable<Token>> GetTokensAsync();

        Task<IEnumerable<CompletionItem>> GetAutoCompleteAsync();

        Task<IReadOnlyCollection<Diagnostic>> GetDiagnosticsAsync();

        Task<CompilerResult> CompileAsync();
    }
}
