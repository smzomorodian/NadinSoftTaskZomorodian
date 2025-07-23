using Application.NadinSoft.DTO.UserDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.NadinSoft.Validators
{
    public class LoginUserDTOValidation : AbstractValidator<LoginUserDTO>
    {
        public LoginUserDTOValidation()
        {
            RuleFor(x => x.NationalCode)
               .NotEmpty().WithMessage("کد ملی الزامی است")
               .Length(10).WithMessage("کد ملی باید دقیقاً ۱۰ رقم باشد")
               .Matches(@"^\d{10}$").WithMessage("کد ملی باید فقط شامل عدد باشد");

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("رمز عبور الزامی است")
               .MinimumLength(6).WithMessage("رمز عبور باید حداقل ۶ کاراکتر باشد")
               .MaximumLength(100);

        }
    }
}
