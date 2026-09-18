using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private static readonly List<User> Users = [];
    private static readonly Regex EmailPattern = new("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static int _nextId = 1;

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        try
        {
            return Ok(Users);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }

    [HttpGet("{id:int}")]
    public ActionResult<User> GetById(int id)
    {
        try
        {
            var user = Users.FirstOrDefault(user => user.Id == id);

            return user is null ? NotFound() : Ok(user);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }

    [HttpPost]
    public ActionResult<User> Create(User user)
    {
        try
        {
            var validationError = ValidateUser(user);

            if (validationError is not null)
            {
                return BadRequest(validationError);
            }

            user.Name = user.Name.Trim();
            user.Email = user.Email.Trim();
            user.Id = _nextId++;
            Users.Add(user);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, User updatedUser)
    {
        try
        {
            var validationError = ValidateUser(updatedUser, id);

            if (validationError is not null)
            {
                return BadRequest(validationError);
            }

            var user = Users.FirstOrDefault(user => user.Id == id);

            if (user is null)
            {
                return NotFound();
            }

            user.Name = updatedUser.Name.Trim();
            user.Email = updatedUser.Email.Trim();
            user.Role = updatedUser.Role;

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var user = Users.FirstOrDefault(user => user.Id == id);

            if (user is null)
            {
                return NotFound();
            }

            Users.Remove(user);

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }

    private static string? ValidateUser(User? user, int? existingUserId = null)
    {
        if (user is null)
        {
            return "A user is required.";
        }

        if (string.IsNullOrWhiteSpace(user.Name))
        {
            return "Name is required.";
        }

        if (user.Name.Trim().Length > 100)
        {
            return "Name must be 100 characters or fewer.";
        }

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            return "Email is required.";
        }

        var email = user.Email.Trim();

        if (email.Length > 150)
        {
            return "Email must be 150 characters or fewer.";
        }

        if (!EmailPattern.IsMatch(email))
        {
            return "Email must be a valid email address.";
        }

        if (Users.Any(existingUser =>
            existingUser.Id != existingUserId &&
            string.Equals(
                existingUser.Email.Trim(),
                email,
                StringComparison.OrdinalIgnoreCase)))
        {
            return "Email is already in use.";
        }

        return null;
    }
}