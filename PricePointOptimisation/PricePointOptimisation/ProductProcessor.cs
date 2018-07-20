using System;
using System.Collections.Generic;

namespace PricePointOptimisation
{
    public class ProductProcessor : IProductProcessor
    {
        public IReadOnlyCollection<IProduct> Products;
        public IReadOnlyCollection<IDigit> PricePoints;
        public IOptimiser Optimiser { get; set; }

        public ProductProcessor(IReadOnlyCollection<IProduct> products, IReadOnlyCollection<IDigit> pricePoints)
        {
            Products = products;
            PricePoints = pricePoints;
        }

        public void Process()
        {
            Optimiser.Optimise(Products, PricePoints);
        }
    }
}
