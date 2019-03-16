using FluentValidation;

namespace CQRSExample.Application.Movies.Commands.CreateMovie
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(x => x.DailyRentalRate).GreaterThan(0).NotEmpty();
            RuleFor(x => x.NumberInStock).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Title).MaximumLength(15).MinimumLength(2);
            RuleFor(x => x.GenreId).NotEmpty();
        }
    }
}