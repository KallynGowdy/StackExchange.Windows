window.onload = function() {
    PR.prettyPrint();
};
window.getHeight = function() {
    return document.body.scrollHeight.toString();
};
(function () {
    var preTags = document.getElementsByTagName('pre');
    for (var i = 0; i < preTags.length; i++) {
        var t = preTags[i];
        t.className = 'prettyprint';
    }
})();