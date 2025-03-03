using Microsoft.AspNetCore.Mvc;
using Study_dashboard_API.Data;
using Study_dashboard_API.Filters.ActionFilters;
using Study_dashboard_API.Filters.ExceptionFilters;
using Study_dashboard_API.Models;
using Study_dashboard_API.Models.Repositories;

namespace Study_dashboard_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly ApplicationDbContext db;

        public UsersController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult getUsers()
        {
            return Ok(db.Users.ToList());
        }
        [HttpGet("{id}")]
        [TypeFilter(typeof(User_ValidateUserIdFilterAttribute))]
        public IActionResult getUserById(int id)
        {
            return Ok(HttpContext.Items["user"]);
        }
        [HttpPut("{id}")]
        [TypeFilter(typeof(User_ValidateUserIdFilterAttribute))]
        [User_ValidateUpdateUserFilter]
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
        [HttpPost]
        [TypeFilter(typeof(User_ValidateAddUserFilterAttribute))]
        public IActionResult createUser([FromBody]User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return CreatedAtAction(nameof(getUserById), new { id = user.UserId }, user);
        }
        [HttpDelete("{id}")]
        [TypeFilter(typeof(User_ValidateUserIdFilterAttribute))]
        public IActionResult deleteUser(int id)
        {
            var user = HttpContext.Items["user"] as User;
            db.Users.Remove(user);
            db.SaveChanges();
            return Ok(user);
        }
    }
}
