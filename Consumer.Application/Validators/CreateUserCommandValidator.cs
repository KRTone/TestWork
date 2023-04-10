using Consumer.Application.Commands;
using FluentValidation;

namespace Consumer.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .WithMessage("Name is required");
            RuleFor(u => u.LastName)
                .NotEmpty()
                .WithMessage("LastName is required");
            RuleFor(u => u.PhoneNumber)
                .NotEmpty()
                .WithMessage("PhoneNumber is required");
            //    .Matches(phineregex);
            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Email is required");
            //    .Matches(emailregex);
        }
    }
}