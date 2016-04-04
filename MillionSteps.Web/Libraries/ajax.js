/// <reference path="jquery.d.ts" />
var Ajax;
(function (Ajax) {
    Ajax.flashFadeTime = 100;
    Ajax.quickFadeTime = 200;
    Ajax.longFadeTime = 1200;
    Ajax.startTime = null;
    function setupLiveForm(formSelector, success, error) {
        var _this = this;
        var _formSelector = formSelector;
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
            var otherChoices = $(_formSelector).not(form);
            if (otherChoices.length > 0) {
                otherChoices.fadeTo(Ajax.flashFadeTime, 0.01);
            }
            form.fadeTo(Ajax.longFadeTime, 0.01);
        });
    }
    Ajax.setupLiveForm = setupLiveForm;
})(Ajax || (Ajax = {}));
//# sourceMappingURL=Ajax.js.map