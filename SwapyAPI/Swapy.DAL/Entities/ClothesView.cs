namespace Swapy.DAL.Entities
{
    public class ClothesView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsChild { get; set; }
        public Guid GenderId { get; set; }
        public Gender Gender { get; set; }
        public Guid ClothesTypeId { get; set; }
        public ClothesType ClothesType { get; set; }

        public ICollection<ClothesBrandView> ClothesBrandsViews { get; set; }
         
        public ClothesView() => ClothesBrandView = new List<ClothesBrandView>();

        public ClothesView(string name, bool isChild, Guid genderId, Guid clothesTypeId) : this()
        { 
            Name = name;
            IsChild = isChild;
            Gender = genderId;   
            ClothesType = clothesTypeId;
               
        }  
    }

     
}   