namespace APBD_CW2.Containers;

public class ColdContainer : Container
{
    private static Dictionary<string, double> _productsAndTemperatures = new Dictionary<string, double>
    {
        { "Bananas", 13.3 },
        { "Chocolate", 18.0 },
        { "Fish", 2.0 },
        { "Meat", -15.0 },
        { "Ice cream", -18.0 },
        { "Frozen Pizza", -30.0 },
        { "Cheese", 7.2 },
        { "Sausages", 5.0 },
        { "Butter", 20.5 },
        { "Eggs", 19.0 }
    };

    public string ProductType { get; set; }
    
    public double Temperature { get; set; }

    public ColdContainer(double cargoMass, double height, double weight, double depth, double maxLoad,
        string productType) : base(cargoMass, height, weight, depth, maxLoad, 'C')
    {
        if (!_productsAndTemperatures.ContainsKey(productType))
        {
            throw new KeyNotFoundException($"There is no product with type {productType}");
        }
        
        ProductType = productType;
        
        Temperature = _productsAndTemperatures[productType];
        
    }

    public override string ToString()
    {
        return base.ToString() + $"product type: {ProductType}, temperature: {Temperature}";
    }
}