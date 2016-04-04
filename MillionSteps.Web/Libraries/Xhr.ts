/// <reference path="jquery.d.ts" />

namespace Xhr {
  let $xhr = $.ajaxSettings.xhr;
  export let result = null;

  export function xhr() {
    result = $xhr();
  }
}

$.ajaxSettings.xhr = Xhr.xhr;
