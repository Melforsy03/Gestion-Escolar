using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface ISubjectAuxMeanService
    {
        Task<SubjectAuxMeanDto> CreateSubjectAuxMeanAsync(SubjectAuxMeanDto subjectAuxMeanDto);
        Task<SubjectAuxMeanDto> UpdateSubjectAuxMeanAsync(SubjectAuxMeanDto subjectAuxMeanDto);
        Task<IEnumerable<SubjectAuxMeanDto>> ListSubjectAuxMeansAsync();
        Task DeleteSubjectAuxMeanByIdAsync(int id);
    }

}
