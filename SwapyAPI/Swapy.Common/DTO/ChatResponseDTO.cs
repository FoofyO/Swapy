namespace Swapy.Common.DTO
{
    public class ChatResponseDTO<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; } 

        public ChatResponseDTO(IEnumerable<T> items, int count, int allPages)
        { 
            Items = items;
            Count = count; 
        }
    }
}
