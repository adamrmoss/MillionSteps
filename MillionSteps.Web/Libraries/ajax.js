﻿// Generated by IcedCoffeeScript 108.0.9
(function() {
  define(function() {
    var ajax;
    return ajax = {
      setupLiveForm: function(formSelector, success, error) {
        return $(document).on('submit', formSelector, {}, function(event) {
          var action, data, form, method;
          event.preventDefault();
          form = $(this);
          action = form.attr('action');
          method = form.attr('method');
          data = form.serialize();
          return $.ajax({
            url: action,
            type: method,
            data: data,
            dataType: 'html',
            success: success,
            error: error
          });
        });
      }
    };
  });

}).call(this);
