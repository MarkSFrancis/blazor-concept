using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using RoslynSandbox.Core.Analysis;
using RoslynSandbox.Core.Compiler;
using RoslynSandbox.Core.Diagnostics;
using RoslynSandbox.Core.Tokens;
using RoslynSandbox.Core.Workspace;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoslynSandbox.Core
{
    public class Analyser : IAnalyser
    {
        public Analyser(PlaygroundWorkspace workspace)
        {
            Workspace = workspace;

            _autoComplete = new AutoCompleteWorkspaceService(workspace);
        }

        public PlaygroundWorkspace Workspace { get; }

        private readonly AutoCompleteWorkspaceService _autoComplete;

        public Task<CompilerResult> CompileAsync()
        {
            return Workspace.CompileAsync();
        }

        public Task<IEnumerable<Token>> GetTokensAsync()
        {
            return Workspace.GetTokensAsync();
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
