using Core.Application.Requests;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.Users.Commands.CreateUser;
using Kodlama.io.Devs.Application.Features.Users.Commands.LoginUser;
using Kodlama.io.Devs.Application.Features.Users.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> SignIn(CreateUserCommand createUserCommand)
        {            
            UserAppDto result = await Mediator.Send(createUserCommand);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserCommand loginUserCommand)
        {
            TokenDto result = await Mediator.Send(loginUserCommand);
            return Ok(result);
        }
    }
}
