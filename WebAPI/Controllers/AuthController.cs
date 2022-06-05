using Business.Abstract;
using Core.Entity.Models;
using Core.Security.Hashing;
using Core.Security.TokenHandler;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthManager _authManager;
        private readonly TokenGenerator _tokenGenerator;
        private readonly HashingHandler _hashingHandler;
        private readonly IRoleManager _iroleManager;

        public AuthController(IAuthManager authManager, TokenGenerator tokenGenerator, HashingHandler hashingHandler, IRoleManager iroleManager)
        {
            _authManager = authManager;
            _tokenGenerator = tokenGenerator;
            _hashingHandler = hashingHandler;
            _iroleManager = iroleManager;
        }

        [HttpPost("login")]
        public async Task<object> LoginUser(LoginDTO model)
        {
            var user = _authManager.Login(model.Email);

            if (user == null) return Ok(new {status = 404, message = "Bele bir istifadeci tapilmadi."});

            if(user.Email == model.Email && user.Password == _hashingHandler.PasswordHash(model.Password))
            {
                var resultUser = new UserDTO(user.FullName, user.Email);
                resultUser.Token = _tokenGenerator.Token(user);

                return Ok(new {status= 200, message = resultUser});
            }
            return Ok(new { status = 404, message = "Email ve ya sifre sehvdir." });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {

            //var userCheck = model.Email;

            //if (userCheck != )
            //{
            //    return Ok(new { status = 404, message = "Bu adli hesab movcuddur." });
            //}

            var pass = model.Password;

            if(pass.Length >= 5)
            {
                _authManager.Register(model);
                return Ok("Okeydi.");
            }
            return Ok(new { status = 404, message = "Parolunuzun uzunlugu en az 5 simvol olmalidir." });



        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        [HttpGet("allusers")]
        public List<K205User> GetUsers()
        {
            return _authManager.GetUsers();
        }

        [HttpGet("getuserbyrole/{userId}")]
        public async Task<Role> GetUserByRole(int userId)
        {
            return _iroleManager.GetRole(userId);
        }
        
    }
}
