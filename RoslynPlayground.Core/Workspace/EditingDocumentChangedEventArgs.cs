using Microsoft.CodeAnalysis;

namespace RoslynSandbox.Core.Workspace
{
    public class EditingDocumentChangedEventArgs
    {
        public EditingDocumentChangedEventArgs(SourceFile file, Document document)
        {
            EditingFile = file;
            EditingDocument = document;
        }

        public bool IsEditing => EditingFile is null;

        public SourceFile EditingFile { get; set; }

        public Document EditingDocument { get; set; }
    }
}
