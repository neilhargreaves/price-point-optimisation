using System.Collections.Generic;

namespace PricePointOptimisation
{
    public interface IProductProcessor
    {
        void Process();
        IOptimiser Optimiser { get; set; }
    }
}
