using AutoMapper;
using Kodlama.io.Devs.Application.Features.Users.Dtos;
using Kodlama.io.Devs.Application.Services.Abstract;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<TokenDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginUserHandler : IRequestHandler<LoginUserCommand, TokenDto>
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly ITokenService _tokenService;
            private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
            private readonly IMapper _mapper;

            public LoginUserHandler(UserManager<IdentityUser> userManager, IMapper mapper, ITokenService tokenService, IUserRefreshTokenRepository userRefreshTokenRepository)
            {
                _mapper = mapper;
                _tokenService = tokenService;
                _userManager = userManager;
                _userRefreshTokenRepository = userRefreshTokenRepository;
            }

            public async Task<TokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {

                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    // return Response<TokenDto>.Fail("Email or Password is wrong", 400, true);
                }

                if (!await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    // return Response<TokenDto>.Fail("Email or Password is wrong", 400, true);
                }

                var token = _tokenService.CreateToken(user);

                var userRefreshToken = await _userRefreshTokenRepository.GetAsync(x => x.UserId == user.Id);

                if (userRefreshToken == null)
                {
                    await _userRefreshTokenRepository.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
                }
                else
                {
                    userRefreshToken.Code = token.RefreshToken;
                    userRefreshToken.Expiration = token.RefreshTokenExpiration;
                    await _userRefreshTokenRepository.UpdateAsync(userRefreshToken);
                }

                return token;
            }
        }
    }
}
