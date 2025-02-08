using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.Common;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.Identity;
using SchoolManagement.Infrastructure.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Administrator;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    // Esta clase implementa la interfaz IAdministratorService y proporciona servicios relacionados con la gestión de administradores.
    public class AdministratorService : IAdministratorService
    {
        private readonly IAdministratorRepository _administratorRepository; // Repositorio para operaciones CRUD de administradores.
        private readonly Triggers _trigger; // Clase para manejar eventos específicos, como registro de usuarios.
        private readonly IMapper _mapper; // Herramienta para mapear entre entidades y DTOs.

        // Constructor que inyecta dependencias necesarias.
        public AdministratorService(IAdministratorRepository administratorRepository, IMapper mapper, Triggers trigger)
        {
            _administratorRepository = administratorRepository;
            _mapper = mapper;
            _trigger = trigger;
        }

        // Método para crear un nuevo administrador.
        public async Task<AdministratorCreateResponseDto> CreateAdministratorAsync(AdministratorDto administratorDto)
        {
            // Mapea el DTO recibido a una entidad de administrador.
            var administrator = _mapper.Map<Administrator>(administratorDto);
            if (!_trigger.CheckName(administratorDto.AdminName)) return null;

            // Registra un usuario asociado al administrador y obtiene el usuario creado y su contraseña.
            (User, string) User = await _trigger.RegisterUser(administratorDto.AdminName, "Admin");

            // Asigna el ID del usuario creado al administrador.
            administrator.UserId = User.Item1.Id;

            // Guarda el administrador en la base de datos.
            var savedAdministrator = await _administratorRepository.CreateAsync(administrator);

            // Prepara la respuesta con los datos del administrador creado.
            AdministratorCreateResponseDto answer = new AdministratorCreateResponseDto();
            answer.Id = administrator.AdminId; // ID del administrador.
            answer.Administrator = _mapper.Map<AdministratorDto>(savedAdministrator); // Mapea la entidad guardada a un DTO.
            answer.UserName = User.Item1.UserName; // Nombre de usuario creado.
            answer.Password = User.Item2; // Contraseña generada.

            return answer; // Devuelve la respuesta.
        }

        // Método para eliminar (soft delete) un administrador por su ID.
        public async Task<AdministratorResponseDto> DeleteAdministratorByIdAsync(int administratorId)
        {
            var administrator = _administratorRepository.GetById(administratorId); // Obtiene el administrador por su ID.

            if (administrator.IsDeleted) // Si ya está eliminado, devuelve null.
            {
                return null;
            }

            administrator.IsDeleted = true; // Marca al administrador como eliminado (soft delete).
            await _administratorRepository.UpdateAsync(administrator); // Actualiza el estado en la base de datos.

            // Prepara la respuesta con los datos del administrador eliminado.
            AdministratorResponseDto answer = new AdministratorResponseDto();
            answer.Id = administrator.AdminId;
            answer.Administrator = _mapper.Map<AdministratorDto>(administrator);

            return answer;
        }

        // Método para listar todos los administradores no eliminados.
        public async Task<IEnumerable<AdministratorResponseDto>> ListAdministratorsAsync()
        {
            var administrators = await _administratorRepository.ListAsync(); // Obtiene todos los administradores.
            var list = administrators.ToList();

            List<AdministratorResponseDto> Administrators_List = new List<AdministratorResponseDto>();

            for (int i = 0; i < administrators.Count(); i++)
            {
                if (!list[i].IsDeleted) // Filtra los administradores que no están eliminados.
                {
                    AdministratorResponseDto answer = new AdministratorResponseDto();
                    answer.Id = list[i].AdminId;
                    answer.Administrator = _mapper.Map<AdministratorDto>(list[i]);
                    Administrators_List.Add(answer);
                }
            }

            return Administrators_List;
        }

        // Método para actualizar un administrador existente.
        public async Task<AdministratorResponseDto> UpdateAdministratorAsync(AdministratorResponseDto administratorInfo)
        {
            if (!_trigger.CheckName(administratorInfo.Administrator.AdminName)) return null;
            var administrator = _administratorRepository.GetById(administratorInfo.Id);

            if (administrator.IsDeleted) // Si el administrador está eliminado, devuelve null.
            {
                return null;
            }

            _mapper.Map(administratorInfo.Administrator, administrator); // Mapea los cambios desde el DTO hacia la entidad existente.

            await _administratorRepository.UpdateAsync(administrator);

            // Prepara la respuesta con los datos del administrador actualizado.
            AdministratorResponseDto answer = new AdministratorResponseDto();
            answer.Id = administrator.AdminId;
            answer.Administrator = _mapper.Map<AdministratorDto>(administrator);

            return answer;
        }
    }
}
