using ACME.Backend.Entities;

namespace ACME.Backend.EntityFramework;
public static class TestData_ProductGroup
{
    private static Dictionary<uint, string> PgRefList = new Dictionary<uint, string>{{1, "Laptops en notebooks"},{6, "DVD-spelers"},{2, "PDA's en palmtops"},{8, "Digitale Camera's"},{13, "Scanners"},{14, "Inkjet-printers"},{15, "Monitors"},{10, "Beamers en projectoren"},{7, "Spiegelreflex Camera's"},{22, "Huistelefoons"},{16, "Draadloze routers"},{23, "PC"},{12, "MP3-spelers"},{24, "Laserprinters"}};
    private static List<ProductGroup> ProductGroups = new();
    public static List<ProductGroup> TestData()
    {
        if (ProductGroups.Count > 0) return ProductGroups;
        ProductGroups = new List<ProductGroup> {
            new ProductGroup { Name="Laserprinters", Image="laserprinters.png"},
            new ProductGroup { Name="Laptops en notebooks", Image="laptops_en_notebooks.png"},
            new ProductGroup { Name="Inkjet-printers", Image="inkjet_printers.png"},
            new ProductGroup { Name="MP3-spelers", Image="mp3_spelers.png"},
            new ProductGroup { Name="Beamers en projectoren", Image="beamers_en_projectoren.png"},
            new ProductGroup { Name="Digitale Camera's", Image="digitale_camera_s.png"},
            new ProductGroup { Name="Draadloze routers", Image="draadloze_routers.png"},
            new ProductGroup { Name="Spiegelreflex Camera's", Image="spiegelreflex_camera_s.png"},
            new ProductGroup { Name="Scanners", Image="scanners.png"},
            new ProductGroup { Name="Huistelefoons", Image="huistelefoons.png"},
            new ProductGroup { Name="PC", Image="pc.png"},
            new ProductGroup { Name="Monitors", Image="monitors.png"}
        };
        return ProductGroups;
    }
    public static ProductGroup? FindProductGroup(uint oldId)
    {
        if (PgRefList.TryGetValue(oldId, out string? name))
        {
            return ProductGroups.FirstOrDefault(pg=>pg.Name == name);
        }
        return null;
    }
}
