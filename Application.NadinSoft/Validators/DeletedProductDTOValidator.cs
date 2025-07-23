using Application.NadinSoft.DTO.ProductDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.NadinSoft.Validators
{
    public class DeletedProductDTOValidator : AbstractValidator<DeletedProductDTO>
    {
        public DeletedProductDTOValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("شناسه محصول نباید خالی باشد.")
                .Must(id => id != Guid.Empty).WithMessage("شناسه محصول معتبر نیست.");
        }
    }
}

