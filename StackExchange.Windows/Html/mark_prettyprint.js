window.onload = function() {
    PR.prettyPrint();
};
window.getHeight = function() {
    var height = document.body.scrollHeight;
    window.external.notify(height.toString());
};
(function () {
    var preTags = document.getElementsByTagName('pre');
    for (var i = 0; i < preTags.length; i++) {
        var t = preTags[i];
        t.className = 'prettyprint';
    }
})();