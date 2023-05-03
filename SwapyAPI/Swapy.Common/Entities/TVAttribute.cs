namespace Swapy.Common.Entities
{
    public class TVAttribute
    {
        public Guid Id { get; set; }  
        public bool IsNew { get; set; }
        public bool IsSmart { get; set; }
        public Guid TVTypeId { get; set; }
        public TVType TVType { get; set; }
        public Guid TVBrandId { get; set; }
        public TVBrand TVBrand { get; set; }
        public Guid ScreenResolutionId { get; set; } 
        public ScreenResolution ScreenResolution { get; set; }
        public Guid ScreenDiagonalId { get; set; }
        public ScreenDiagonal ScreenDiagonal { get; set; }  
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public TVAttribute() { } 

        public TVAttribute(bool isNew, bool isSmart, Guid tvTypeId, Guid tvBrandId, Guid screenResolutionId, Guid screenDiagonalId, Guid productId)
        {  
            IsNew = isNew;
            IsSmart = isSmart; 
            TVTypeId = tvTypeId;
            TVBrandId = tvBrandId;
            ScreenResolutionId = screenResolutionId;
            ScreenDiagonalId = screenDiagonalId;  
            ProductId = productId;
        }  
    }
}
