﻿namespace Website {
  export const flashFadeTime = 100;
  export const quickFadeTime = 200;
  export const longFadeTime = 1200;

  const choicesSectionSelector = "section.choices";
  const choicesFormSelector = "section.choices form";

  export function initialize(): void {
    Ajax.setupLiveForm(choicesFormSelector, handleChoiceResponse, handleAjaxError, fadeOutOldChoices);
  }

  function handleChoiceResponse(response): void {
    let newUrl = Xhr.result.responseURL;
    history.replaceState({}, "", newUrl);

    let timeWaited = Date.now() - Ajax.startTime;
    let timeToWait = Math.max(0, longFadeTime - timeWaited);

    window.setTimeout(() => fadeInNewChoices(response), timeToWait);
  }

  function fadeInNewChoices(response): void {
    let newChoicesSection = $(response).find(choicesSectionSelector);
    $(choicesSectionSelector).replaceWith(newChoicesSection);
    let newChoices = $(choicesFormSelector);
    newChoices.hide();
    newChoices.fadeIn(quickFadeTime);
  }

  function handleAjaxError(): void {
    window.location.replace("/");
  }

  function fadeOutOldChoices(form): void {
    let otherChoices = $(choicesFormSelector).not(form);
    if (otherChoices.length > 0) {
      otherChoices.fadeTo(flashFadeTime, 0.01);
    }

    form.fadeTo(longFadeTime, 0.01);
  }
}

$(() => Website.initialize());
