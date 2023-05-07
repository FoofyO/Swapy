﻿namespace Swapy.Common.Entities
{
    public class FavoriteProduct
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public FavoriteProduct() { }

        public FavoriteProduct(Guid userId, Guid productId)
        {
            UserId = userId;
            ProductId = productId;
        }
    }
}
