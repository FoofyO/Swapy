namespace Swapy.Common.Entities
{
    public class ClothesView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsChild { get; set; }
        public Guid GenderId { get; set; }
        public Gender Gender { get; set; }
        public Guid ClothesTypeId { get; set; }
        public Subcategory ClothesType { get; set; }

        public ICollection<ClothesBrandView> ClothesBrandViews { get; set; }
         
        public ClothesView() => ClothesBrandViews = new List<ClothesBrandView>();

        public ClothesView(string name, bool isChild, Guid genderId, Guid clothesTypeId) : this()
        { 
            Name = name;
            IsChild = isChild;
            GenderId = genderId;   
            ClothesTypeId = clothesTypeId;
        }  
    }
}   