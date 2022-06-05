using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleManager _userRoleManager;

        public UserRoleController(IUserRoleManager userRoleManager)
        {
            _userRoleManager = userRoleManager;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var userroless = _userRoleManager.GetAllUserRoles();
            return Ok(new { status = 200, message = userroless });
        }

        [HttpPost("add")]
        public IActionResult AddUserRole(AddUserRoleDTO userRoleDTO)
        {
            try
            {
                _userRoleManager.AddUserRole(userRoleDTO);
            }
            catch (Exception e)
            {
                return Ok(new { status = 400, message = e });
            }

            return Ok(new { status = 200, message = "UserRole elave olundu." });
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var role = _userRoleManager.GetUserRoleById(id);

            return Ok(new { status = 200, message = role });
        }
    }
}
