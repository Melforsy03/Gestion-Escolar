using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ResponseDto.Secretary;
using SchoolManagement.Application.ApplicationServices.Services;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Identity;

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
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateSecretary(SecretaryDto secretary)
        {
            var secretaryCreated = await _secretaryService.CreateSecretaryAsync(secretary);
            return Ok(secretaryCreated);
        }

        [HttpGet]
        [Route("list")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<Secretary>>> ListSecretaries()
        {
            var secretaries = await _secretaryService.ListSecretariesAsync();
            return Ok(secretaries);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> UpdateSecretary(SecretaryResponseDto secretaryInfo)
        {
            var updatedSecretary = await _secretaryService.UpdateSecretaryAsync(secretaryInfo);
            return Ok(updatedSecretary);
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteSecretary(int secretaryId)
        {
            var secretary = await _secretaryService.DeleteSecretaryByIdAsync(secretaryId);
            return Ok(secretary);
        }

    }
}