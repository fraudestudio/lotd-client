using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ProceduralGeneration.Objets
{
    public interface IGeneratorAlgo
    {

        /// <summary>
        /// Renvoie l'objet en fonction d'un type de salle
        /// </summary>
        /// <param name="salle"></param>
        /// <returns>Le gameobject souhaité</returns>
        GameObject GetObjectSalle(TypeSalle salle);
    }
}
