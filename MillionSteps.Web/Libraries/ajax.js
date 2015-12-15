﻿// Generated by IcedCoffeeScript 108.0.9
(function() {
  define(function() {
    var ajax;
    return ajax = {
      setupLiveForm: function(formSelector, success, error) {
        var _formSelector;
        _formSelector = formSelector;
        return $(document).on('submit', formSelector, {}, function(event) {
          var action, data, form, method, otherChoices, performAjax;
          event.preventDefault();
          form = $(this);
          otherChoices = $(_formSelector).not(form);
          if (otherChoices.length > 0) {
            otherChoices.fadeTo(100, 0.01);
          }
          form.fadeTo(800, 0.01);
          action = form.attr('action');
          method = form.attr('method');
          data = form.serialize();
          performAjax = function() {
            return $.ajax({
              url: action,
              type: method,
              data: data,
              dataType: 'html',
              success: success,
              error: error
            });
          };
          return window.setTimeout(performAjax, 800);
        });
      }
    };
  });

}).call(this);
