namespace Website {
  const choicesSectionSelector = "section.choices";
  const choicesFormSelector = "section.choices form";

  function handleChoiceResponse(response) {
    let newUrl = Xhr.result.responseURL;
    history.replaceState({}, "", newUrl);

    let timeWaited = Date.now() - Ajax.startTime;
    let timeToWait = Math.max(0, Ajax.longFadeTime - timeWaited);

    let fadeInNewChoices = () => {
      let newChoicesSection = $(response).find(choicesSectionSelector);
      $(choicesSectionSelector).replaceWith(newChoicesSection);
      let newChoices = $(choicesFormSelector);
      newChoices.hide();
      newChoices.fadeIn(Ajax.quickFadeTime);
    };

    window.setTimeout(fadeInNewChoices, timeToWait);
  }

  function handleAjaxError() {
    window.location.replace("/");

    Ajax.setupLiveForm(choicesFormSelector, handleChoiceResponse, handleAjaxError);
  }
}
