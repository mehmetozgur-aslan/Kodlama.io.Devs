using Application.Features.Users.Rules;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Users.Commands.CreateUser
{
    public class UserCreateCommand : UserForRegisterDto, IRequest<AccessToken>
    {

    }
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, AccessToken>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly ITokenHelper tokenHelper;
        private readonly UserBusinessRules userBusinessRules;

        public UserCreateCommandHandler(IMapper mapper, IUserRepository userRepository, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.tokenHelper = tokenHelper;
            this.userBusinessRules = userBusinessRules;
        }

        async Task<AccessToken> IRequestHandler<UserCreateCommand, AccessToken>.Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            await userBusinessRules.UserEmailCannotBeDuplicated(user.Email);
            User newUser = await userRepository.AddAsync(user);
            var token = tokenHelper.CreateToken(newUser, new List<OperationClaim>());
            return token;
        }
    }
}
