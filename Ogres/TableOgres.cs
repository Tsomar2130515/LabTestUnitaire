using Microsoft.EntityFrameworkCore;
using Ogres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Ogres.Ogre;

namespace Ogres
{
    public class TableOgres
    {
        private List<Ogre> ogres;
        private static readonly object verrouChoixPlat = new();
        private static readonly object verrouAffichage = new();

        //private readonly BuffetBDContext contexte = new();
        public BuffetBDContext contexte { get; set; }

        public TableOgres() { }

        public TableOgres(BuffetBDContext contexte)
        {
            this.contexte = contexte;
        }

        public void Start()
        {
            ogres = new List<Ogre> {
                new ("Loup", () => ListePlat(StrategieCarnivore)),
                new ("Daim", () => ListePlat(StrategieVegetarien)),
                new ("Ours", () => ListePlat(StrategieGrossePortion)),
                new ("Emeu", () => ListePlat(StrategieSnob))
            };

            foreach (Ogre o in ogres)
            {
                o.Dormir = Thread.Sleep;
                o.AddObservateur(Update);

                Task task = new(o.Demarrer);
                task.ContinueWith( // Gestion des exception si une tâche échoue
                    (t) => Console.WriteLine(t.Exception),
                    TaskContinuationOptions.OnlyOnFaulted);
                task.Start();
            }
            Update();
            Console.Read();
        }

        public Plat ListePlat(Func<IQueryable<Plat>, Plat> strategieChoixPlat)
        {
            Plat p = null;
            lock (verrouChoixPlat) // Verrou partagé avec tous les ogres
            {
                try
                {
                    p = strategieChoixPlat(contexte.Plats.Include(p => p.TypePlat));
                    if (p != null)
                    {
                        contexte.Plats.Remove(p);
                        contexte.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("BOOM" + e);
                    p = null;
                }
            }
            return p;
        }

        public void Update()
        {
            lock (verrouAffichage)
            {
                Console.Clear();
                ogres.ForEach(o => Console.WriteLine(o));
                BuffetBDContext contexte = new();
                Console.WriteLine("\nTable :");
                foreach (Plat p in (contexte.Plats.Include(p => p.TypePlat)))
                    Console.WriteLine(p);
            }
        }

        

        public static void Main(string[] _)
        {
            Console.WriteLine("Console des ogres");
            TableOgres p = new();
            p.Start();
        }
    }
}
