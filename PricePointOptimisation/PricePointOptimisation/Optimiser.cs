using System;
using System.Collections.Generic;
using System.Linq;

namespace PricePointOptimisation
{
    public class Optimiser : IOptimiser
    {
        public void Optimise(IReadOnlyCollection<IProduct> productList, IReadOnlyCollection<IDigit> digitList)
        {
            foreach (var product in productList)
            {
                var startingPrice = Shared.SetStartingPrice(product);
                var currentPoint = Shared.GetLastDigit(product, startingPrice);
                Shared.CheckPriceRange(product);

                if (product.NewPrice != 0)
                    return;

                if (digitList.ToList().Any(x => x.PricePoint == currentPoint) && product.NewPrice == 0)
                    product.NewPrice = startingPrice;
                
                if (product.NewPrice == 0)
                    this.ProcessProduct(product, digitList, startingPrice, currentPoint);

                Shared.SetProductMessage(product);
            }
        }

        private void ProcessProduct(IProduct product, IReadOnlyCollection<IDigit> digitList, double price, int currentPoint)
        {
            if (digitList.Count == 0)
            {
                product.NewPrice = product.OriginalPrice;
                product.Message = "Unable to set price";
                return;
            }

            var plusTen = currentPoint + 10;
            var closest = this.GetClosest(currentPoint, digitList);
            var plusTenTest = this.GetClosest(plusTen, digitList);

            if (Math.Abs(plusTenTest - plusTen) < Math.Abs(closest - currentPoint))
            {
                currentPoint = 10;
                closest = plusTenTest;
            }

            this.SetPrice(product, price, closest, currentPoint, digitList);
        }

        private int GetClosest(int currentPoint, IReadOnlyCollection<IDigit> digitList)
        {
            var closestPricePoint = digitList
                .Aggregate((x, y) =>
                Math.Abs(x.PricePoint - currentPoint) < Math.Abs(y.PricePoint - currentPoint) ? x : y);

            return closestPricePoint.PricePoint;
        }

        private void SetPrice(IProduct product, double price, int closest, int currentPoint, IReadOnlyCollection<IDigit> digitList)
        {
            var value = Math.Abs(closest - currentPoint).ToString();
            value = value.Length == 1 ? $".0{value}" : value.Insert(value.Length - 2, ".");
            var isDouble = double.TryParse(value, out var change);
            var newDigitList = digitList
                        .Select(x => new Digit(x.PricePoint))
                        .Where(y => y.PricePoint != closest).ToList();

            if (!isDouble)
            {
                product.Message = "An error occured :: Price not in correct format";
                product.NewPrice = product.OriginalPrice;
                return;
            }

            if (closest < currentPoint)
            {
                var updatedPrice = price - change;

                if (updatedPrice < product.MinPrice || updatedPrice > product.MaxPrice)
                {
                    this.ProcessProduct(product, newDigitList, price, currentPoint);
                }
                else
                {
                    product.NewPrice = updatedPrice;
                }
            }
            else if (closest > currentPoint)
            {
                var updatedPrice = price + change;

                if (updatedPrice < product.MinPrice || updatedPrice > product.MaxPrice)
                {
                    this.ProcessProduct(product, newDigitList, price, currentPoint);
                }
                else
                {
                    product.NewPrice = updatedPrice;
                }
            }
        }
    }
}
