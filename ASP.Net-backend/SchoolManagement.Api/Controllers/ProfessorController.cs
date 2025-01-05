using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
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
            //[Authorize(Roles = "SuperAdmin")]
            public async Task<IActionResult> CreateProfessor(User professor)
            {
                var professor2 = await _professorService.CreateProfessorAsync(professor);
                return Ok(professor2);
            }

            [HttpGet]
            [Route("list")]
            //[Authorize (Roles = "SuperAdmin")]
            public async Task<ActionResult<IEnumerable<Professor>>> ListProfessor()
            {
                var professor = await _professorService.ListProfessorAsync();
                return Ok(professor);
            }

            [HttpPut]
            [Route("update")]
            public async Task<ActionResult> UpdateProfessor(ProfessorDto professor)
            {
                var _professor = await _professorService.UpdateProfessorAsync(professor);
                return Ok(_professor);
            }

            [HttpDelete]
            [Route("delete")]
            public async Task<IActionResult> DeleteProfessor(int professorId)
            {
                await _professorService.DeleteProfessorByIdAsync(professorId);
                return Ok();
            }
        }
    
}
