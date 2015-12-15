define ['ajax', 'xhr'], (ajax, xhr) ->
  website =
    choicesSectionSelector: 'section.choices'
    choicesFormSelector: 'section.choices form'

    xhr: xhr

    handleChoiceResponse: (response) ->
      newUrl = website.xhr.fromLastCall.responseURL
      history.replaceState({}, '', newUrl)
      newChoicesSection = $(response).find(website.choicesSectionSelector)
      $(website.choicesSectionSelector).replaceWith(newChoicesSection)

    handleAjaxError: ->
      alert('Ajax error')

  ajax.setupLiveForm(website.choicesFormSelector, website.handleChoiceResponse, website.handleAjaxError)

  website
