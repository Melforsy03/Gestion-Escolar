using SchoolManagement.Application.ApplicationServices.Maps_Dto.TechnologicalMeans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface ITechnologicalMeansService
    {
        Task<TechnologicalMeansResponseDto> CreateTechnologicalMeansAsync(TechnologicalMeansDto technologicalMeansDto);
        Task<TechnologicalMeansResponseDto> UpdateTechnologicalMeansAsync(TechnologicalMeansResponseDto technologicalMeansDto);
        Task<IEnumerable<TechnologicalMeansResponseDto>> ListTechnologicalMeansAsync();
        Task <TechnologicalMeansResponseDto> DeleteTechnologicalMeansByIdAsync(int technologicalMeansDto);
    }
}
