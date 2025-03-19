using Microsoft.AspNetCore.Mvc;
using Study_dashboard_API.Data;
using Study_dashboard_API.Filters.ActionFilters;
using Study_dashboard_API.Filters.AuthFilters;
using Study_dashboard_API.Filters.ExceptionFilters;
using Study_dashboard_API.Models;
using Study_dashboard_API.Security;

namespace Study_dashboard_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IPasswordHasher passwordHasher;

        public UsersController(ApplicationDbContext db, IPasswordHasher passwordHasher)
        {
            this.db = db;
            this.passwordHasher = passwordHasher;
        }

        // GET: api/users
        // Returns all users (only accessible with valid JWT)
        [HttpGet]
        [JwtTokenAuthFilter]
        public IActionResult getUsers()
        {
            return Ok(db.Users.ToList());
        }

        // GET: api/users/{id}
        // Returns a user by ID (authorization and validation applied)
        [HttpGet("{id}")]
        [JwtTokenAuthFilter]
        [TypeFilter(typeof(User_ValidateUserIdFilterAttribute))]
        public IActionResult getUserById(int id)
        {
            return Ok(HttpContext.Items["user"]);
        }

        // GET: api/users/current
        // Returns currently authenticated user (based on JWT)
        [HttpGet("current")]
        [JwtTokenAuthFilter]
        public IActionResult getUser()
        {
            var userId = HttpContext.Items["UserId"] as int?;
            var user = db.Users.Find(userId);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        // PUT: api/users/{id}
        // Updates user data (only if valid and authorized)
        [HttpPut("{id}")]
        [JwtTokenAuthFilter]
        [TypeFilter(typeof(User_ValidateUserIdFilterAttribute))]
        [TypeFilter(typeof(User_ValidateUpdateUserFilterAttribute))]
        [TypeFilter(typeof(User_HandleUpdateUserExceptionFilterAttribute))]
        public IActionResult updateUser(int id, User user)
        {
            var updatedUser = HttpContext.Items["user"] as User;
            updatedUser.Name = user.Name;
            updatedUser.Email = user.Email;
            updatedUser.Password = user.Password;

            db.SaveChanges();
            return NoContent();
        }

        // POST: api/users
        // Registers a new user (with password hashing)
        [HttpPost]
        [TypeFilter(typeof(User_ValidateAddUserFilterAttribute))]
        public IActionResult createUser([FromBody]User user)
        {
            var passwordHash = passwordHasher.HashPassword(user.Password);
            user.Password = passwordHash;
            db.Users.Add(user);
            db.SaveChanges();
            return CreatedAtAction(nameof(getUserById), new { id = user.UserId }, user);
        }

        // DELETE: api/users/{id}
        // Deletes a user (after validation and authorization)
        [HttpDelete("{id}")]
        [JwtTokenAuthFilter]
        [TypeFilter(typeof(User_ValidateUserIdFilterAttribute))]
        public IActionResult deleteUser(int id)
        {
            var user = HttpContext.Items["user"] as User;
            db.Users.Remove(user);
            db.SaveChanges();
            return Ok(user);
        }

        // PUT: api/users/updatePassword
        // Allows authenticated users to change their password
        [HttpPut("updatePassword")]
        [JwtTokenAuthFilter]
        [TypeFilter(typeof(User_ValidateUpdatePasswordFilterAttribute))]
        public IActionResult UpdatePassword([FromBody] ChangePasswordDto model)
        {
            var user = HttpContext.Items["user"] as User;

            var passwordHash = passwordHasher.HashPassword(model.NewPassword);
            user.Password = passwordHash;
            db.SaveChanges();

            return NoContent();
        }
    }
}
