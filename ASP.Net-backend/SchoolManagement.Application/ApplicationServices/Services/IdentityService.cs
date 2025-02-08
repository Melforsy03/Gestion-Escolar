using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Role;
using SchoolManagement.Infrastructure.Identity;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class IdentityService : IIdentityService
    {
        //private readonly IProfessorService _professorService;
        private readonly IIdentityManager _identityManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;
        public IdentityService(IJwtTokenGenerator jwtTokenGenerator, IIdentityManager identityManager, IMapper mapper)
        {
            // _professorService = professorService;
            _identityManager = identityManager;
            _mapper = mapper;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<(bool, string, string)> CheckCredentialsAsync(LoginDto userDto)
        {
            // Mapea el DTO de inicio de sesión a la entidad User
            var user = _mapper.Map<User>(userDto);
            string role = string.Empty; // Inicializa una variable para almacenar el rol del usuario

            // Verifica si el usuario es nulo
            if (user == null) return (false, "", ""); // Retorna false si el mapeo falla

            // Verifica las credenciales del usuario
            var savedUser = await _identityManager.CheckCredentialsAsync(user.UserName!, userDto.Password);

            // Si las credenciales son correctas
            if (savedUser)
            {
                // Obtiene el ID del usuario a partir de su nombre de usuario
                var userId = _identityManager.ListUsersAsync().FirstOrDefault(x => x.UserName == user.UserName)!.Id;
                user.Id = userId; // Asigna el ID al objeto user

                // Verifica los roles del usuario
                var adminRole = await _identityManager.IsInRoleAsync(userId, Role.Admin);
                var superAdmin = await _identityManager.IsInRoleAsync(userId, Role.SuperAdmin);
                var secretaryRole = await _identityManager.IsInRoleAsync(userId, Role.Secretary);
                var professorRole = await _identityManager.IsInRoleAsync(userId, Role.Professor);
                var studentRole = await _identityManager.IsInRoleAsync(userId, Role.Student);

                // Genera un token basado en el rol del usuario y lo retorna
                if (adminRole)
                {
                    var token = _jwtTokenGenerator.GenerateToken(user, Role.Admin);
                    return (savedUser, token, Role.Admin);
                }
                else if (superAdmin)
                {
                    var token = _jwtTokenGenerator.GenerateToken(user, Role.SuperAdmin);
                    return (savedUser, token, Role.SuperAdmin);
                }
                else if (secretaryRole)
                {
                    var token = _jwtTokenGenerator.GenerateToken(user, Role.Secretary);
                    return (savedUser, token, Role.Secretary);
                }
                else if (professorRole)
                {
                    var token = _jwtTokenGenerator.GenerateToken(user, Role.Professor);
                    return (savedUser, token, Role.Professor);
                }
                else
                {
                    var token = _jwtTokenGenerator.GenerateToken(user, Role.Student);
                    return (savedUser, token, Role.Student);
                }
            }

            // Retorna un mensaje de error si las credenciales son incorrectas
            return (false, "Your credentials are incorrect", "");
        }

        public async Task<(string, string)> CreateUserAsync(RegisterDto userDto)
        {
            // Mapea el DTO de registro a la entidad User
            var user = _mapper.Map<User>(userDto);

            // Crea un nuevo usuario en la base de datos
            var savedUser = await _identityManager.CreateUserAsync(user, userDto.Password);

            // Asigna roles al nuevo usuario
            await _identityManager.AddRoles(savedUser.Id, userDto.role);

            // Genera un token para el nuevo usuario basado en su rol
            var token = _jwtTokenGenerator.GenerateToken(savedUser, userDto.role);

            // Retorna el token y el ID del nuevo usuario como cadena
            return (token, savedUser.Id.ToString());
        }

        public Task<IEnumerable<User>> ListUsersAsync()
        {
            // Obtiene la lista de usuarios desde el administrador de identidad
            var users = _identityManager.ListUsersAsync();

            // Retorna la lista de usuarios como una tarea
            return (Task<IEnumerable<User>>)users;
        }

    }
}
