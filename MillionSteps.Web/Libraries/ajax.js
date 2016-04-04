/// <reference path="jquery.d.ts" />
var Ajax;
(function (Ajax) {
    Ajax.startTime = null;
    function setupLiveForm(formSelector, success, error, meanwhile) {
        var _this = this;
        $(document).on("submit", formSelector, {}, function (event) {
            event.preventDefault();
            Ajax.startTime = Date.now();
            var form = $(_this);
            var action = form.attr("action");
            var method = form.attr("method");
            var data = form.serialize();
            $.ajax({
                url: action,
                type: method,
                data: data,
                dataType: "html",
                success: success,
                error: error
            });
            meanwhile(form);
        });
    }
    Ajax.setupLiveForm = setupLiveForm;
})(Ajax || (Ajax = {}));
