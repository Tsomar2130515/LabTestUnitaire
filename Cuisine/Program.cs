using System;
using System.Collections.Generic;
using System.Text;

namespace Cuisine
{
    class Program
    {
        public const int TAILLE_PLAT_MOYEN = 5000;
        public const int TEMPS_MOYEN_PREPARATION_PLAT = 2000;

        static void Main(string[] _)
        {
            Console.WriteLine("Console de la cuisine");
            Cuisine cuisine = new CuisineBuilder().Build();
            cuisine.TailleMoyenPlat = TAILLE_PLAT_MOYEN;
            cuisine.TempsMoyenPrepPlat = TEMPS_MOYEN_PREPARATION_PLAT;
            cuisine.Demarrer();
        }
    }
}
