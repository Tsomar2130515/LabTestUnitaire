
using Cuisine.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using static Cuisine.Models.Plat;

namespace Cuisine
{
    public class Cuisine
    {
        public IRandomiseur Alea { get; set; }
        public BuffetBDContext Contexte { get; set; }

        public Action<int> Dormir { get; set; }
        public Action<string> Notifier { get; set; }
        public Func<DateTime> Maintenant { get; set;  }

        public int TempsMoyenPrepPlat { get; set; }

        public int TailleMoyenPlat { get; set; }

        public Dictionary<TypePlat, int> distribution;

        public Cuisine()
        {
            TempsMoyenPrepPlat = 1000;
            TailleMoyenPlat = 10000;
        }

        public void Demarrer()
        {
            if (!Contexte.TypePlats.Any())
            {
                Contexte.TypePlats.Add(new TypePlat() { Nom = "V", Probabilite = 6 });
                Contexte.TypePlats.Add(new TypePlat() { Nom = "C", Probabilite = 4 });
                Contexte.TypePlats.Add(new TypePlat() { Nom = "S", Probabilite = 1 });
            }
            Contexte.SaveChanges();
            distribution = new Dictionary<TypePlat, int>();
            foreach (var type in Contexte.TypePlats)
                distribution.Add(type, type.Probabilite);

            try
            {
                while (true) // TODO fin de service
                    Tour();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.Read();
        }

        public Plat DernierPlatAjoute()
        {
            return Contexte.Plats.OrderByDescending(p => p.PlatId).FirstOrDefault();
        }


        public void Tour()
        {
            Plat c = NouveauPlat();
            Contexte.Plats.Add(c);
            Contexte.SaveChanges();
            Notifier("");

            Dormir((int)Alea.ExpoRandom(TempsMoyenPrepPlat));
           
        }
        
        //Faire un test dessus
        public Plat NouveauPlat()
        {
            return new Plat
            {
                Taille = (int)Alea.ExpoRandom(TailleMoyenPlat),
                DateCreation = Maintenant(),
                TypePlat = Alea.DistributionProbabiliteDiscrete(distribution)
            };
        }
    }
}

// PM > Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB;Database=BuffetBDSolution;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
