using FluentValidation;
using Mongraw.Katalog.Application.Dto.ItemDtos;

namespace Mongraw.Katalog.Application.Validations
{
    public class AddItemDtoValidator : AbstractValidator<AddItemDto>
    {
        public AddItemDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithName("Nazwa produktu")
                .WithMessage("To pole jest wymagane.")
                .MaximumLength(100)
                .WithMessage("Nazwa produktu nie może przekraczać 100 znaków.");
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithName("Ilość")
                .WithMessage("To pole jest wymagane.")
                .MaximumLength(50)
                .WithMessage("Ilość nie może przekraczać 50 znaków.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithName("Opis produktu")
                .WithMessage("To pole jest wymagane.")
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
                .NotEmpty()
                .WithName("Nazwa hurtownika")
                .WithMessage("To pole jest wymagane.")
                .MaximumLength(100)
                .WithMessage("Nazwa hurtownika nie może przekraczać 100 znaków.");
        }
    }
}
