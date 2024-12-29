﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser(RegisterDto registerDto)
        {

            var result = await _identityService.CreateUserAsync(registerDto);
            //return Ok(result.Item1);
            return Ok(new
            {
                userId = result.Item2,
                token = result.Item1
            });
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LogUser(LogingDto loginDto)
        {
            var user = await _identityService.CheckCredentialsAsync(loginDto);

            return Ok(new
            {
                verificstion = user.Item1,
                token = user.Item2
            });
        }
    }
}
