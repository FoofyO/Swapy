namespace Swapy.Common.DTO.Products.Responses
{
    public class CurrencyResponseDTO : SpecificationResponseDTO
    {
        public string Symbol { get; set; }

        public CurrencyResponseDTO() : base() { }

        public CurrencyResponseDTO(string id, string name, string symbol) : base(id, name) => Symbol = symbol;
    }
}
