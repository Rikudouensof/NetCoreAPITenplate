using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Domain.Models.ApiModels
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get;  set; } = "";

        [Required]
        [MinLength(8)]
        [Display(Name = "Password")]
        public string Password { get;  set; } = "";


        public LoginModel(string username, string password)
        {
            UserName = username;
            Password = password;
        }
    }
}
