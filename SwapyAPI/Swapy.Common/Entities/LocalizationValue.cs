using Swapy.Common.Enums;

namespace Swapy.Common.Entities
{
    public class LocalizationValue
    {
        public string Id { get; set; }
        public Language Language { get; set; }
        public string Value { get; set; }

        //public string AnimalBreedId { get; set; }
        //public AnimalBreed AnimalBreed { get; set; }
        //public string CategoryId { get; set; }
        //public Category Category { get; set; }
        //public string CityId { get; set; }
        //public City City { get; set; }
        //public string ClothesSeasonId { get; set; }
        //public ClothesSeason ClothesSeason { get; set; }
        //public string ClothesViewId { get; set; }
        //public ClothesView ClothesView { get; set; }
        //public string ColorId { get; set; }
        //public Color Color { get; set; }
        //public string FuelTypeId { get; set; }
        //public FuelType FuelType { get; set; }
        //public string GenderId { get; set; }
        //public Gender Gender { get; set; }
        //public string SubcategoryId { get; set; }
        //public Subcategory Subcategory { get; set; }
        //public string TransmissionTypeId { get; set; }
        //public TransmissionType TransmissionType { get; set; }
        //public string TVTypeId { get; set; }
        //public TVType TVType { get; set; }

        public LocalizationValue() => Id = Guid.NewGuid().ToString();
    }
}
