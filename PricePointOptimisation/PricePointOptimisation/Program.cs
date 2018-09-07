using System;
using System.Collections.Generic;

namespace PricePointOptimisation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Price Point Optimiser");

            var products = new List<IProduct>
            {
                new Product("A", 68.91, 68.85, 69.05),
                new Product("B", 71.92, 71.92, 71.98),
                new Product("C", 69.93, 69.96, 69.98),
                new Product("D", 68.54, 68.96, 69.98),
                new Product("E", 68.95, 68.85, 69.05),
                new Product("F", 68.96, 68.85, 69.05),
                new Product("G", 68.97, 68.85, 69.05),
                new Product("H", 68.98, 68.85, 69.05),
                new Product("I", 68.99, 68.85, 69.05),
                new Product("J", 69.00, 68.85, 69.95),
                new Product("K", 69.10, 68.85, 69.05),
                new Product("L", 68.50, 68.85, 69.95)
            };

            PricePointList(products);
            PricePointListEdgeCase(products);
            PricePointListSinglePoint(products);

            Console.ReadLine();
        }

        private static void PricePointList(List<IProduct> products)
        {
            Console.WriteLine("Basic Tests");

            var pricePoints = new List<IDigit>
            {
                new Digit(3),
                new Digit(5),
                new Digit(9)
            };

            IProductProcessor productProcessor = ProductProcessorFactory.GetProductProcessor(products, pricePoints);

            productProcessor.Optimiser = new Optimiser();
            productProcessor.Process();

            foreach (var product in products)
            {
                Console.WriteLine($"Name: {product.ToString()}");
                Console.WriteLine($"Original Price: {product.OriginalPrice}");
                Console.WriteLine($"New Price: {product.NewPrice}");
                Console.WriteLine($"Min Price: {product.MinPrice}");
                Console.WriteLine($"Max Price: {product.MaxPrice}");
                Console.WriteLine($"Message: {product.Message}");
                Console.WriteLine("####################");
            }

            Console.WriteLine("Basic Tests");
            Console.WriteLine();
        }

        private static void PricePointListEdgeCase(List<IProduct> products)
        {
            Console.WriteLine("Edge Case Tests");

            var pricePoints = new List<IDigit>
            {
                new Digit(3),
                new Digit(9)
            };

            IProductProcessor productProcessor = ProductProcessorFactory.GetProductProcessor(products, pricePoints);

            productProcessor.Optimiser = new Optimiser();
            productProcessor.Process();

            foreach (var product in products)
            {
                Console.WriteLine($"Name: {product.ToString()}");
                Console.WriteLine($"Original Price: {product.OriginalPrice}");
                Console.WriteLine($"New Price: {product.NewPrice}");
                Console.WriteLine($"Min Price: {product.MinPrice}");
                Console.WriteLine($"Max Price: {product.MaxPrice}");
                Console.WriteLine($"Message: {product.Message}");
            }

            Console.WriteLine("Edge Case Tests");
            Console.WriteLine();
        }

        private static void PricePointListSinglePoint(List<IProduct> products)
        {
            Console.WriteLine("Single Point Tests");

            var pricePoints = new List<IDigit>
            {
                new Digit(3)
            };

            IProductProcessor productProcessor = ProductProcessorFactory.GetProductProcessor(products, pricePoints);

            productProcessor.Optimiser = new Optimiser();
            productProcessor.Process();

            foreach (var product in products)
            {
                Console.WriteLine($"Name: {product.ToString()}");
                Console.WriteLine($"Original Price: {product.OriginalPrice}");
                Console.WriteLine($"New Price: {product.NewPrice}");
                Console.WriteLine($"Min Price: {product.MinPrice}");
                Console.WriteLine($"Max Price: {product.MaxPrice}");
                Console.WriteLine($"Message: {product.Message}");
            }
        }
    }
}
