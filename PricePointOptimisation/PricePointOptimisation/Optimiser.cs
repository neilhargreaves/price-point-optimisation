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
                var price = product.OriginalPrice;

                if (product.OriginalPrice < product.MinPrice)
                    price = product.MinPrice;

                if (product.OriginalPrice > product.MaxPrice)
                    price = product.MaxPrice;

                //Get last digit
                var isInteger = int.TryParse(price.ToString("n2")
                    .Substring(price.ToString().Length - 1), out var currentPoint);

                if (currentPoint != 0)
                {
                    if (!isInteger)
                    {
                        product.Message = "An error occured";
                        continue;
                    }
                }

                if (product.MinPrice > product.MaxPrice)
                {
                    product.Message = "An error occured";
                    continue;
                }


                //Check final digit isn't in optimisation list
                if (digitList.ToList().Any(x => x.PricePoint == currentPoint))
                {
                    product.Message = "Current price is optimal";
                    product.NewPrice = price;
                }

                ProcessProduct(product, digitList, price, currentPoint);
            }
        }

        private void ProcessProduct(IProduct product, IReadOnlyCollection<IDigit> digitList, double price, int currentPoint)
        {
            var closest = GetClosest(currentPoint, digitList);            

            if(currentPoint == 0)
            {
                var plusTenTest = GetClosest(currentPoint += 10, digitList);

                if (Math.Abs(plusTenTest - 10) < Math.Abs(closest - currentPoint))
                {
                    currentPoint = 10;
                    closest = plusTenTest;
                }
            }            

            SetPrice(product, price, closest, currentPoint);
        }

        private int GetClosest(int currentPoint, IReadOnlyCollection<IDigit> digitList)
        {
            var closestPricePoint = digitList
                .Aggregate((x, y) =>
                Math.Abs(x.PricePoint - currentPoint) < Math.Abs(y.PricePoint - currentPoint) ? x : y);

            return closestPricePoint.PricePoint;
        }

        private void SetPrice(IProduct product,double price, int closest, int currentPoint)
        {
            var value = Math.Abs(closest - currentPoint).ToString();
            value = value.Length == 1 ? $".0{value}" : value.Insert(value.Length - 2, ".");

            if (closest < currentPoint)
            {
                var isDeductionDouble = double.TryParse(value, out var deduction);

                if (!isDeductionDouble)
                {
                    product.Message = "An error occured";
                    return;
                }

                product.NewPrice = price -= deduction;
                product.Message = $"Price reduced";
            }
            else
            {
                var isAdditionDouble = double.TryParse(value, out var addition);

                if (!isAdditionDouble)
                {
                    product.Message = "An error occured";
                    return;
                }

                product.NewPrice = price + addition;
                product.Message = $"Price increased";
            }
        }
    }
}
