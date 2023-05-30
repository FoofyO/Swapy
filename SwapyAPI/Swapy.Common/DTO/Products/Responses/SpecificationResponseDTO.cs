namespace Swapy.Common.DTO.Products.Responses
{
    public class SpecificationResponseDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public SpecificationResponseDTO() { }

        public SpecificationResponseDTO(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
