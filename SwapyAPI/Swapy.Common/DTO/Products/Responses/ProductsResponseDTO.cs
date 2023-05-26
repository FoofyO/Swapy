namespace Swapy.Common.DTO.Products.Responses
{
    public class ProductsResponseDTO<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }
        public int AllPages { get; set; }

        public ProductsResponseDTO(IEnumerable<T> items, int count, int allPages)
        {
            Items = items;
            Count = count;
            AllPages = allPages;
        }
    }
}
