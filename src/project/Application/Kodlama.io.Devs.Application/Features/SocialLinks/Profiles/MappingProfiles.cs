using AutoMapper;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.SocialLinks.Commands.CreateSocialLink;
using Kodlama.io.Devs.Application.Features.SocialLinks.Commands.DeleteSocialLink;
using Kodlama.io.Devs.Application.Features.SocialLinks.Commands.UpdateSocialLink;
using Kodlama.io.Devs.Application.Features.SocialLinks.Dtos;
using Kodlama.io.Devs.Application.Features.SocialLinks.Models;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.SocialLinks.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
           
            CreateMap<IPaginate<SocialLink>, GetListSocialLinkModel>().ReverseMap();
            CreateMap<SocialLink, GetListSocialLinkDto>().ReverseMap();
            CreateMap<SocialLink, CreatedSocialLinkDto>().ReverseMap();
            CreateMap<SocialLink, CreateSocialLinkCommand>().ReverseMap();
            CreateMap<SocialLink, UpdatedSocialLinkDto>().ReverseMap();
            CreateMap<SocialLink, UpdateSocialLinkCommand>().ReverseMap();
            CreateMap<SocialLink, DeletedSocialLinkDto>().ReverseMap();
            CreateMap<SocialLink, DeleteSocialLinkCommand>().ReverseMap();

        }
    }

}
