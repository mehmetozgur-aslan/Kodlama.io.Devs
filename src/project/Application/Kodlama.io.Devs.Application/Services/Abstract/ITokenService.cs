using Kodlama.io.Devs.Application.Features.Users.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Services.Abstract
{
    public interface ITokenService
    {
        TokenDto CreateToken(IdentityUser userApp);
    }
}
