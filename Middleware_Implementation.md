# Middleware Implementation

## Request Logging Middleware

I used Microsoft Copilot to create RequestLoggingMiddleware.

- It logs the HTTP method, request path, and response status code.
- It uses ILogger<RequestLoggingMiddleware>.
- It logs requests such as GET /Users and responses such as 200 OK or 201 Created.
- It was added to the application through UseRequestLogging().

## Error Handling Middleware

I used Microsoft Copilot to create ErrorHandlingMiddleware.

- It wraps the middleware pipeline in a try-catch block.
- It catches unhandled exceptions.
- It returns a consistent JSON error response:
  { "error": "Internal server error." }
- It returns HTTP status code 500 without exposing internal error details.
- It was added through UseErrorHandling().

## Token Authentication Middleware

I used Microsoft Copilot to create TokenAuthMiddleware.

- It reads the Authorization header from incoming requests.
- It expects the format Bearer <token>.
- The valid test token is valid-token-123.
- Requests with a missing or invalid token return 401 Unauthorized.
- Valid token requests are allowed to access the Users endpoints.
- It was added through UseTokenAuth().

## Middleware Pipeline Order

The middleware was configured in Program.cs in this order:

1. UseErrorHandling()
2. UseTokenAuth()
3. UseRequestLogging()

This order ensures that unhandled exceptions are caught globally, authentication protects the API endpoints, and authenticated requests/responses are logged.

## Tests Completed

- GET /Users with Bearer valid-token-123 returned 200 OK.
- GET /Users without a token returned 401 Unauthorized.
- GET /Users with an invalid token returned 401 Unauthorized.
- POST /Users with a valid token and unique email returned 201 Created.
- POST /Users with an already-used email was rejected by duplicate-email validation.
- Terminal logs recorded request methods, paths, and response status codes.

## How Copilot Helped

- Copilot generated logging, error-handling, and authentication middleware classes.
- Copilot generated extension methods to register middleware cleanly.
- Copilot helped configure the middleware pipeline in Program.cs.
- Copilot suggested the valid-token, invalid-token, duplicate-email, and successful-request test cases.