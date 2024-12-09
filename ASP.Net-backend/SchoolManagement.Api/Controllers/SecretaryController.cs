using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Application.ApplicationServices.Services;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("secretary")]
    public class SecretaryController : ControllerBase
    {
        private readonly ISecretaryService _secretaryService;

        public SecretaryController(ISecretaryService secretaryService)
        {
            _secretaryService = secretaryService;
        }

        [HttpPost]
        [Route("create")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateSecretary(SecretaryDto secretary)
        {
            var secretaryCreated = await _secretaryService.CreateSecretaryAsync(secretary);
            return Ok(secretaryCreated);
        }

        [HttpGet]
        [Route("list")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<Secretary>>> ListSecretaries()
        {
            var secretaries = await _secretaryService.ListSecretariesAsync();
            return Ok(secretaries);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateSecretary(SecretaryDto secretary)
        {
            var updatedSecretary = await _secretaryService.UpdateSecretaryAsync(secretary);
            return Ok(updatedSecretary);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteSecretary(int secretaryId)
        {
            await _secretaryService.DeleteSecretaryByIdAsync(secretaryId);
            return Ok();
        }

    }
}