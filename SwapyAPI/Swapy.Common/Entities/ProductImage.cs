namespace Swapy.Common.Entities
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public ProductImage() { }

        public ProductImage(string image, Guid productId) : this()
        {
            Image = image;
            ProductId = productId;
        }
    }
}
