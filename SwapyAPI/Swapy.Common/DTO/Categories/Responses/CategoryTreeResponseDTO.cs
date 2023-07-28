using Swapy.Common.DTO.Products.Responses;

namespace Swapy.Common.DTO.Categories.Responses
{
    public class CategoryTreeResponseDTO
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public bool IsFinal { get; set; }
        public SpecificationResponseDTO<string> Parent { get; set; }

        public CategoryTreeResponseDTO() { }

        public CategoryTreeResponseDTO(string id, string value, bool isFinal, string parentId, string parentName)
        {
            Id = id;
            Value = value;
            IsFinal = isFinal;
            Parent = new SpecificationResponseDTO<string>(parentId, parentName);
        }
    }
}
