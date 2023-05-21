namespace Swapy.Common.Entities
{
    public class SubcategoryBranch
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public Subcategory Parent { get; set; }
        public string ChildId { get; set; }
        public Subcategory Child { get; set; }

       public SubcategoryBranch() => Id = Guid.NewGuid().ToString();

        public SubcategoryBranch(string parentId, string childId) : this()
        {
            ParentId = parentId;
            ChildId = childId;
        }
    }
}
