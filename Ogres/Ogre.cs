using Ogres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ogres
{
    public class ComparateurDentSucree : IComparer<TypePlat>
    {
        public int Compare(TypePlat x, TypePlat y)
        {
            if (x == null || y == null) return 0;
            return x.Nom == "S" ? (y.Nom == "S" ? 0 : -1) : (y.Nom == "S" ? 1 : 0);
        }
    }

    public class Ogre
    {
        public Action annoncer;
        //private readonly Func<Plat> nextPlat;
        public Func<Plat> nextPlat;
        public Plat platSeFaisantManger;
        public Action<int> Dormir { get; set; }
        public string Name { get; set; }

        public Ogre(string name, Func<Plat> nextPlat)
        {
            this.nextPlat = nextPlat;
            Name = name;
        }

        public void AddObservateur(Action annoncer)
        {
            this.annoncer += annoncer;
        }

        //Faire un test dessus
        public void Demarrer()
        {
            while (true)
            {
                Manger();
                Dormir(10);
            }
        }

        //Faire un test dessus
        public void Manger()
        {
            platSeFaisantManger = nextPlat();
            if (platSeFaisantManger != null)
            {
                annoncer();
                Dormir(platSeFaisantManger.Taille);
                platSeFaisantManger = null;
                annoncer();
            }
        }

        //Loup
        public static Plat StrategieCarnivore(IQueryable<Plat> plats)
        {
            // IQueryable ne supporte pas les comparateurs customs
            return plats.Where(p => p.TypePlat.Nom == "C" || p.TypePlat.Nom == "S").ToList().
                OrderBy(p => p.TypePlat, new ComparateurDentSucree()).
                ThenByDescending(p => p.Taille).FirstOrDefault();
        }

        //Animal choisi
        public static Plat StrategieSnob(IQueryable<Plat> plats)
        {
            // IQueryable ne supporte pas les comparateurs customs
            return plats.Where(p => p.Taille < 3000).ToList().
                OrderByDescending(p => p.DateCreation).FirstOrDefault();
        }

        //Chevreuil
        public static Plat StrategieVegetarien(IQueryable<Plat> plats)
        {
            return plats.Where(p => p.TypePlat.Nom == "V" || p.TypePlat.Nom == "S").ToList().
                OrderBy(p => p.TypePlat, new ComparateurDentSucree()).
                ThenByDescending(p => p.Taille).FirstOrDefault();
        }

        //Ours
        public static Plat StrategieGrossePortion(IQueryable<Plat> plats)
        {
            return plats.OrderByDescending(p => p.Taille).FirstOrDefault();
        }

        public override string ToString()
        {
            return Name + " mange " + platSeFaisantManger;
        }
    }
}
