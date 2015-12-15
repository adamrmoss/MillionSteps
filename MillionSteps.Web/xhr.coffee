define ['jquery'], ($) ->
  xhr =
    originalXhr: $.ajaxSettings.xhr
    fromLastCall: null

  $.ajaxSettings.xhr = () ->
    xhr.fromLastCall = xhr.originalXhr()

  xhr
