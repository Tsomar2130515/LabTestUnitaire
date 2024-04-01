using System;
using System.Collections.Generic;

namespace Cuisine.Modeles;

public partial class Plat
{
    public int PlatId { get; set; }

    public DateTime DateCreation { get; set; }

    public int Taille { get; set; }

    public int? TypePlatId { get; set; }

    public virtual TypePlat TypePlat { get; set; }
}
