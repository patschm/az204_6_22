using ACME.Backend.Entities;
using Bogus;

namespace ACME.Backend.EntityFramework;
public class TestData_Review
{
    private static List<Review> Reviews = new();
    public static List<Review> TestData()
    {
        uint id = 0;
        var fk = new Faker<Review>();
        foreach(var product in TestData_Product.TestData())
        {
            var range = fk.RuleFor(r=>r.ID, f => ++id)  
            .RuleFor(r=>r.Author, f => f.Person.FullName)
            .RuleFor(r=>r.Email, f => f.Person.Email)
            .RuleFor(r=>r.Score, f => (byte)f.Random.Number(1, 5))
            .RuleFor(r=>r.Text, f => f.Lorem.Lines(f.Random.Number(1,5)))
            .RuleFor(r=>r.Product, f => product)
            .Generate(5)
            .ToList();
            Reviews.AddRange(range);
        }
        Reviews.ForEach(r=> r.ID = 0);
        return Reviews;      
    }
}
