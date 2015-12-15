define () ->
  ajax =
    ajaxStartTime: null

    setupLiveForm: (formSelector, success, error) ->
      _formSelector = formSelector

      $(document).on('submit', formSelector, {}, (event) ->
        event.preventDefault()

        form = $(this)
        otherChoices = $(_formSelector).not(form)
        if otherChoices.length > 0
          otherChoices.fadeTo(100, 0.01)
        form.fadeTo(800, 0.01)        

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
      )
