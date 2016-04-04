/// <reference path="jquery.d.ts" />

namespace Ajax {
  export let startTime = null;

  export function setupLiveForm(formSelector, success, error, meanwhile): void {
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

      meanwhile(form);
    });
  }
}
