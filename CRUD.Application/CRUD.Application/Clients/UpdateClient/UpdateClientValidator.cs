namespace CRUD.Application.Clients.UpdateClient
{
    public sealed class UpdateClientValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email cannot be null or whitespace");

            RuleFor(x => x.Profession)
                .NotEmpty()
                .NotNull()
                .WithMessage("Profession cannot be null or whitespace");
        }
    }
}
