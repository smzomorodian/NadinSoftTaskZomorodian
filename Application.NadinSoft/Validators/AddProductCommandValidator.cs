using Application.NadinSoft.Command.ProductCommand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.NadinSoft.Validators
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("نام نمی تواند خالی باشد.")
                .MaximumLength(200);

            RuleFor(x => x.ManufacturePhone)
                .NotEmpty().WithMessage("شماره موبایل الزامی است.")
                .Matches(@"^09\d{9}$").WithMessage("شماره موبایل معتبر نیست");

            RuleFor(x => x.ManufactureEmail)
                .NotEmpty().WithMessage("ایمیل نمی تواند خالی باشد.")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage("فرمت ایمیل نامعتبر است.");

            RuleFor(x => x.ProduceDate)
                .NotEmpty().WithMessage("تاریخ تولید الزامی است.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("تاریخ تولید نمی‌تواند در آینده باشد.");
        }
    }
}
