namespace Api.UseCases
{
    public record CalcularCdbResponse
    {
        public decimal ValorBruto { get; set; }
        public decimal ValorLiquido { get; set; }
        public decimal Desconto { get; set; }
    }
}
