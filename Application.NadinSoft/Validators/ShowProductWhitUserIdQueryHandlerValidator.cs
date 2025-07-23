using Application.NadinSoft.Query;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.NadinSoft.Validators
{
    public class ShowProductWhitUserIdQueryHandlerValidator : AbstractValidator<ShowProductWhitUserIdQuery>
    {
        public ShowProductWhitUserIdQueryHandlerValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Id نمی‌تواند خالی باشد.")
                .MaximumLength(150).WithMessage("Id نباید بیش از 150 کاراکتر باشد.")
                .Must(id => !int.TryParse(id, out _)).WithMessage("Id نباید فرمت عددی داشته باشد.");
        }
    }
}
