define () ->
  ajax =
    setupLiveForm: (formSelector, success, error) ->
      $(document).on('submit', formSelector, {}, (event) ->
        event.preventDefault()
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
      )