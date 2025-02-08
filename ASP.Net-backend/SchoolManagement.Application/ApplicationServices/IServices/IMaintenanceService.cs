using SchoolManagement.Application.ApplicationServices.Maps_Dto.Maintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IMaintenanceService
    {
        Task<MaintenanceResponseDto> CreateMaintenanceAsync(MaintenanceDto maintenanceDto);
        Task<MaintenanceResponseDto> UpdateMaintenanceAsync(MaintenanceResponseDto maintenanceDto);
        Task<IEnumerable<MaintenanceResponseDto>> ListMaintenancesAsync();
        Task<MaintenanceResponseDto> DeleteMaintenanceByIdAsync(int maintenanceDto);
    }
}
