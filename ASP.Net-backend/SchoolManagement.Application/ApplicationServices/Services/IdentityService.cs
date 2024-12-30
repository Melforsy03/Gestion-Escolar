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

        private readonly IProfessorService _professorService;
        private readonly IIdentityManager _identityManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;
        public IdentityService(IJwtTokenGenerator jwtTokenGenerator, IIdentityManager identityManager, IMapper mapper, IProfessorService professorService)
        {
            _professorService = professorService;
            _identityManager = identityManager;
            _mapper = mapper;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<(bool, string)> CheckCredentialsAsync(LoginDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            if (user == null) return (false, "");
            var savedUser = await _identityManager.CheckCredentialsAsync(user.UserName!, userDto.Password);
            if (savedUser)
            {
                var userId = _identityManager.ListUsersAsync().FirstOrDefault(x => x.UserName == user.UserName)!.Id;
                user.Id = userId;
                var adminRole = await _identityManager.IsInRoleAsync(userId, Role.Admin);
                var superAdminRole = await _identityManager.IsInRoleAsync(userId, Role.SuperAdmin);
                var professorRole = await _identityManager.IsInRoleAsync(userId, Role.Professor);

                if (adminRole)
                {
                    var token = _jwtTokenGenerator.GenerateToken(user, Role.Admin);
                    return (savedUser, token);
                }
                else if (superAdminRole)
                {
                    var token = _jwtTokenGenerator.GenerateToken(user, Role.SuperAdmin);
                    return (savedUser, token);
                }
                else
                {
                    var token = _jwtTokenGenerator.GenerateToken(user, Role.Professor);
                    return (savedUser, token);
                }

            }
            return (false, "Your credentials are incorrect");
        }

        public async Task<(string,string)> CreateUserAsync(RegisterDto userDto)
        {
           
            var user = _mapper.Map<User>(userDto);
            var savedUser = await _identityManager.CreateUserAsync(user, userDto.Password);
            await _identityManager.AddRoles(savedUser.Id, userDto.role);
            //await _professorService.CreateprofessorAsync(savedUser);                          el profe es un User
            var token = _jwtTokenGenerator.GenerateToken(savedUser, userDto.role);
            return (token, savedUser.Id.ToString());
        }
    }
}
