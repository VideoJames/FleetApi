using FluentValidation;
using WorkflowApi.DTOs;

namespace WorkflowApi.Validators
{
    public class UserUpdateDtoValidator :AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.RowVersion)
                .NotNull();
        }
    }
}
