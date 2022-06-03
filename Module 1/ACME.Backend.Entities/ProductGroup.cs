namespace ACME.Backend.Entities;
public class ProductGroup: Entity
{
    public string? Name { get; set; }
    public string? Image { get; set; }
    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    public ICollection<SpecificationDefinition> SpecificationDefinitions { get; set; } = new HashSet<SpecificationDefinition>();
}