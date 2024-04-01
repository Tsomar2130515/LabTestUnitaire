using System;
using System.Collections.Generic;

#nullable disable

namespace Ogres.Models
{
    public partial class Plat
    {
        public int PlatId { get; set; }
        public DateTime DateCreation { get; set; }
        public int Taille { get; set; }

        public virtual TypePlat TypePlat { get; set; }
        public override string ToString()
        {
            return $"[{PlatId},{DateCreation},{Taille},{TypePlat?.Nom}]";
        }
    }
}
