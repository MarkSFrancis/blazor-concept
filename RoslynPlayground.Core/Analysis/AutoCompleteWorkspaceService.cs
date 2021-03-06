﻿using Microsoft.CodeAnalysis.Completion;
using RoslynSandbox.Core.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoslynSandbox.Core.Analysis
{
    public class AutoCompleteWorkspaceService : IDisposable
    {
        public AutoCompleteWorkspaceService(PlaygroundWorkspace workspace)
        {
            Workspace = workspace ?? throw new ArgumentNullException(nameof(workspace));

            Workspace.EditingDocumentChanged += WorkspaceEditingDocumentChanged;

            WorkspaceEditingDocumentChanged(
                null,
                new EditingDocumentChangedEventArgs(Workspace.EditingFile, Workspace.EditingDocument)
            );
        }

        private void WorkspaceEditingDocumentChanged(object sender, EditingDocumentChangedEventArgs e)
        {
            if (Workspace.EditingDocument != null)
            {
                _completionService = CompletionService.GetService(Workspace.EditingDocument);
            }
        }

        private CompletionService _completionService;

        public PlaygroundWorkspace Workspace { get; }

        public async Task<IEnumerable<CompletionItem>> GetAutoCompleteAsync()
        {
            if (Workspace.EditingDocument is null)
            {
                return new List<CompletionItem>();
            }

            CompletionList completionList = await _completionService.GetCompletionsAsync(Workspace.EditingDocument, Workspace.EditingFile.EditorPosition.Value);

            return FilterByActiveSpan(completionList, Workspace.EditingFile.RawContents, Workspace.EditingFile.EditorPosition);
        }

        private IEnumerable<CompletionItem> FilterByActiveSpan(CompletionList suggested, string originalSource, int? editorPosition)
        {
            if (suggested is null)
            {
                return new List<CompletionItem>();
            }

            if (originalSource is null || suggested.Span.IsEmpty || !editorPosition.HasValue)
            {
                return suggested.Items;
            }

            var editingSpanLength = editorPosition.Value - suggested.Span.Start;

            if (editingSpanLength <= 0)
            {
                return suggested.Items;
            }

            var editingPrefix = originalSource.Substring(suggested.Span.Start, editingSpanLength);

            return suggested.Items.Where(c => c.DisplayText.StartsWith(editingPrefix));
        }

        public void Dispose()
        {
            if (Workspace != null)
            {
                Workspace.EditingDocumentChanged -= WorkspaceEditingDocumentChanged;
            }
        }
    }
}
