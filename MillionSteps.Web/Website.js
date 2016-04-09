var Website;
(function (Website) {
    Website.flashFadeTime = 100;
    Website.quickFadeTime = 200;
    Website.longFadeTime = 1200;
    var choicesSectionSelector = "section.choices";
    var choicesFormSelector = "section.choices form";
    function initialize() {
        Ajax.setupLiveForm(choicesFormSelector, handleChoiceResponse, handleAjaxError, fadeOutOldChoices);
    }
    Website.initialize = initialize;
    function handleChoiceResponse(response) {
        var timeWaited = Date.now() - Ajax.startTime;
        var timeToWait = Math.max(0, Website.longFadeTime - timeWaited);
        window.setTimeout(function () { return fadeInNewChoices(response); }, timeToWait);
    }
    function fadeInNewChoices(response) {
        var newChoicesSection = $(response).find(choicesSectionSelector);
        $(choicesSectionSelector).replaceWith(newChoicesSection);
        var newChoices = $(choicesFormSelector);
        newChoices.hide();
        newChoices.fadeIn(Website.quickFadeTime);
    }
    function handleAjaxError(error) {
        window.location.replace("/");
    }
    function fadeOutOldChoices(form) {
        var otherChoices = $(choicesFormSelector).not(form);
        if (otherChoices.length > 0) {
            otherChoices.fadeTo(Website.flashFadeTime, 0.01);
        }
        form.fadeTo(Website.longFadeTime, 0.01);
    }
})(Website || (Website = {}));
$(function () { return Website.initialize(); });
//# sourceMappingURL=Website.js.map