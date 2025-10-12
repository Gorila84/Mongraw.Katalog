using FluentValidation;
using Mongraw.Katalog.Application.Dto.ItemDtos;
using System.Data;

namespace Mongraw.Katalog.Application.Validations
{
    public class EditItemDtoValidators : AbstractValidator<EditItemDto>
    {
        public EditItemDtoValidators()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Nazwa produktu nie może przekraczać 100 znaków.");
            RuleFor(x => x.Quantity)
                .MaximumLength(50)
                .WithMessage("Ilość nie może przekraczać 50 znaków.");
            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .WithMessage("Opis produktu nie może przekraczać 1000 znaków.");
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithName("Cena")
                .WithMessage("Cena musi być większa od zera.");
            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithName("Kategoria")
                .WithMessage("To pole jest wymagane.");
            RuleFor(x => x.SubcategoryId)
                .GreaterThan(0)
                .WithName("Podkategoria")
                .WithMessage("To pole jest wymagane.");
            RuleFor(x => x.WholesalerName)
                .MaximumLength(100)
                .WithMessage("Nazwa hurtownika nie może przekraczać 100 znaków.");
        }
    }
}
