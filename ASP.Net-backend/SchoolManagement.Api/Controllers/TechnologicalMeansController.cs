using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("technologicalmeans")]
    public class TechnologicalMeansController : ControllerBase
    {
        private readonly ITechnologicalMeansService _technologicalMeansService;

        public TechnologicalMeansController(ITechnologicalMeansService technologicalMeansService)
        {
            _technologicalMeansService = technologicalMeansService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> CreateTechnologicalMeans(TechnologicalMeansDto technologicalMeans)
        {
            var createdTechnologicalMeans = await _technologicalMeansService.CreateTechnologicalMeansAsync(technologicalMeans);
            return Ok(createdTechnologicalMeans);
        }

        [HttpGet]
        [Route("list")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult<IEnumerable<TechnologicalMeans>>> ListTechnologicalMeans()
        {
            var technologicalMeansList = await _technologicalMeansService.ListTechnologicalMeansAsync();
            return Ok(technologicalMeansList);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> UpdateTechnologicalMeans(TechnologicalMeansDto technologicalMeans)
        {
            var updatedTechnologicalMeans = await _technologicalMeansService.UpdateTechnologicalMeansAsync(technologicalMeans);
            return Ok(updatedTechnologicalMeans);
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> DeleteTechnologicalMeans(int technologicalMeansId)
        {
            await _technologicalMeansService.DeleteTechnologicalMeansByIdAsync(technologicalMeansId);
            return Ok();
        }
    }
}
