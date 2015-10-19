define ['jquery'], ($) ->
  ajax =
    setupForm: (form, success, error) ->
      $(form).submit (event) ->
        event.preventDefault()
        form = $(this)
        action = form.attr('action')
        method = form.attr('method')
        data = form.serialize()
        $.ajax
          url: action
          type: method
          data: data
          dataType: 'json'
          success: success
          error: error

  ajax
