using FluentValidation;

namespace Api.UseCases;

public class CalcularCdbRequestValidator : AbstractValidator<CalcularCdbRequest>
{
    public CalcularCdbRequestValidator()
    {
        RuleFor(x => x.ValorAporteInicial)
            .GreaterThan(0)
            .WithMessage("Aporte inicial deve ser maior que 0");

        RuleFor(x => x.QuantidadeMeses)
            .GreaterThan(1)
            .WithMessage("A quantidade de meses deve ser maior que 1");
    }
}
