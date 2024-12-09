using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IRestrictionService
    {
        Task<RestrictionDto> CreateRestrictionAsync(RestrictionDto restrictionDto);
        Task<RestrictionDto> UpdateRestrictionAsync(RestrictionDto restrictionDto);
        Task<IEnumerable<RestrictionDto>> ListRestrictionAsync();
        Task DeleteRestrictionByIdAsync(int restrictionDto);
    }
}
