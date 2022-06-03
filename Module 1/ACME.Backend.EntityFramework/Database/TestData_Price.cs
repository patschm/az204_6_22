using ACME.Backend.Entities;
using Bogus;

namespace ACME.Backend.EntityFramework;
public class TestData_Price
{
    private static List<Price> Prices = new();
    public static List<Price> TestData()
    {
        var fk = new Faker<Price>();
        foreach(var product in TestData_Product.TestData())
        {
            var range = fk.RuleFor(p=>p.BasePrice, f => f.Random.Float(100, 5000))
                .RuleFor(p=>p.ShopName, f => f.Company.CompanyName())
                .RuleFor(p=>p.PriceDate, f=>f.Date.Past(2, DateTime.Now))
                .RuleFor(p=>p.Product, f => product)
                .Generate(5)
                .ToList();
            Prices.AddRange(range);
        }
        return Prices;      
    }
}
