namespace Swapy.DAL.Entities
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public ProductImage() { }

<<<<<<< HEAD
        public ProductImage(string image, Guid productId) : this()
=======
        public ProductImage(string image, Guid productId)
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0
        {
            Image = image;
            ProductId = productId;
        }
    }
}
