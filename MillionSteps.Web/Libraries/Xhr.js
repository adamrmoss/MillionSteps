/// <reference path="jquery.d.ts" />
var Xhr;
(function (Xhr) {
    var $xhr = $.ajaxSettings.xhr;
    Xhr.result = null;
    function xhr() {
        Xhr.result = $xhr();
        return Xhr.result;
    }
    Xhr.xhr = xhr;
    function initialize() {
        $.ajaxSettings.xhr = xhr;
    }
    Xhr.initialize = initialize;
})(Xhr || (Xhr = {}));
$(function () { return Xhr.initialize(); });
//# sourceMappingURL=Xhr.js.map