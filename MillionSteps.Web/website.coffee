define ['jquery', 'ajax', 'cookies'], ($, ajax, cookies) ->
  website =
    initialize: ->
      return null

  #ajax.setupForm(login.selector, (-> website.handleLogin()), (-> website.handleError()))
  website.initialize()

  website
