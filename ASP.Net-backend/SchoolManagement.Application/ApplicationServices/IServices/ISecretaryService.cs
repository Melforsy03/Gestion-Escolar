using SchoolManagement.Application.ApplicationServices.Maps_Dto;
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
        Task<SecretaryDto> CreateSecretaryAsync(SecretaryDto secretaryDto);
        Task<SecretaryDto> UpdateSecretaryAsync(SecretaryDto secretaryDto);
        Task<IEnumerable<SecretaryDto>> ListSecretariesAsync();
        Task<SecretaryDto> DeleteSecretaryByIdAsync(int secretaryId);
    }
}
