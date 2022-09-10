using AutoMapper;
using Kodlama.io.Devs.Application.Features.SocialLinks.Dtos;
using Kodlama.io.Devs.Application.Features.SocialLinks.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.SocialLinks.Commands.CreateSocialLink
{
    public class CreateSocialLinkCommand : IRequest<CreatedSocialLinkDto>
    {
        public string Url { get; set; }
        public int UserId { get; set; }
    }
    public class CreateSocialLinkCommandHandler : IRequestHandler<CreateSocialLinkCommand, CreatedSocialLinkDto>
    {
        private readonly IMapper mapper;
        private readonly ISocialLinkRepository socialLinkRepository;
        private readonly SocialLinkBusinessRules socialLinkBusinessRules;

        public CreateSocialLinkCommandHandler(IMapper mapper, ISocialLinkRepository socialLinkRepository, SocialLinkBusinessRules socialLinkBusinessRules)
        {
            this.mapper = mapper;
            this.socialLinkRepository = socialLinkRepository;
            this.socialLinkBusinessRules = socialLinkBusinessRules;
        }

        public async Task<CreatedSocialLinkDto> Handle(CreateSocialLinkCommand request, CancellationToken cancellationToken)
        {
            await socialLinkBusinessRules.SocialLinkCannotBeDuplicated(request.Url);
            var socialLink = mapper.Map<SocialLink>(request);
            SocialLink createdLink = await socialLinkRepository.AddAsync(socialLink);
            var mappedLink = mapper.Map<CreatedSocialLinkDto>(createdLink);
            return mappedLink;
        }
    }

}
