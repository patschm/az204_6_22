namespace ACME.Backend.Entities;
public class Price: Entity
{
    public uint ProductID { get; set; }
    public double BasePrice { get; set; }
    public string? ShopName { get; set; }
    public DateTime PriceDate { get; set; }
    public Product? Product { get; set; }
}