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