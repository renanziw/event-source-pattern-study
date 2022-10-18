using RUSH.App.Domain.Model;
using FluentValidation;

namespace RUSH.App.API.Infrastructure.Validators
{
    public class ToDoValidator : AbstractValidator<ToDo>
    {
        public ToDoValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x.Status).NotNull().NotEmpty();
        }
    }
}
