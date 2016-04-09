/// <reference path="jquery.d.ts" />

namespace Ajax {
  export let startTime = null;

  export function setupLiveForm(formSelector, success, error, meanwhile): void {
    $(document).on("submit", formSelector, {}, (event) => {
      event.preventDefault();

      startTime = Date.now();

      let form = $(event.target).closest("form");
      let action = form.attr("action");
      let method = form.attr("method");
      let data = form.serialize();

      $.ajax({
        url: action,
        type: method,
        data: data,
        dataType: "html",
        success: (response) => {
          Ajax.respondToAjax(success, response);
        },
        error: error
      });

      meanwhile(form);
    });
  }

  export function respondToAjax(success, response) {
    let newUrl = Xhr.result.responseURL;
    history.replaceState({}, "", newUrl);
    success(response);
  }
}
