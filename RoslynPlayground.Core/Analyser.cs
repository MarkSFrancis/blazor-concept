using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using RoslynSandbox.Core.Analysis;
using RoslynSandbox.Core.Compiler;
using RoslynSandbox.Core.Diagnostics;
using RoslynSandbox.Core.Workspace;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoslynSandbox.Core
{
    public class Analyser : IAnalyser
    {
        public Analyser(Sandbox workspace)
        {
            Workspace = workspace;

            _autoComplete = new AutoCompleteService(workspace);
        }

        public Sandbox Workspace { get; }

        private readonly AutoCompleteService _autoComplete;

        public Task<CompilerResult> CompileAsync()
        {
            return Workspace.CompileAsync();
        }

        public Task<IEnumerable<CompletionItem>> GetAutoCompleteAsync()
        {
            return _autoComplete.GetAutoCompleteAsync();
        }

        public Task<IReadOnlyCollection<Diagnostic>> GetDiagnosticsAsync()
        {
            return Workspace.GetDiagnosticsAsync();
        }

        public void Dispose()
        {
            _autoComplete?.Dispose();
        }
    }
}
