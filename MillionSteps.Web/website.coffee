define ['ajax', 'xhr'], (ajax, xhr) ->
  _xhr = xhr

  website =
    choicesSectionSelector: 'section.choices'
    choicesFormSelector: 'section.choices form'

    handleChoiceResponse: (response) ->
      newUrl = _xhr.fromLastCall.responseURL
      history.replaceState({}, '', newUrl)

      timeWaited = Date.now() - ajax.startTime
      timeToWait = Math.max(0, ajax.longFadeTime - timeWaited)

      fadeInNewChoices = ->
        newChoicesSection = $(response).find(website.choicesSectionSelector)
        $(website.choicesSectionSelector).replaceWith(newChoicesSection)
        newChoices = $(website.choicesFormSelector)
        newChoices.hide()
        newChoices.fadeIn(ajax.quickFadeTime)

      window.setTimeout(fadeInNewChoices, timeToWait)

    handleAjaxError: ->
      window.location.replace('/')

  ajax.setupLiveForm(website.choicesFormSelector, website.handleChoiceResponse, website.handleAjaxError)

  website
