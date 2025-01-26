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
        Task<(int Id, SecretaryDto secretary, string UserName, string Password)> CreateSecretaryAsync(SecretaryDto secretaryDto);
        Task<(int Id, SecretaryDto secretary)> UpdateSecretaryAsync((int Id, SecretaryDto secretaryDto) professorInfo);
        Task<IEnumerable<(int Id, SecretaryDto secretary)>> ListSecretariesAsync();
        Task<(int Id, SecretaryDto secretary)> DeleteSecretaryByIdAsync(int secretaryId);
    }
}
