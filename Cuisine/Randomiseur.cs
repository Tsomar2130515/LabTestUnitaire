using System;
using System.Collections.Generic;
using System.Text;

namespace Cuisine.Models
{
    public class Randomiseur : IRandomiseur
    {
        private Random rnd = new ();

        public Randomiseur() 
        { 
        }

        public void SetSourceDAlea(Random rnd)
        {
            this.rnd = rnd;
        }

        public int Next(int L)
        {
            return rnd.Next(L);
        }

        public double ExpoRandom(double L)
        {
            return -L * Math.Log(1 - rnd.NextDouble());
        }

        public T DistributionProbabiliteDiscrete<T>(Dictionary<T, int> distribution)
        {
            int taille = 0;
            foreach (var s in distribution)
                taille += s.Value;

            int choix = rnd.Next(taille);
            int total = 0;
            foreach (var s in distribution)
            {
                total += s.Value;
                if (choix < total)
                    return s.Key;
            }
            return default;
        }
    }
}
