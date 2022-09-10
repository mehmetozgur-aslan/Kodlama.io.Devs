using AutoMapper;
using AutoMapper.Internal.Mappers;
using Kodlama.io.Devs.Application.Features.Users.Dtos;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserAppDto>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserAppDto>
        {
            private readonly UserManager<IdentityUser> _userManager;

            private readonly IMapper _mapper;

            public CreateUserHandler(UserManager<IdentityUser> userManager, IMapper mapper)
            {
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<UserAppDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {

                //Kurallar

                var user = new IdentityUser()
                {
                    Email = request.Email,
                    UserName = request.Username
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(x => x.Description).ToList();
                    throw new Exception(errors.ToString());
                }

                return _mapper.Map<UserAppDto>(user);
            }

        }
    }
}
