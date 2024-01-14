namespace CRUD.Application.Clients.CreateClient
{
    public sealed class CreateClientValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull().WithMessage("Name cannot be null or whitespace")
                .MaximumLength(100).WithMessage("Name cannot be greater than {MaxLength} characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull().WithMessage("Email cannot be null or whitespace")
                .MaximumLength(100).WithMessage("Email cannot be greater than {MaxLength} characters");

            RuleFor(x => x.Profession)
                .NotEmpty()
                .NotNull().WithMessage("Profession cannot be null or whitespace")
                .MaximumLength(100).WithMessage("Profession cannot be greater than {MaxLength} characters");
        }
    }
}
