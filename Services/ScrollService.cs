using System;
using System.Drawing;
using Microsoft.JSInterop;

namespace BlazorSampleApp.Services
{
    public class ScrollService
    {
        public Point ScrollPosition
        {
            get
            {
                return _scrollPosition;
            }
            set
            {
                _scrollPosition = value;
                Scrolled?.Invoke(_scrollPosition);
            }
        }

        private Point _scrollPosition = new Point(0, 0);

        public delegate void ScrollEvent(Point newPosition);

        public event ScrollEvent Scrolled;

        [JSInvokable]
        public void SetScrollPosition(int[] coords)
        {
            ScrollPosition = new Point(coords[0], coords[1]);
        }
    }
}