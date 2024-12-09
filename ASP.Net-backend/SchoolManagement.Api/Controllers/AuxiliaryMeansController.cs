using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("auxiliarymeans")]
    public class AuxiliaryMeansController : ControllerBase
    {
        private readonly IAuxiliaryMeansService _auxiliaryMeansService;

        public AuxiliaryMeansController(IAuxiliaryMeansService auxiliaryMeansService)
        {
            _auxiliaryMeansService = auxiliaryMeansService;
        }

        [HttpPost]
        [Route("create")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateAuxiliaryMeans(AuxiliaryMeansDto auxiliaryMeans)
        {
            var createdAuxiliaryMeans = await _auxiliaryMeansService.CreateAuxiliaryMeansAsync(auxiliaryMeans);
            return Ok(createdAuxiliaryMeans);
        }

        [HttpGet]
        [Route("list")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<AuxiliaryMeans>>> ListAuxiliaryMeans()
        {
            var auxiliaryMeansList = await _auxiliaryMeansService.ListAuxiliaryMeansAsync();
            return Ok(auxiliaryMeansList);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateAuxiliaryMeans(AuxiliaryMeansDto auxiliaryMeans)
        {
            var updatedAuxiliaryMeans = await _auxiliaryMeansService.UpdateAuxiliaryMeansAsync(auxiliaryMeans);
            return Ok(updatedAuxiliaryMeans);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAuxiliaryMeans(int auxiliaryMeansId)
        {
            await _auxiliaryMeansService.DeleteAuxiliaryMeansByIdAsync(auxiliaryMeansId);
            return Ok();
        }
    }
}
