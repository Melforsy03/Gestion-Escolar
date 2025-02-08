using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IProfessorSubjectService
    {
        Task<ProfessorSubjectResponseDto> CreateProfessorSubjectAsync(ProfessorSubjectDto professorDto);
        Task<IEnumerable<ProfessorSubjectResponseDto>> ListProfessorSubjectAsync();
        Task<ProfessorSubjectResponseDto> DeleteProfessorSubjectByIdAsync(int professorSubjectDto);
    }
}
