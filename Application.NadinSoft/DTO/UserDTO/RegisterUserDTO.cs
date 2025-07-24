using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.NadinSoft.DTO.UserDTO
{
    public class RegisterUserDTO
    {
        public string Nationalcode { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }


    public sealed record RegisterUserParameter(
        string Nationalcode,
        string UserName,
        string Email,
        string Password,
        string PhoneNumber
        );
}
