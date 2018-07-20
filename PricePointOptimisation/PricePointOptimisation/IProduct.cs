namespace PricePointOptimisation
{
    public interface IProduct
    {
        double OriginalPrice { get; }
        double MinPrice { get; }
        double MaxPrice { get; }
        double NewPrice { get; set; }
        string Message { get; set; }
    }
}
