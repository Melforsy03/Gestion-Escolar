using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IAuxiliaryMeansService
    {
        Task<AuxiliaryMeansDto> CreateAuxiliaryMeansAsync(AuxiliaryMeansDto auxiliaryMeansDto);
        Task<AuxiliaryMeansDto> UpdateAuxiliaryMeansAsync(AuxiliaryMeansDto auxiliaryMeansDto);
        Task<IEnumerable<AuxiliaryMeansDto>> ListAuxiliaryMeansAsync();
        Task<AuxiliaryMeansDto> DeleteAuxiliaryMeansByIdAsync(int auxiliaryMeansDto);
    }
}
