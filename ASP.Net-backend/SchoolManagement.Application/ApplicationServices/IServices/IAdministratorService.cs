using SchoolManagement.Application.ApplicationServices.Maps_Dto.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IAdministratorService
    {
        Task<AdministratorCreateResponseDto> CreateAdministratorAsync(AdministratorDto administratorDto);
        Task<AdministratorResponseDto> UpdateAdministratorAsync(AdministratorResponseDto administratorInfo);
        Task<IEnumerable<AdministratorResponseDto>> ListAdministratorsAsync();
        Task<AdministratorResponseDto> DeleteAdministratorByIdAsync(int administratorId);
    }


}
