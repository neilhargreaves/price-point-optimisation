namespace PricePointOptimisation
{
    public class Product :IProduct
    {
        private readonly string ProductName;
        public double OriginalPrice { get; }
        public double MinPrice { get; }
        public double MaxPrice { get; }
        public double NewPrice { get; set; }
        public string Message { get; set; }

        public Product(string productName, double originalPrice, double minPrice, double maxPrice)
        {
            ProductName = productName;
            OriginalPrice = originalPrice;
            MinPrice = minPrice;
            MaxPrice = maxPrice;                           
        }

        public override string ToString()
        {
            return ProductName;
        }
    }
}
