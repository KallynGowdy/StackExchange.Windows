window.onload = function() {
    PR.prettyPrint();
};
(function () {
    var preTags = document.getElementsByTagName('pre');
    for (var i = 0; i < preTags.length; i++) {
        var t = preTags[i];
        t.className = 'prettyprint';
    }
})();