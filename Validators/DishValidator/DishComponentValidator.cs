using FluentValidation;
using WebApplication1.Models.ControllersIn.Dish;

namespace WebApplication1.Validators.DishValidator
{
    public class DishComponentValidator : AbstractValidator<DishComponent>
    {
        public DishComponentValidator()
        {
            RuleFor(x => x.ID)
                .NotEmpty().WithMessage("Вкажіть ідентифікатор");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Вкажіть кількість")
                .LessThan(10000).WithMessage("К-сть не може бути більше 10000");
        }
    }
}
