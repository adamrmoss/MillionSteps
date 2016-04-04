var Website;
(function (Website) {
    var choicesSectionSelector = "section.choices";
    var choicesFormSelector = "section.choices form";
    function handleChoiceResponse(response) {
        var newUrl = Xhr.result.responseURL;
        history.replaceState({}, "", newUrl);
        var timeWaited = Date.now() - Ajax.startTime;
        var timeToWait = Math.max(0, Ajax.longFadeTime - timeWaited);
        var fadeInNewChoices = function () {
            var newChoicesSection = $(response).find(choicesSectionSelector);
            $(choicesSectionSelector).replaceWith(newChoicesSection);
            var newChoices = $(choicesFormSelector);
            newChoices.hide();
            newChoices.fadeIn(Ajax.quickFadeTime);
        };
        window.setTimeout(fadeInNewChoices, timeToWait);
    }
    function handleAjaxError() {
        window.location.replace("/");
        Ajax.setupLiveForm(choicesFormSelector, handleChoiceResponse, handleAjaxError);
    }
})(Website || (Website = {}));
//# sourceMappingURL=Website.js.map