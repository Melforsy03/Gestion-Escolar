using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IMaintenanceService
    {
        Task<MaintenanceDto> CreateMaintenanceAsync(MaintenanceDto maintenanceDto);
        Task<MaintenanceDto> UpdateMaintenanceAsync(MaintenanceDto maintenanceDto);
        Task<IEnumerable<MaintenanceDto>> ListMaintenancesAsync();
        Task DeleteMaintenanceByIdAsync(int maintenanceDto);
    }
}
