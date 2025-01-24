using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IAdministratorService
    {
        Task<AdministratorDto> CreateAdministratorAsync(AdministratorDto administratorDto);
        Task<AdministratorDto> UpdateAdministratorAsync(AdministratorDto administratorDto);
        Task<IEnumerable<AdministratorDto>> ListAdministratorAsync();
        Task<AdministratorDto> DeleteAdministratorByIdAsync(int administratorId);
    }

}
