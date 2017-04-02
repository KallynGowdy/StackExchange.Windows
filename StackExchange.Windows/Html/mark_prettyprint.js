(function (window) {
    var recordedHeight = null;
    window.onload = function () {
        PR.prettyPrint();
        recordedHeight = findHeight();
    };
    window.getHeight = function () {
        // Recorded height stays null until the content has been completely loaded.
        // (i.e. including images)
        return recordedHeight ? recordedHeight.toString() : "-1";
    };
    window.findHeight = function () {
        var height = 0;
        var children = document.body.children;
        // The last child is the last element minus the 2 scripts
        // that are at the end of the body.
        var lastChild = children[children.length - 3];

        // The height of our webview can be calculated as the bottom
        // part of the bounding rectangle on our last element. 
        // Round up so that we will always end up slightly more space than we may need.
        // The height is passed back as an integer, so we can't exactly handle partial units
        // in any other way.
        height += Math.ceil(lastChild.getBoundingClientRect().bottom);

        return height.toString();
    };
    var preTags = document.getElementsByTagName('pre');
    for (var i = 0; i < preTags.length; i++) {
        var t = preTags[i];
        t.className = 'prettyprint';
    }
})(window);