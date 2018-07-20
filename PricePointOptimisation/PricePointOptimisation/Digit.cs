namespace PricePointOptimisation
{
    public class Digit : IDigit
    {
        public int PricePoint { get; }

        public Digit(int pricePoint)
        {
            PricePoint = pricePoint;
        }
    }
}
