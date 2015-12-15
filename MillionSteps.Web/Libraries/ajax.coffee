define () ->
  ajax =
    flashFadeTime: 100
    quickFadeTime: 200
    longFadeTime: 1200

    startTime: null

    setupLiveForm: (formSelector, success, error) ->
      _formSelector = formSelector

      $(document).on('submit', formSelector, {}, (event) ->
        event.preventDefault()

        ajax.startTime = Date.now()

        form = $(this)
        action = form.attr('action')
        method = form.attr('method')
        data = form.serialize()

        $.ajax
          url: action
          type: method
          data: data
          dataType: 'html'
          success: success
          error: error

        otherChoices = $(_formSelector).not(form)
        if otherChoices.length > 0
          otherChoices.fadeTo(ajax.flashFadeTime, 0.01)
        form.fadeTo(ajax.longFadeTime, 0.01)        
      )
