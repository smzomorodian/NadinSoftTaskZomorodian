using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.NadinSoft.Command
{
    public class LoginUserCommand : IRequest<string>
    {
        public string Password { get; set; }
        public string NationalCode { get; set; }
    }
}
