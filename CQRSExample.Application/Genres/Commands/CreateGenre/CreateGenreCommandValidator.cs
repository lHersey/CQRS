using FluentValidation;

namespace CQRSExample.Application.Genres.Commands.CreateGenre
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(p => p.Description).MaximumLength(15).NotEmpty();
        }
    }
}