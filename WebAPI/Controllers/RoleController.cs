using Business.Abstract;
using Core.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleManager _roleManager;

        public RoleController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet("getall")]
        public List<Role> GetAll()
        {
            return _roleManager.GetAllRoles();
        }

        [HttpPost("add")]
        public object AddRole(Role role)
        {
            _roleManager.AddRole(role);
            return Ok(new { status = 200, message = "Rol ugurla elave edildi." });
        }

        [HttpPut("update")]
        public IActionResult UpdateRole(Role role)
        {
            _roleManager.UpdateRole(role);
            return Ok(new { status = 200, message = role });
        }

        [HttpDelete("remove")]
        public IActionResult DeleteRole(Role role)
        {
            _roleManager.RemoveRole(role);
            return Ok(new { status = 200, message = "Rol ugurla silindi." });
        }
    }
}
