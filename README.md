# Blazor Scroll Sample

Demos how to use JsInterop to capture scroll events and pass them between JS and C#

This application was largely written as a proof of concept to test outside of the guides from [AspNetCore Docs](https://docs.microsoft.com/en-gb/aspnet/core/blazor) how easy/ difficult Blazor is to use

---

## Code
#### [Javascript window scroll listener:](/wwwroot/js/scrollManager.js)
```js
scrollManager = function () {
    var dotnetRef;

    document.onscroll = function () {
        var x = document.documentElement.scrollLeft;
        var y = document.documentElement.scrollTop;

        notifyNetOfScroll(x, y);
    }

    function setup(myDotnetRef) {
        dotnetRef = myDotnetRef;

        var x = document.documentElement.scrollLeft;
        var y = document.documentElement.scrollTop;

        notifyNetOfScroll(x, y);
    }

    function notifyNetOfScroll(x, y) {
        if (!dotnetRef) {
            return;
        }

        dotnetRef.invokeMethodAsync('SetScrollPosition', x, y);
    }

    return {
        setup,
        notifyNetOfScroll
    }
}();
```

#### [CSharp scroll service](/Services/ScrollService.cs)
```csharp
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
        public void SetScrollPosition(int x, int y)
        {
            ScrollPosition = new Point(x, y);
        }
    }
}
```
