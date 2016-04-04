/// <reference path="jquery.d.ts" />
var Xhr;
(function (Xhr) {
    var $xhr = $.ajaxSettings.xhr;
    Xhr.result = null;
    function xhr() {
        Xhr.result = $xhr();
    }
    Xhr.xhr = xhr;
    function initialize() {
        $.ajaxSettings.xhr = xhr;
    }
    Xhr.initialize = initialize;
})(Xhr || (Xhr = {}));
$(Xhr.initialize);
