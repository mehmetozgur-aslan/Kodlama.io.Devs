using Core.Application.Requests;
using Kodlama.io.Devs.Application.Features.SocialLinks.Commands.CreateSocialLink;
using Kodlama.io.Devs.Application.Features.SocialLinks.Commands.DeleteSocialLink;
using Kodlama.io.Devs.Application.Features.SocialLinks.Commands.UpdateSocialLink;
using Kodlama.io.Devs.Application.Features.SocialLinks.Dtos;
using Kodlama.io.Devs.Application.Features.SocialLinks.Models;
using Kodlama.io.Devs.Application.Features.SocialLinks.Queries.GetListSocialLink;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialLinksController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateSocialLinkCommand createSocialLinkCommand)
        {
            CreatedSocialLinkDto createdSocialLinkDto = await Mediator.Send(createSocialLinkCommand);
            return Created("", createdSocialLinkDto);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteSocialLinkCommand deleteSocialLinkCommand)
        {
            DeletedSocialLinkDto deletedSocialLinkDto = await Mediator.Send(deleteSocialLinkCommand);
            return Ok(deletedSocialLinkDto);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSocialLinkCommand updateSocialLinkCommand)
        {
            UpdatedSocialLinkDto updatedSocialLinkDto = await Mediator.Send(updateSocialLinkCommand);
            return Ok(updatedSocialLinkDto);
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSocialLinkQuery query = new() { PageRequest = pageRequest };
            GetListSocialLinkModel getListSocialLinkModel = await Mediator.Send(query);
            return Ok(getListSocialLinkModel);
        }
    }
}
