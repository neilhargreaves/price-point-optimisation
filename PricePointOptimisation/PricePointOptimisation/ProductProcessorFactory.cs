using System.Collections.Generic;

namespace PricePointOptimisation
{
    public static class ProductProcessorFactory
    {
        public static IProductProcessor GetProductProcessor(IReadOnlyCollection<IProduct> products, IReadOnlyCollection<IDigit> pricePoints)
        {
            return new ProductProcessor(products, pricePoints);
        }
    }
}
