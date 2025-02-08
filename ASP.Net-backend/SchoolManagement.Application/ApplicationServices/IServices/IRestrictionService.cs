using SchoolManagement.Application.ApplicationServices.Maps_Dto.Restriction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IRestrictionService
    {
        Task<RestrictionResponseDto> CreateRestrictionAsync(RestrictionDto restrictionDto);
        Task<RestrictionResponseDto> UpdateRestrictionAsync(RestrictionResponseDto restrictionDto);
        Task<IEnumerable<RestrictionResponseDto>> ListRestrictionAsync();
        Task<RestrictionResponseDto> DeleteRestrictionByIdAsync(int restrictionDto);
    }
}
