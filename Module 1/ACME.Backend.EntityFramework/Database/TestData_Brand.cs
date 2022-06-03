using ACME.Backend.Entities;

namespace ACME.Backend.EntityFramework;
public class TestData_Brand
{
    private static List<Brand> Brands = new();
    public static List<Brand> TestData()
    {
        if (Brands.Count > 0) return Brands;
        Brands = new List<Brand>{
            new Brand { Name="Lenovo"},
            new Brand { Name="Iiyama"},
            new Brand { Name="Lexmark"},
            new Brand { Name="AOC"},
            new Brand { Name="Optoma"},
            new Brand { Name="vivitek"},
            new Brand { Name="ViewSonic"},
            new Brand { Name="Pentax"},
            new Brand { Name="Grundig"},
            new Brand { Name="AEG"},
            new Brand { Name="Gigaset"},
            new Brand { Name="Audioline"},
            new Brand { Name="AVM"},
            new Brand { Name="LG"},
            new Brand { Name="Swissvoice"},
            new Brand { Name="Topcom"},
            new Brand { Name="Synology"},
            new Brand { Name="Linksys"},
            new Brand { Name="TP-Link"},
            new Brand { Name="D-link"},
            new Brand { Name="DrayTek"},
            new Brand { Name="Netgear"},
            new Brand { Name="Edimax"},
            new Brand { Name="Apple"},
            new Brand { Name="Cowon"},
            new Brand { Name="Sandisk"},
            new Brand { Name="Archos"},
            new Brand { Name="Siemens"},
            new Brand { Name="Philips"},
            new Brand { Name="Samsung"},
            new Brand { Name="Sony"},
            new Brand { Name="Asus"},
            new Brand { Name="Dell"},
            new Brand { Name="HP"},
            new Brand { Name="Toshiba"},
            new Brand { Name="MSI"},
            new Brand { Name="Acer"},
            new Brand { Name="Microsoft"},
            new Brand { Name="Panasonic"},
            new Brand { Name="PolaroID"},
            new Brand { Name="Ricoh"},
            new Brand { Name="BenQ"},
            new Brand { Name="Canon"},
            new Brand { Name="Sigma"},
            new Brand { Name="Olympus"},
            new Brand { Name="Kyocera"},
            new Brand { Name="Nikon"},
            new Brand { Name="Kodak"},
            new Brand { Name="Epson"},
            new Brand { Name="Brother"},
            new Brand { Name="Plustek"},
            new Brand { Name="Reflecta"},
            new Brand { Name="Leica"},
            new Brand { Name="Fujitsu"},
            new Brand { Name="Xerox"}
        };  
        foreach(var item in Brands)
        {
            item.Website = $"www.{item.Name?.ToLower()}.com";
        }
        return Brands;      
    }
}
