using SchoolManagement.Application.ApplicationServices.Maps_Dto.SubjectAuxMean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface ISubjectAuxMeanService
    {
        Task<SubjectAuxMeanResponseDto> CreateSubjectAuxMeanAsync(SubjectAuxMeanDto subjectAuxMeanDto);
        Task<SubjectAuxMeanResponseDto> UpdateSubjectAuxMeanAsync(SubjectAuxMeanResponseDto subjectAuxMeanDto);
        Task<IEnumerable<SubjectAuxMeanResponseDto>> ListSubjectAuxMeansAsync();
        Task<SubjectAuxMeanResponseDto> DeleteSubjectAuxMeanByIdAsync(int id);
    }

}
