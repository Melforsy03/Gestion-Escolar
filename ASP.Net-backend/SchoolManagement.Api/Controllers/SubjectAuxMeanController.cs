using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("subjectAuxMean")]
    public class SubjectAuxMeanController : ControllerBase
    {
        private readonly ISubjectAuxMeanService _subjectAuxMeanService;

        public SubjectAuxMeanController(ISubjectAuxMeanService subjectAuxMeanService)
        {
            _subjectAuxMeanService = subjectAuxMeanService;
        }

        [HttpPost]
        [Route("create")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateSubjectAuxMean(SubjectAuxMeanDto subjectAuxMean)
        {
            var createdSubjectAuxMean = await _subjectAuxMeanService.CreateSubjectAuxMeanAsync(subjectAuxMean);
            return Ok(createdSubjectAuxMean);
        }

        [HttpGet]
        [Route("list")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<SubjectAuxMeanDto>>> ListSubjectAuxMeans()
        {
            var subjectAuxMeans = await _subjectAuxMeanService.ListSubjectAuxMeansAsync();
            return Ok(subjectAuxMeans);
        }

        [HttpPut]
        [Route("update")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> UpdateSubjectAuxMean(SubjectAuxMeanDto subjectAuxMean)
        {
            var updatedSubjectAuxMean = await _subjectAuxMeanService.UpdateSubjectAuxMeanAsync(subjectAuxMean);
            return Ok(updatedSubjectAuxMean);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteSubjectAuxMean(int id)
        {
            await _subjectAuxMeanService.DeleteSubjectAuxMeanByIdAsync(id);
            return Ok();
        }
    }

}
