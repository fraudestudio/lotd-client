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
        GameObject GetObjectSalle(TypeSalle salle);
    }
}
