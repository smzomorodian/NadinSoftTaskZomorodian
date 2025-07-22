using Application.NadinSoft.Command.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.NadinSoft.Validators
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("نام نمی‌تواند خالی باشد")
                .MaximumLength(100);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("شماره موبایل الزامی است")
                .Matches(@"^09\d{9}$").WithMessage("شماره موبایل معتبر نیست");

            RuleFor(x => x.Nationalcode)
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
