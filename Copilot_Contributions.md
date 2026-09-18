## How Microsoft Copilot Helped

- Generated `Program.cs` with controllers, Swagger, and JSON camelCase configuration.
- Created the `User` model in `UserManagementAPI.Models` with `Id`, `Name`, `Email`, `Role`, and `CreatedAt` properties.
- Generated the full `UsersController` with CRUD endpoints (GET all, GET by id, POST, PUT, DELETE).
- Suggested correct HTTP status codes: 200 for reads, 201 for create, 204 for update/delete, 404 for not found.
- Implemented validation logic to ensure `Name` and `Email` are required and return 400 if missing.
- Suggested using data annotations (`[Required]`, `[EmailAddress]`, `[StringLength]`) on the `User` model and adding a `ModelState.IsValid` check in the `Create` endpoint.
- Helped implement custom middleware:
  - `ErrorHandlingMiddleware` for centralized exception handling.
  - `RequestLoggingMiddleware` to log incoming requests and response status codes.
  - `TokenAuthMiddleware` for simple token-based authentication.
- Provided example JSON payloads for POST and PUT requests used during testing in Swagger/Postman.
- Explained why `CreatedAtAction` is used for POST and why `NoContent()` is returned for PUT and DELETE.
- Assisted in debugging routing and validation issues by suggesting fixes to controller attributes and request models.