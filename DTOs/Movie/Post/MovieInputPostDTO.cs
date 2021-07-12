using System.Collections.Generic;
using FluentValidation;
public class MovieInputPostDTO{

    public string Titulo { get; set; }

    public string Ano { get; set; }

    public string Genero { get; set; }

    public long DiretorId { get; set; }
}


public class MovieInputPostDTOValidator : AbstractValidator<MovieInputPostDTO> {
    public MovieInputPostDTOValidator() {
        RuleFor(movie=> movie.Titulo).NotNull().NotEmpty();
        RuleFor(movie => movie.Titulo).Length(2, 250).WithMessage("Tamanho {TotalLength} é invalido");

        RuleFor(movie=> movie.Ano).NotNull().NotEmpty();

        RuleFor(movie=> movie.Genero).NotNull().NotEmpty();
        RuleFor(movie => movie.Genero).Length(2, 250).WithMessage("Tamanho {TotalLength} é invalido");

        RuleFor(movie=> movie.DiretorId).NotNull().NotEmpty();

    }
}
