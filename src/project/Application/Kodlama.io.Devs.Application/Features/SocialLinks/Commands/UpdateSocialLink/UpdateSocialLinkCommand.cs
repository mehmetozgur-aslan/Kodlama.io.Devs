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

namespace Kodlama.io.Devs.Application.Features.SocialLinks.Commands.UpdateSocialLink
{
    public class UpdateSocialLinkCommand : IRequest<UpdatedSocialLinkDto>
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
    }
    public class UpdateSocialLinkCommandHandler : IRequestHandler<UpdateSocialLinkCommand, UpdatedSocialLinkDto>
    {
        private readonly IMapper mapper;
        private readonly ISocialLinkRepository socialLinkRepository;
        private readonly SocialLinkBusinessRules socialLinkBusinessRules;

        public UpdateSocialLinkCommandHandler(IMapper mapper, ISocialLinkRepository socialLinkRepository, SocialLinkBusinessRules socialLinkBusinessRules)
        {
            this.mapper = mapper;
            this.socialLinkRepository = socialLinkRepository;
            this.socialLinkBusinessRules = socialLinkBusinessRules;
        }

        public async Task<UpdatedSocialLinkDto> Handle(UpdateSocialLinkCommand request, CancellationToken cancellationToken)
        {

            var socialLink = mapper.Map<SocialLink>(request);
            socialLinkBusinessRules.SocialLinkMustBeExistWhenRequested(socialLink);
            SocialLink updatedLink = await socialLinkRepository.UpdateAsync(socialLink);
            var mappedLink = mapper.Map<UpdatedSocialLinkDto>(updatedLink);
            return mappedLink;
        }
    }
}
