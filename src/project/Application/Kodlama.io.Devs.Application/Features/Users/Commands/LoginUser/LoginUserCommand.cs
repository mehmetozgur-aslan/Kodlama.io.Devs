using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Kodlama.io.Devs.Application.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommand : UserForLoginDto, IRequest<AccessToken>
    {
    }
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, AccessToken>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly ITokenHelper tokenHelper;

        public LoginUserHandler(IMapper mapper, IUserRepository userRepository, ITokenHelper tokenHelper)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.tokenHelper = tokenHelper;
        }
        public async Task<AccessToken> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetAsync(u => u.Email.ToLower() == request.Email.ToLower(),
               include: u => u.Include(u => u.UserOperationClaims).ThenInclude(o => o.OperationClaim));
            if (!HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new BusinessException("Given Email or Password wrong.");
            }
            List<OperationClaim> operationClaims = new List<OperationClaim>();
            foreach (var operationClaim in user.UserOperationClaims)
            {
                operationClaims.Add(operationClaim.OperationClaim);
            }
            var token = tokenHelper.CreateToken(user, operationClaims);
            return token;
        }
    }

}
