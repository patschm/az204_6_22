namespace ACME.Backend.Entities;
public class Specification: Entity
{
    public string? Key { get; set; }
    public bool? BoolValue { get; set;}
    public string? StringValue { get; set;}
    public double? NumberValue { get; set;}
    public uint ProductID { get; set; }
    public Product? Product { get; set; }
}
