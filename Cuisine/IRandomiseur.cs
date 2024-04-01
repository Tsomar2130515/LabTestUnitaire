using System;
using System.Collections.Generic;
using System.Text;

namespace Cuisine.Models
{
    public interface IRandomiseur
    {
        public void SetSourceDAlea(Random r);

        public int Next(int L);
        
        public double ExpoRandom(double L);
        
        public T DistributionProbabiliteDiscrete<T>(Dictionary<T, int> distribution);
    }
}
