
using Cuisine.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Cuisine
{
    public class CuisineBuilder
    {
        private readonly Cuisine cuisine = new ();

        public Cuisine Build() {
            cuisine.Contexte ??= new BuffetBDContext();
            cuisine.Alea ??= new Randomiseur();
            cuisine.Dormir ??= Thread.Sleep;
            cuisine.Notifier ??= Console.WriteLine;
            cuisine.Maintenant ??= () => DateTime.Now;

            return cuisine;
        }

        public CuisineBuilder()
        {
            cuisine = new Cuisine();
        }
        
        public CuisineBuilder SetContext(BuffetBDContext dao)
        {
            cuisine.Contexte = dao;
            return this;
        }

        public CuisineBuilder SetDormir(Action<int> dormir)
        {
            cuisine.Dormir = dormir;
            return this;
        }
        public CuisineBuilder SetAlea(IRandomiseur alea)
        {
            cuisine.Alea = alea;
            return this;
        }

        public CuisineBuilder SetNotifier(Action<String> notifier)
        {
            cuisine.Notifier = notifier;
            return this;
        }


        public CuisineBuilder SetFournisseurMaintenant(Func<DateTime> maintenantFournisseur)
        {
            cuisine.Maintenant = maintenantFournisseur;
            return this;
        }
    }
}
