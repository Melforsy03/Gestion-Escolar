using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
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
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateAdministrator(AdministratorDto administrator)
        {
            var createdAdministrator = await _administratorService.CreateAdministratorAsync(administrator);
            return Ok(createdAdministrator);
        }

        [HttpGet]
        [Route("list")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<Administrator>>> ListAdministrator()
        {
            var administratorList = await _administratorService.ListAdministratorAsync();
            return Ok(administratorList);
        }

        [HttpPut]
        [Route("update")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> UpdateAdministrator(AdministratorDto administrator)
        {
            var updatedAdministrator = await _administratorService.UpdateAdministratorAsync(administrator);
            return Ok(updatedAdministrator);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAdministrator(int administratorId)
        {
            await _administratorService.DeleteAdministratorByIdAsync(administratorId);
            return Ok();
        }
    }

}
