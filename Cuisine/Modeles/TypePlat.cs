using System;
using System.Collections.Generic;

namespace Cuisine.Modeles;

public partial class TypePlat
{
    public int TypePlatId { get; set; }

    public string Nom { get; set; }

    public int Probabilite { get; set; }

    public virtual ICollection<Plat> Plats { get; } = new List<Plat>();
}
