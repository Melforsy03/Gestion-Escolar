using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Application.ApplicationServices.Services;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("restriction")]
    public class RestrictionController : ControllerBase
    {
        private readonly IRestrictionService _restrictionService;

        public RestrictionController(IRestrictionService restrictionService)
        {
            _restrictionService = restrictionService;
        }

        [HttpPost]
        [Route("create")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateRestriction(RestrictionDto restriction)
        {
            var restriction2 = await _restrictionService.CreateRestrictionAsync(restriction);
            return Ok(restriction2);
        }

        [HttpGet]
        [Route("list")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<Restriction>>> ListRestrictions()
        {
            var restrictions = await _restrictionService.ListRestrictionAsync();
            return Ok(restrictions);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateRestriction(RestrictionDto restriction)
        {
            var updatedRestriction = await _restrictionService.UpdateRestrictionAsync(restriction);
            return Ok(updatedRestriction);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteRestriction(int restrictionId)
        {
            await _restrictionService.DeleteRestrictionByIdAsync(restrictionId);
            return Ok();
        }
    }
}
