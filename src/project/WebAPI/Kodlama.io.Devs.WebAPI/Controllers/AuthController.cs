using Core.Application.Requests;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.Users.Commands.CreateUser;
using Kodlama.io.Devs.Application.Features.Users.Commands.LoginUser;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            var response = await Mediator.Send(loginUserCommand);
            return Ok(response);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserCreateCommand userCreateCommand)
        {
            var response = await Mediator.Send(userCreateCommand);
            return Ok(response);
        }
    }
}
