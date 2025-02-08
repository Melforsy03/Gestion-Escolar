using SchoolManagement.Application.ApplicationServices.Maps_Dto.AuxiliaryMeans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IAuxiliaryMeansService
    {
        Task<AuxiliaryMeansResponseDto> CreateAuxiliaryMeansAsync(AuxiliaryMeansDto auxiliaryMeansDto);
        Task<AuxiliaryMeansResponseDto> UpdateAuxiliaryMeansAsync(AuxiliaryMeansResponseDto auxiliaryMeansDto);
        Task<IEnumerable<AuxiliaryMeansResponseDto>> ListAuxiliaryMeansAsync();
        Task<AuxiliaryMeansResponseDto> DeleteAuxiliaryMeansByIdAsync(int auxiliaryMeansDto);
    }
}
