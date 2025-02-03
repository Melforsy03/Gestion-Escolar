using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Secretary;
using SchoolManagement.Application.Common;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.DataAccess.Repository;
using SchoolManagement.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class SecretaryService : ISecretaryService
    {
        private readonly ISecretaryRepository _secretaryRepository;
        private readonly Triggers _trigger;
        private readonly IMapper _mapper;

        public SecretaryService(ISecretaryRepository secretaryRepository, IMapper mapper, Triggers trigger)
        {
            _secretaryRepository = secretaryRepository;
            _mapper = mapper;
            _trigger = trigger;
        }

        public async Task<SecretaryCreateResponseDto> CreateSecretaryAsync(SecretaryDto secretaryDto)
        {
            // Mapea el DTO de secretaria a la entidad de dominio Secretary
            var secretary = _mapper.Map<Secretary>(secretaryDto);

            // Registra al usuario asociado a la secretaria y obtiene su información
            (User, string) User = await _trigger.RegisterUser(secretaryDto.NameS, "Secretary");

            // Asigna el ID del usuario al objeto secretaria
            secretary.UserId = User.Item1.Id;

            // Crea la nueva secretaria en la base de datos
            var savedSecretary = await _secretaryRepository.CreateAsync(secretary);

            // Crea un DTO de respuesta para la nueva secretaria creada
            SecretaryCreateResponseDto answer = new SecretaryCreateResponseDto();
            answer.Id = secretary.IdS; // Asigna el ID de la secretaria a la respuesta
            answer.secretary = _mapper.Map<SecretaryDto>(savedSecretary); // Mapea y asigna el DTO de la secretaria guardada
            answer.UserName = User.Item1.UserName; // Asigna el nombre de usuario a la respuesta
            answer.Password = User.Item2; // Asigna la contraseña a la respuesta

            return answer; // Retorna el DTO con los detalles de la nueva secretaria creada
        }

        public async Task<SecretaryResponseDto> DeleteSecretaryByIdAsync(int secretaryId)
        {
            // Obtiene la secretaria por su ID
            var secretary = _secretaryRepository.GetById(secretaryId);

            // Verifica si la secretaria ya está marcada como eliminada
            if (secretary.IsDeleted)
            {
                return null; // Retorna null si ya está eliminada
            }

            // Marca la secretaria como eliminada
            secretary.IsDeleted = true;

            // Actualiza el estado en la base de datos
            await _secretaryRepository.UpdateAsync(secretary);

            // Crea un DTO de respuesta para la secretaria eliminada
            SecretaryResponseDto answer = new SecretaryResponseDto();
            answer.Id = secretary.IdS; // Asigna el ID de la secretaria a la respuesta
            answer.secretary = _mapper.Map<SecretaryDto>(secretary); // Mapea y asigna el DTO de la secretaria

            return answer; // Retorna el DTO con los detalles de la secretaria eliminada
        }

        public async Task<IEnumerable<SecretaryResponseDto>> ListSecretariesAsync()
        {
            // Obtiene todas las secretarias desde el repositorio
            var secretaries = await _secretaryRepository.ListAsync();

            var list = secretaries.ToList(); // Convierte a lista para su manipulación
            List<SecretaryResponseDto> Secretaries_List = new List<SecretaryResponseDto>(); // Inicializa una lista para almacenar los DTOs

            // Itera sobre cada secretaria y agrega solo las no eliminadas a la lista de resultados
            for (int i = 0; i < secretaries.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {
                    SecretaryResponseDto answer = new SecretaryResponseDto();
                    answer.Id = list[i].IdS; // Asigna el ID de la secretaria a la respuesta

                    answer.secretary = _mapper.Map<SecretaryDto>(list[i]); // Mapea y asigna el DTO de la secretaria

                    Secretaries_List.Add(answer); // Agrega a la lista de resultados
                }
            }

            return Secretaries_List; // Retorna la lista de DTOs de secretarias no eliminadas
        }

        public async Task<SecretaryResponseDto> UpdateSecretaryAsync(SecretaryResponseDto secretaryInfo)
        {
            // Obtiene la secretaria por su ID desde el DTO
            var secretary = _secretaryRepository.GetById(secretaryInfo.Id);

            // Verifica si la secretaria está marcada como eliminada
            if (secretary.IsDeleted)
            {
                return null; // Retorna null si está eliminada
            }

            // Mapea los cambios desde el DTO a la entidad existente
            _mapper.Map(secretaryInfo.secretary, secretary);

            // Actualiza la entidad en la base de datos
            await _secretaryRepository.UpdateAsync(secretary);

            // Crea un DTO de respuesta para la secretaria actualizada
            SecretaryResponseDto answer = new SecretaryResponseDto();
            answer.Id = secretary.IdS; // Asigna el ID de la secretaria a la respuesta

            answer.secretary = _mapper.Map<SecretaryDto>(secretary); // Mapea y asigna el DTO actualizado

            return answer; // Retorna el DTO con los detalles actualizados de la secretaria
        }



    }
}
