using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ResponseDto;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ResponseDto.Professor;
using SchoolManagement.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IProfessorService
    {
        Task<ProfessorCreateResponseDto> CreateProfessorAsync(ProfessorDto professorDto);
        Task<ProfessorResponseDto> UpdateProfessorAsync(ProfessorResponseDto professorInfo);
        Task<IEnumerable<ProfessorResponseDto>> ListProfessorAsync();
        Task<ProfessorResponseDto> DeleteProfessorByIdAsync(int professorDto);
    }
}
