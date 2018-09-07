using System;

namespace PricePointOptimisation
{
    public static class Shared
    {
        public static double SetStartingPrice(IProduct product)
        {
            var price = product.OriginalPrice;

            if (product.OriginalPrice < product.MinPrice)
                price = product.MinPrice;

            if (product.OriginalPrice > product.MaxPrice)
                price = product.MaxPrice;

            return price;
        }

        public static int GetLastDigit(IProduct product, double startingPrice)
        {
            var isInteger = int.TryParse(startingPrice.ToString("n2")
                    .Substring(startingPrice.ToString().Length - 1), out var currentPoint);

            if (currentPoint != 0)
            {
                if (!isInteger)
                {
                    product.Message = "An error occured :: Price Point Not an Integer";
                    product.NewPrice = product.OriginalPrice;
                }
            }

            return currentPoint;
        }

        public static int GetLastDigit(int startingPrice)
        {
            var isInteger = int.TryParse(startingPrice.ToString()
                    .Substring(startingPrice.ToString().Length - 1), out var currentPoint);

            if (currentPoint != 0 && !isInteger)
            {
                return -1;
            }

            return currentPoint;
        }

        public static void SetProductMessage(IProduct product)
        {
            if (string.IsNullOrEmpty(product.Message))
            {
                if (Math.Round(product.NewPrice, 2) < Math.Round(product.OriginalPrice, 2))
                    product.Message = "Price reduced";
                else if (Math.Round(product.NewPrice, 2) > Math.Round(product.OriginalPrice, 2))
                    product.Message = "Price increased";
                else
                    product.Message = "Current price is optimal";
            }
        }

        public static void CheckPriceRange(IProduct product)
        {
            if (product.MinPrice > product.MaxPrice || product.MaxPrice < product.MinPrice)
            {
                product.Message = "An error occured :: Price Range Incorrect";
                product.NewPrice = product.OriginalPrice;
            }
        }
    }
}
