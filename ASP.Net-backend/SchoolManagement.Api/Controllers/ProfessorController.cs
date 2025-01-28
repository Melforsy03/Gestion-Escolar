using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Professor;
using SchoolManagement.Application.ApplicationServices.Services;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Identity;

namespace SchoolManagement.Api.Controllers
{

    [ApiController]
    [Route("professor")]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorService _professorService;
        public ProfessorController(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "SuperAdmin, Secretary")]
        public async Task<IActionResult> CreateProfessor(ProfessorDto professorDto)
        {
            var professor2 = await _professorService.CreateProfessorAsync(professorDto);
            return Ok(professor2);
        }

        [HttpGet]
        [Route("list")]
        [Authorize(Roles = "SuperAdmin, Secretary")]
        public async Task<ActionResult<IEnumerable<ProfessorResponseDto>>> ListProfessor()
        {
            var professor = await _professorService.ListProfessorAsync();
            return Ok(professor);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "SuperAdmin, Secretary")]
        public async Task<ActionResult> UpdateProfessor(ProfessorResponseDto professorInfo)
        {
            var professor = await _professorService.UpdateProfessorAsync(professorInfo);
            return Ok(professor);
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteProfessor(int professorId)
        {
            var professor = await _professorService.DeleteProfessorByIdAsync(professorId);
            return Ok(professor);
        }
    }

}
