/// <reference path="jquery.d.ts" />

namespace Xhr {
  let $xhr = $.ajaxSettings.xhr;
  export let result = null;

  export function xhr(): void {
    result = $xhr();
  }

  export function initialize(): void {
    $.ajaxSettings.xhr = xhr;
  }
}

$(Xhr.initialize);
