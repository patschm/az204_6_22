namespace ACME.Backend.Entities;
public class Product: Entity
{
    public string? Name { get; set; }    
    public uint BrandID { get; set; } 
    public uint ProductGroupID { get; set; }
    public string? Image { get; set; }
    public Brand? Brand { get; set; }
    public ProductGroup? ProductGroup { get; set;}
    public ICollection<Price> Prices { get; set; } = new HashSet<Price>();
    public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    public ICollection<Specification> Specifications { get; set; } = new HashSet<Specification>();
}