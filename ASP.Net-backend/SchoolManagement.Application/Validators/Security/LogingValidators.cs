using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Application.Validators.Security;

public class LoginValidators
{
    [Required]
    public string UserName {get; set;}

    [Required]
    public string Password {get; set;}
}