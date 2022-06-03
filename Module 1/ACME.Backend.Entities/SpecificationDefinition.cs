namespace ACME.Backend.Entities;
public class SpecificationDefinition: Entity
{
    public string? Key { get; set; }
    public string? Name { get; set; }
    public string? Value { get; set; }
    public string? Unit { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }
    
    public uint ProductGroupID { get; set; }
    public ProductGroup? ProductGroup { get; set; }
}
