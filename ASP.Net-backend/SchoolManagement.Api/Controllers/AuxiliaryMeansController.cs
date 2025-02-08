using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.AuxiliaryMeans;
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
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> CreateAuxiliaryMeans(AuxiliaryMeansDto auxiliaryMeans)
        {
           
            var createdAuxiliaryMeans = await _auxiliaryMeansService.CreateAuxiliaryMeansAsync(auxiliaryMeans);
            
            return Ok(createdAuxiliaryMeans);
        }

        [HttpGet]
        [Route("list")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult<IEnumerable<AuxiliaryMeansResponseDto>>> ListAuxiliaryMeans()
        {
            var auxiliaryMeansList = await _auxiliaryMeansService.ListAuxiliaryMeansAsync();
            return Ok(auxiliaryMeansList);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> UpdateAuxiliaryMeans(AuxiliaryMeansResponseDto auxiliaryMeans)
        {
            var updatedAuxiliaryMeans = await _auxiliaryMeansService.UpdateAuxiliaryMeansAsync(auxiliaryMeans);
            return Ok(updatedAuxiliaryMeans);
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> DeleteAuxiliaryMeans(int auxiliaryMeansId)
        {
            var auxiliaryMeanDto = await _auxiliaryMeansService.DeleteAuxiliaryMeansByIdAsync(auxiliaryMeansId);
            return Ok(auxiliaryMeanDto);
        }
    }
}
