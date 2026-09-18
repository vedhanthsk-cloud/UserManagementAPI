## How Microsoft Copilot Helped

- Generated `Program.cs` with controllers, Swagger, and JSON camelCase configuration.
- Created the `User` model in `UserManagementAPI.Models` with Id, Name, Email, Role, and CreatedAt properties.
- Generated the full `UsersController` with CRUD endpoints (GET all, GET by id, POST, PUT, DELETE).
- Suggested correct HTTP status codes: 200 for reads, 201 for create, 204 for update/delete, 404 for not found.
- Implemented validation logic to ensure Name and Email are required and return 400 if missing.
- Provided example JSON payloads for POST and PUT requests used during testing in Swagger/Postman.
- Explained why `CreatedAtAction` is used for POST and why `NoContent()` is returned for PUT and DELETE.