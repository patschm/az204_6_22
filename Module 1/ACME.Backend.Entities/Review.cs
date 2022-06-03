namespace ACME.Backend.Entities;
public class Review: Entity
{
    public int ProductID {get; set; }
    public string? Author { get; set; }
    public string? Text { get; set; }
    public byte Score { get; set; }
    public string? Email { get; set; }
    public Product? Product { get; set; }
}