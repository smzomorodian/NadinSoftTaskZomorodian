using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.NadinSoft.DTO.UserDTO
{
    public class LoginUserDTO
    {
        public string Password { get; set; }
        public string NationalCode { get; set; }
    }

    public sealed record LoginUserParameter
        (
            string Password,
            string NationalCode
        );
}
