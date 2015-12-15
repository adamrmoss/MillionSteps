define ['jquery', 'ajax'], ($, ajax) ->
  website =
    choicesSectionSelector: 'section.choices'
    choicesFormSelector: 'section.choices form'

    initialize: ->
      ajax.setupLiveForm(website.choicesFormSelector, website.handleChoiceResponse, website.handleAjaxError)

    handleChoiceResponse: (response) ->      
      newChoicesSection = $(response).find(website.choicesSectionSelector)
      $(website.choicesSectionSelector).replaceWith(newChoicesSection)

    handleAjaxError: ->
      alert('Ajax error')

  website.initialize()

  website
