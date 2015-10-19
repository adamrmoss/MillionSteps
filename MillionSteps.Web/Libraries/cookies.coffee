define [], ($) ->
  cookies =
    get: (name) ->
      cookieString = document.cookie + ";"
      startIndex = cookieString.indexOf(name + "=")
      endIndex = cookieString.indexOf(";", startIndex)
      if (startIndex == -1)
        null
      else
        cookieString.substring(startIndex, endIndex)

  cookies
