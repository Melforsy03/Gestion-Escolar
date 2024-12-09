using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Application.ApplicationServices.Services;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers
{
   
        [ApiController]
        [Route("professor")]
        public class ProfessorController : ControllerBase
        {
            private readonly IProfessorService _professorService;
            public ProfessorController(ProfessorService professorService)
            {
                _professorService = professorService;
            }

            [HttpPost]
            [Route("create")]
            //[Authorize(Roles = "SuperAdmin")]
            public async Task<IActionResult> Createprofessor(ProfessorDto professor)
            {
                var professor2 = await _professorService.CreateProfessorAsync(professor);
                return Ok(professor2);
            }

            [HttpGet]
            [Route("list")]
            //[Authorize (Roles = "SuperAdmin")]
            public async Task<ActionResult<IEnumerable<Professor>>> Listprofessor()
            {
                var professor = await _professorService.ListProfessorAsync();
                return Ok(professor);
            }

            [HttpPut]
            [Route("update")]
            public async Task<ActionResult> Updateprofessor(ProfessorDto professor)
            {
                var _professor = await _professorService.UpdateProfessorAsync(professor);
                return Ok(_professor);
            }

            [HttpDelete]
            [Route("delete")]
            public async Task<IActionResult> Deleteprofessor(int professorId)
            {
                await _professorService.DeleteProfessorByIdAsync(professorId);
                return Ok();
            }
        }
    
}
