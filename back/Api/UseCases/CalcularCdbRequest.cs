using MediatR;

namespace Api.UseCases;

public class CalcularCdbRequest : IRequest<CalcularCdbResponse>
{
    public decimal ValorAporteInicial { get; set; }
    public int QuantidadeMeses { get; set; }
}
