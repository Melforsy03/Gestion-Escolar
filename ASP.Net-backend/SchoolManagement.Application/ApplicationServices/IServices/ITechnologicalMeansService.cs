using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface ITechnologicalMeansService
    {
        Task<TechnologicalMeansDto> CreateTechnologicalMeansAsync(TechnologicalMeansDto technologicalMeansDto);
        Task<TechnologicalMeansDto> UpdateTechnologicalMeansAsync(TechnologicalMeansDto technologicalMeansDto);
        Task<IEnumerable<TechnologicalMeansDto>> ListTechnologicalMeansAsync();
        Task <TechnologicalMeansDto> DeleteTechnologicalMeansByIdAsync(int technologicalMeansDto);
    }
}
