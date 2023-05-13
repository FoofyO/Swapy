namespace Swapy.Common.DTO
{
    public class ProductResponseDTO<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }
        public int AllPages { get; set; }

        public ProductResponseDTO(IEnumerable<T> items, int count, int allPages)
        {
            Items = items;
            Count = count;
            AllPages = allPages;
        }
    }
}
