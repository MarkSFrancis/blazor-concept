using System;

namespace RoslynSandbox.Core.Code
{
    public static class PositionConverter
    {
        public static (int row, int column) PositionToRowColumn(int position, string text)
        {
            if (position >= text.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(position));
            }

            var rows = text.Split(new[] { UnixStringExtensions.UnixNewLine }, StringSplitOptions.None);

            int passedCharsCount = 0;
            int rowIndex = 0;
            while (rowIndex < rows.Length)
            {
                var indexInRow = position - passedCharsCount;
                var rowSize = rows[rowIndex].Length;

                if (indexInRow < rowSize)
                {
                    return (rowIndex + 1, indexInRow + 1);
                }

                passedCharsCount += rowSize + UnixStringExtensions.UnixNewLine.Length;
                rowIndex++;
            }

            throw new ArgumentOutOfRangeException(nameof(position));
        }
    }
}
