/// <reference path="jquery.d.ts" />

namespace Ajax {
  export const flashFadeTime = 100;
  export const quickFadeTime = 200;
  export const longFadeTime = 1200;

  export let startTime = null;

  export function setupLiveForm(formSelector, success, error) {
    let _formSelector = formSelector;

    $(document).on("submit", formSelector, {}, (event) => {
      event.preventDefault();

      startTime = Date.now();

      let form = $(this);
      let action = form.attr("action");
      let method = form.attr("method");
      let data = form.serialize();

      $.ajax({
        url: action,
        type: method,
        data: data,
        dataType: "html",
        success: success,
        error: error
      });

      let otherChoices = $(_formSelector).not(form);
      if (otherChoices.length > 0) {
        otherChoices.fadeTo(flashFadeTime, 0.01);
      }

      form.fadeTo(longFadeTime, 0.01);
    });
  }
}
