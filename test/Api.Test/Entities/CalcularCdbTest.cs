using Api.Domain.Entities;
using Api.Domain.Exceptions;
using Api.Infrastructure.ExternalApi;
using Api.UseCases;
using FluentAssertions;

namespace Api.Test.Unitario;

public class CalcularCdbTest
{             
    [Theory]
    [InlineData(1000, 2, 4.40)]
    [InlineData(1500.30, 6, 20.17)]
    [InlineData(50000, 12, 1230.82)]
    [InlineData(100050.10, 24, 4575.28)]
    [InlineData(500000, 120, 164428.75)]
    public void DeveRetornarComSucessoQuandoResultadoDescontoForIgualEsperado(
        decimal valorInicial,
        int meses, 
        decimal desconto)
    {
        // Setup
        var cdb = new CdbOutput { Cdi = 0.009M, TaxaBancaria = 1.08M };

        // Scenario
        var requisicao = new CalcularCdbRequest
        {
            ValorAporteInicial = valorInicial,
            QuantidadeMeses = meses
        };

        // Action
        var calcularCdb = new CalcularCdb(requisicao.ValorAporteInicial, requisicao.QuantidadeMeses, cdb.Cdi, cdb.TaxaBancaria);

        // Assert
        calcularCdb.Should().NotBeNull();
        calcularCdb.ValorBruto.Should().BeGreaterThan(0);
        calcularCdb.ValorLiquido.Should().BeLessThan(calcularCdb.ValorBruto);        

        var resultadoDesconto = Math.Round(calcularCdb.ValorBruto - calcularCdb.ValorLiquido, 2);
        resultadoDesconto.Should().Be(desconto);
    }

    [Fact]
    public void DeveRetornarExcecaoQuandoAporteInicialMenorIgualAZero()
    {
        // Setup
        var cdb = new CdbOutput { Cdi = 0.009M, TaxaBancaria = 1.08M };

        // Scenario
        var requisicao = new CalcularCdbRequest
        {
            ValorAporteInicial = 0,
            QuantidadeMeses = 0
        };

        // Action
        var ex = Assert.Throws<DomainExceptionValidation>(() => new CalcularCdb(requisicao.ValorAporteInicial, requisicao.QuantidadeMeses, cdb.Cdi, cdb.TaxaBancaria));

        // Assert        
        Assert.Equal("Valor aporte inicial não pode ser negativo.", ex.Message);
    }

    [Fact]    
    public void DeveRetornarExcecaoQuandoQuantidadeDeMesesMenorQueDois()
    {
        // Setup
        var cdb = new CdbOutput { Cdi = 0.009M, TaxaBancaria = 1.08M };

        // Scenario
        var requisicao = new CalcularCdbRequest
        {
            ValorAporteInicial = 5000,
            QuantidadeMeses = 1
        };

        // Action
        var ex = Assert.Throws<DomainExceptionValidation>(() => new CalcularCdb(requisicao.ValorAporteInicial, requisicao.QuantidadeMeses, cdb.Cdi, cdb.TaxaBancaria));

        // Assert        
        Assert.Equal("Quantidade de meses não pode ser menos que 2 meses.", ex.Message);
    }

    [Fact]
    public void DeveRetornarExcecaoQuandoTaxaCdiIgualAZero()
    {
        // Setup
        var cdb = new CdbOutput { Cdi = 0, TaxaBancaria = 1.08M };

        // Scenario
        var requisicao = new CalcularCdbRequest
        {
            ValorAporteInicial = 4000,
            QuantidadeMeses = 12
        };

        // Action
        var ex = Assert.Throws<DomainExceptionValidation>(() => new CalcularCdb(requisicao.ValorAporteInicial, requisicao.QuantidadeMeses, cdb.Cdi, cdb.TaxaBancaria));

        // Assert        
        Assert.Equal("Taxa cdi deve ser informada.", ex.Message);
    }

    [Fact]
    public void DeveRetornarExcecaoQuandoTaxaBancariaIgualAZero()
    {
        // Setup
        var cdb = new CdbOutput { Cdi = 0.009M, TaxaBancaria = 0 };

        // Scenario
        var requisicao = new CalcularCdbRequest
        {
            ValorAporteInicial = 50000,
            QuantidadeMeses = 6
        };

        // Action
        var ex = Assert.Throws<DomainExceptionValidation>(() => new CalcularCdb(requisicao.ValorAporteInicial, requisicao.QuantidadeMeses, cdb.Cdi, cdb.TaxaBancaria));

        // Assert        
        Assert.Equal("Taxa bancária deve ser informada.", ex.Message);
    }
}