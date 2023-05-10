namespace Swapy.Common.Entities
{
    public class FavoriteProduct
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public FavoriteProduct() { }

        public FavoriteProduct(string userId, string productId)
        {
            UserId = userId;
            ProductId = productId;
        }
    }
}
