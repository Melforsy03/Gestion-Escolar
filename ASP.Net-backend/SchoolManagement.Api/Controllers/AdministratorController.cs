using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ResponseDto.Administrator;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("administrator")]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorService _administratorService;

        public AdministratorController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateAdministrator(AdministratorDto administrator)
        {
            var administratorCreated = await _administratorService.CreateAdministratorAsync(administrator);
            return Ok(administratorCreated);
        }

        [HttpGet]
        [Route("list")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<AdministratorResponseDto>>> ListAdministrators()
        {
            var administrators = await _administratorService.ListAdministratorsAsync();
            return Ok(administrators);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> UpdateAdministrator(AdministratorResponseDto administratorInfo)
        {
            var updatedAdministrator = await _administratorService.UpdateAdministratorAsync(administratorInfo);
            return Ok(updatedAdministrator);
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteAdministrator(int administratorId)
        {
            var administrator = await _administratorService.DeleteAdministratorByIdAsync(administratorId);
            return Ok(administrator);
        }
    }

}
