using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IProfessorService
    {
        Task<ProfessorDto> CreateProfessorAsync(ProfessorDto professorDto);
        Task<ProfessorDto> UpdateProfessorAsync(ProfessorDto professorDto);
        Task<IEnumerable<ProfessorDto>> ListProfessorAsync();
        Task DeleteProfessorByIdAsync(int professorDto);
    }
}
