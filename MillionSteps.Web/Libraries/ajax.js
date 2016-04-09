/// <reference path="jquery.d.ts" />
var Ajax;
(function (Ajax) {
    Ajax.startTime = null;
    function setupLiveForm(formSelector, success, error, meanwhile) {
        $(document).on("submit", formSelector, {}, function (event) {
            event.preventDefault();
            Ajax.startTime = Date.now();
            var form = $(event.target).closest("form");
            var action = form.attr("action");
            var method = form.attr("method");
            var data = form.serialize();
            $.ajax({
                url: action,
                type: method,
                data: data,
                dataType: "html",
                success: function (response) {
                    Ajax.respondToAjax(success, response);
                },
                error: error
            });
            meanwhile(form);
        });
    }
    Ajax.setupLiveForm = setupLiveForm;
    function respondToAjax(success, response) {
        var newUrl = Xhr.result.responseURL;
        history.replaceState({}, "", newUrl);
        success(response);
    }
    Ajax.respondToAjax = respondToAjax;
})(Ajax || (Ajax = {}));
//# sourceMappingURL=Ajax.js.map