﻿// Generated by IcedCoffeeScript 108.0.9
(function() {
  define(['jquery', 'ajax'], function($, ajax) {
    var website;
    website = {
      choicesFormSelector: 'form.choices',
      initialize: function() {
        return ajax.setupForm(website.choicesFormSelector, website.handleAjax, website.handleAjaxError);
      },
      handleChoiceResponse: function(response) {
        var newForm;
        newForm = $(response).find(website.choicesFormSelector);
        return $(website.choicesFormSelector).replaceWith(newForm);
      },
      handleAjaxError: function() {
        return alert('Ajax error');
      }
    };
    return website;
  });

}).call(this);
