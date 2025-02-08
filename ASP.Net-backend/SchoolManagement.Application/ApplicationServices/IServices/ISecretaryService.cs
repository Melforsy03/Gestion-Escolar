using SchoolManagement.Application.ApplicationServices.Maps_Dto.Secretary;
using SchoolManagement.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface ISecretaryService
    {
        Task<SecretaryCreateResponseDto> CreateSecretaryAsync(SecretaryDto secretaryDto);
        Task<SecretaryResponseDto> UpdateSecretaryAsync(SecretaryResponseDto professorInfo);
        Task<IEnumerable<SecretaryResponseDto>> ListSecretariesAsync();
        Task<SecretaryResponseDto> DeleteSecretaryByIdAsync(int secretaryId);
    }
}
