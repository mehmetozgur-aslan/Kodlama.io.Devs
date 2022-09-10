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

namespace Kodlama.io.Devs.Application.Features.SocialLinks.Commands.DeleteSocialLink
{
    public class DeleteSocialLinkCommand : IRequest<DeletedSocialLinkDto>
    {
        public int Id { get; set; }
    }
    public class DeleteSocialLinkCommandHanler : IRequestHandler<DeleteSocialLinkCommand, DeletedSocialLinkDto>
    {
        private readonly IMapper mapper;
        private readonly ISocialLinkRepository socialLinkRepository;
        private readonly SocialLinkBusinessRules socialLinkBusinessRules;

        public DeleteSocialLinkCommandHanler(IMapper mapper, ISocialLinkRepository socialLinkRepository, SocialLinkBusinessRules socialLinkBusinessRules)
        {
            this.mapper = mapper;
            this.socialLinkRepository = socialLinkRepository;
            this.socialLinkBusinessRules = socialLinkBusinessRules;
        }

        public async Task<DeletedSocialLinkDto> Handle(DeleteSocialLinkCommand request, CancellationToken cancellationToken)
        {

            SocialLink socialLink = await socialLinkRepository.GetAsync(s => s.Id == request.Id);
            await socialLinkBusinessRules.SocialLinkMustBeExistWhenRequested(socialLink);
            SocialLink deletedLink = await socialLinkRepository.DeleteAsync(socialLink);
            var mappedLink = mapper.Map<DeletedSocialLinkDto>(deletedLink);
            return mappedLink;
        }
    }

}
