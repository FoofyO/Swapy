namespace Swapy.BLL.Interfaces
{
    public interface IFavoriteProductsService
    {
        Task<bool> IsFavoriteAsync(string productId, string userId);
    }
}
