# MSCloudExperts

## Identity API
- Register a new User entering an email and password
http://localhost:8082/swagger/index.html

## OlympicGames API
- Secured Rest API to CRUD the **weightlifting** resource
  http://localhost:8081/swagger/index.html
- Main Query
  http://localhost:8081/weightlifting/rank

## Configure New Token POSTMAN
- Type:              Oauth 2.0
- Grant type:        Authorization Code (With PKCE)
- Callback URL:      urn:ietf:wg:oauth:2.0:oob
- Auth URL:          http://localhost:8082/connect/authorize
- Access Token URL:  http://localhost:8082/connect/token
- Client ID:         postman
- Scope:             olympicgames.fullaccess
