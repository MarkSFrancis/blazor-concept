using Microsoft.CodeAnalysis;
using RoslynSandbox.Core.Workspace;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace RoslynSandbox.Core.Diagnostics
{
    public static class DiagnosticsExtensions
    {
        public static async Task<IReadOnlyCollection<Diagnostic>> GetDiagnosticsAsync(this PlaygroundWorkspace workspace)
        {
            if (workspace is null)
            {
                throw new ArgumentNullException(nameof(workspace));
            }

            SemanticModel semantics = await workspace.EditingDocument.GetSemanticModelAsync();

            ImmutableArray<Diagnostic> diagnostics = semantics.GetDiagnostics();

            return diagnostics;
        }
    }
}
