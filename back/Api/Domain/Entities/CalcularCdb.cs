using Api.Domain.Exceptions;

namespace Api.Domain.Entities;

public class CalcularCdb
{
    public CalcularCdb(decimal valorAporteInicial, int quantidadeMeses, decimal cdi, decimal taxaBancaria)
    {
        ValorAporteInicial = valorAporteInicial;
        QuantidadeMeses = quantidadeMeses;
        Cdi = cdi;
        TaxaBancaria = taxaBancaria;

        Validar();
        CalcularValorBruto();
        CalculoValorLiquido();
    }

    private decimal Cdi { get; set; }
    private decimal TaxaBancaria { get; set; }
    private decimal ValorAporteInicial { get; set; }
    private int QuantidadeMeses { get; set; }
    public decimal ValorBruto { get; private set; }
    public decimal ValorLiquido { get; private set; }

    /// <summary>
    /// Para o cálculo do CDB, deve-se utilizar a fórmula VF = VI x [1 +
    /// (CDI x TB)] onde:
    /// i.VF é o valor final;
    /// ii.VI é o valor inicial;
    /// iii.CDI é o valor dessa taxa no último mês;
    /// iv.TB é quanto o banco paga sobre o CDI;
    /// </summary>
    /// <returns></returns>
    public decimal CalcularValorBruto()
    {
        var valorBruto = calcular(ValorAporteInicial);

        for (var i = 1; i < QuantidadeMeses; i++)
        {
            valorBruto = calcular(valorBruto);
        }

        ValorBruto = valorBruto;

        return valorBruto;

        decimal calcular(decimal vi) => vi * (1 + Cdi * TaxaBancaria);
    }

    public decimal CalculoValorLiquido()
    {
        var aliquota = ObterAliquota();

        var imposto = aliquota * (ValorBruto - ValorAporteInicial);

        ValorLiquido = ValorBruto - imposto;

        return ValorLiquido;
    }

    public decimal ObterValorDesconto() => ValorBruto - ValorLiquido;


    /// <summary>
    /// Para cálculo do imposto utilizar a seguinte tabela:
    /// i.Até 06 meses: 22,5%
    /// ii.Até 12 meses: 20%
    /// iii.Até 24 meses 17,5%
    /// iv.Acima de 24 meses 15%
    /// </summary>
    /// <returns></returns>
    private decimal ObterAliquota() => QuantidadeMeses switch
    {
        > 0 and <= 6 => 0.225M,
        > 6 and <= 12 => 0.20M,
        > 12 and <= 24 => 0.175M,
        _ => 0.15M
    };

    private void Validar()
    {
        DomainExceptionValidation.When(ValorAporteInicial <= 0, "Valor aporte inicial não pode ser negativo.");
        DomainExceptionValidation.When(QuantidadeMeses == 0, "Quantidade de meses deve ser informada.");
        DomainExceptionValidation.When(QuantidadeMeses < 2, "Quantidade de meses não pode ser menos que 2 meses.");
        DomainExceptionValidation.When(QuantidadeMeses > 500, "Quantidade de meses não suportada.");
        DomainExceptionValidation.When(Cdi == 0, "Taxa cdi deve ser informada.");
        DomainExceptionValidation.When(TaxaBancaria == 0, "Taxa bancária deve ser informada.");
    }

}
