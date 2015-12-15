define ['ajax', 'xhr'], (ajax, xhr) ->
  _xhr = xhr

  website =
    choicesSectionSelector: 'section.choices'
    choicesFormSelector: 'section.choices form'

    handleChoiceResponse: (response) ->
      newUrl = _xhr.fromLastCall.responseURL
      history.replaceState({}, '', newUrl)
      newChoicesSection = $(response).find(website.choicesSectionSelector)
      $(website.choicesSectionSelector).replaceWith(newChoicesSection)
      newChoices = $(website.choicesFormSelector)
      newChoices.hide()
      newChoices.fadeIn(200)

    handleAjaxError: ->
      alert('Ajax error')

  ajax.setupLiveForm(website.choicesFormSelector, website.handleChoiceResponse, website.handleAjaxError)

  website
