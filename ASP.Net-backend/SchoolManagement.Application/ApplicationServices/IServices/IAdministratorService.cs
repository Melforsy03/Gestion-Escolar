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
        Task<(int Id, AdministratorDto administrator, string UserName, string Password)> CreateAdministratorAsync(AdministratorDto administratorDto);
        Task<(int Id, AdministratorDto administrator)> UpdateAdministratorAsync((int Id, AdministratorDto administratorDto) administratorInfo);
        Task<IEnumerable<(int Id, AdministratorDto administrator)>> ListAdministratorAsync();
        Task<(int Id, AdministratorDto administrator)> DeleteAdministratorByIdAsync(int administratorId);
    }

}
