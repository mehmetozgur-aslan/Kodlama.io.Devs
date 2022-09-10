using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.SocialLinks.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.SocialLinks.Models
{
    public class GetListSocialLinkModel : BasePageableModel
    {
        public List<GetListSocialLinkDto> Items { get; set; }
    }
}
