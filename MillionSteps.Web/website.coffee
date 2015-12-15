define ['jquery', 'ajax'], ($, ajax) ->
  website =
    choicesFormSelector: 'form.choices'

    initialize: ->
      ajax.setupForm(website.choicesFormSelector, website.handleAjax, website.handleAjaxError)

    handleChoiceResponse: (response) ->
      newForm = $(response).find(website.choicesFormSelector)
      $(website.choicesFormSelector).replaceWith(newForm)

    handleAjaxError: ->
      alert('Ajax error')

  #website.initialize()

  website
