﻿using FluentValidation;
using WebApplication1.Models.ControllersIn.Ingredient;

namespace WebApplication1.Validators.Ingredient
{
    public class IngredientSearchModelValidator : AbstractValidator<IngredientSearchModel>
    {
        public IngredientSearchModelValidator()
        {
            RuleFor(x => x.Query)
                .NotEmpty().WithMessage("Вкажіть запит")
                .MinimumLength(3).WithMessage("Запит має бути не коротше 3 символів")
                .MaximumLength(50).WithMessage("Запит має бути не довше 50 символів")
                .Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9'\\s-]+$").WithMessage("Назва може містити лише цифри та букви");
        }
    }
}
