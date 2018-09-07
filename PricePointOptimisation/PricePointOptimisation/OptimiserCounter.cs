using System;
using System.Collections.Generic;
using System.Linq;

namespace PricePointOptimisation
{
    public class OptimiserCounter : IOptimiser
    {
        public void Optimise(IReadOnlyCollection<IProduct> productList, IReadOnlyCollection<IDigit> digitList)
        {
            foreach (var product in productList)
            {
                var startingPrice = Shared.SetStartingPrice(product);
                var currentPoint = Shared.GetLastDigit(product, startingPrice);
                Shared.CheckPriceRange(product);

                //if (product.NewPrice != 0)
                //    return;

                if (digitList.ToList().Any(x => x.PricePoint == currentPoint) && product.NewPrice == 0)
                    product.NewPrice = startingPrice;

                if (product.NewPrice == 0)
                {
                    this.ProcessProduct(product, startingPrice, digitList);
                }

                Shared.SetProductMessage(product);
            }
        }


        private int ConvertToPence(double value)
        {
            var pence = value * 100;
            return (int)Math.Ceiling(pence);
        }

        private double ConvertToPounds(int value)
        {
            return (double)value / 100;
        }

        private void ProcessProduct(IProduct product, double startingPrice, IReadOnlyCollection<IDigit> digitList)
        {
            var startingPriceInPence = this.ConvertToPence(startingPrice);
            var maxPriceInPence = this.ConvertToPence(product.MaxPrice);
            var minPriceInPence = this.ConvertToPence(product.MinPrice);

            var incrementSteps = this.Increment(maxPriceInPence, minPriceInPence, startingPriceInPence, digitList);
            var decrementSteps = this.Decrement(minPriceInPence, maxPriceInPence, startingPriceInPence, digitList);

            if (incrementSteps == -1 && decrementSteps == -1)
            {
                product.NewPrice = product.OriginalPrice;
                product.Message = "Unable to set price";
                return;
            }

            if (decrementSteps == -1)
            {
                var newIncrementPrice = this.ConvertToPounds(startingPriceInPence + incrementSteps);
                product.NewPrice = newIncrementPrice;
                return;
            }

            if (incrementSteps == -1)
            {
                var newDecrementPrice = this.ConvertToPounds(startingPriceInPence - decrementSteps);
                product.NewPrice = newDecrementPrice;
                return;
            }

            if (incrementSteps <= decrementSteps)
            {
                var newIncrementPrice = this.ConvertToPounds(startingPriceInPence + incrementSteps);
                product.NewPrice = newIncrementPrice;
                return;
            }

            var newPrice = this.ConvertToPounds(startingPriceInPence - decrementSteps);
            product.NewPrice = newPrice;
        }

        private int Increment(int maxValue, int minValue, int currentValue, IReadOnlyCollection<IDigit> digitList)
        {
            var steps = 0;
            var found = false;
            var value = currentValue;

            while (value <= maxValue)
            {
                var lastDigit = Shared.GetLastDigit(value);

                if (lastDigit == -1)
                    break;

                if (digitList.ToList().Any(x => x.PricePoint == lastDigit) && value >= minValue)
                {
                    found = true;
                    break;
                }

                value++;
                steps++;
            }


            if (found)
                return steps;

            return -1;
        }

        private int Decrement(int minValue, int maxValue, int currentValue, IReadOnlyCollection<IDigit> digitList)
        {
            var steps = 0;
            var found = false;
            var value = currentValue;

            while (value >= minValue)
            {
                var lastDigit = Shared.GetLastDigit(value);

                if (lastDigit == -1)
                    break;

                if (digitList.ToList().Any(x => x.PricePoint == lastDigit) && value <= maxValue)
                {
                    found = true;
                    break;
                }

                value--;
                steps++;
            }


            if (found)
                return steps;

            return -1;
        }


    }
}
