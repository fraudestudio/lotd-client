using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ProceduralGeneration
{
    public class Sommet
    {
        private TypeSalle typeSalle;
        public TypeSalle TypeSalle { get => typeSalle; set => typeSalle = value; }


        List<Sommet> voisins = new List<Sommet>();

        public List<Sommet> Voisins { get => voisins; }

        public Sommet(TypeSalle typeSalle = TypeSalle.NORMALE)
        {
            TypeSalle = typeSalle;
        }

        public void AjouterVoisin(Sommet s)
        {
            voisins.Add(s);
        }
    }
}
