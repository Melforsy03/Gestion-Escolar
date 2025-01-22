﻿using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IProfessorSubjectService
    {
        Task<ProfessorSubjectDto> CreateProfessorSubjectAsync(ProfessorSubjectDto professorDto);
        Task<IEnumerable<ProfessorSubjectDto>> ListProfessorSubjectAsync();
        Task DeleteProfessorSubjectByIdAsync(int professorSubjectDto);
    }
}
