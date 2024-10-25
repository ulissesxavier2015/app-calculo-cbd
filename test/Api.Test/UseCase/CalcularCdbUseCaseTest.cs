using Api.Domain.Interfaces;
using Api.Infrastructure;
using Api.Infrastructure.ExternalApi;
using Api.UseCases;
using FluentAssertions;
using Moq;

namespace Api.TestUnitario.UseCase;

public class CalcularCdbUseCaseTest
{

    private readonly Mock<ICdbService> _cdbService = new();

    [Theory]
    [InlineData(1000, 2, 4.40)]
    [InlineData(1500.30, 6, 20.17)]
    [InlineData(50000, 12, 1230.82)]
    [InlineData(100050.10, 24, 4575.28)]
    [InlineData(500000, 120, 164428.75)]
    public async Task DeveRetornarComSucessoQuandoResultadoDescontoForIgualEsperado(
        decimal valorInicial,
        int meses,
        decimal desconto)
    {
        // Setup
        var cdb = new CdbOutput { Cdi = 0.009M, TaxaBancaria = 1.08M };
        _cdbService.Setup(m => m.ObterTaxaCdb()).ReturnsAsync(cdb);

        // Scenario
        var requisicao = new CalcularCdbRequest
        {
            ValorAporteInicial = valorInicial,
            QuantidadeMeses = meses
        };

        // Action
        var validador = new CalcularCdbRequestValidator();
        var useCase = new CalcularCdbUseCase(validador, _cdbService.Object);

        var response = await useCase.Handle(requisicao, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.ValorBruto.Should().BeGreaterThan(0);
        response.ValorLiquido.Should().BeLessThan(response.ValorBruto);

        var resultadoDesconto = Math.Round(response.ValorBruto - response.ValorLiquido, 2);
        resultadoDesconto.Should().Be(desconto);
    }

    [Fact]
    public async Task DeveRetornaExcecaoQuandoPrazoMensalMenorQueDois()
    {
        // Scenario
        var requisicao = new CalcularCdbRequest
        {
            ValorAporteInicial = 80M,
            QuantidadeMeses = 1
        };

        var validador = new CalcularCdbRequestValidator();
        var useCase = new CalcularCdbUseCase(validador, _cdbService.Object);

        // Action
        var acao = useCase.Invoking(h => h.Handle(requisicao, CancellationToken.None));

        // Assert
        await acao.Should()
           .ThrowExactlyAsync<ValidationErrorException>()
           .WithMessage($"'{nameof(CalcularCdbRequest.QuantidadeMeses)}': A quantidade de meses deve ser maior que 1;");
    }

    [Fact]
    public async Task DeveRetornarExcecaoQuandoValorInicialNaoPositivo()
    {
        // Scenario
        var requisicao = new CalcularCdbRequest
        {
            ValorAporteInicial = -99M,
            QuantidadeMeses = 6
        };

        var validador = new CalcularCdbRequestValidator();

        var useCase = new CalcularCdbUseCase(validador, _cdbService.Object);

        // Action
        var acao = useCase.Invoking(h => h.Handle(requisicao, CancellationToken.None));

        // Assert
        await acao.Should()
            .ThrowExactlyAsync<ValidationErrorException>()
            .WithMessage($"'{nameof(CalcularCdbRequest.ValorAporteInicial)}': Aporte inicial deve ser maior que 0;");
    }
}