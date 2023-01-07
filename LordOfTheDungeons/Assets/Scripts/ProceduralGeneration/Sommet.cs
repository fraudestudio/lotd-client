using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ProceduralGeneration
{
    public class Sommet
    {
        // type of the vertex
        private TypeSalle typeSalle;
        public TypeSalle TypeSalle { get => typeSalle; set => typeSalle = value; }


        // neighbors of the vertex
        List<Sommet> voisins = new List<Sommet>();

        public List<Sommet> Voisins { get => voisins; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeSalle"></param>
        public Sommet(TypeSalle typeSalle = TypeSalle.NORMALE)
        {
            TypeSalle = typeSalle;
        }

        /// <summary>
        /// add a vertex neighbor to the vertex
        /// </summary>
        /// <param name="s"></param>
        public void AjouterVoisin(Sommet s)
        {
            voisins.Add(s);
        }
    }
}
