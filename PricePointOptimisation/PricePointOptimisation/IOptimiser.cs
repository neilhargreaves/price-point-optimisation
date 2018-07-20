using System.Collections.Generic;

namespace PricePointOptimisation
{
    public interface IOptimiser
    {
        void Optimise(IReadOnlyCollection<IProduct> productList, IReadOnlyCollection<IDigit> digitList);
    }
}
