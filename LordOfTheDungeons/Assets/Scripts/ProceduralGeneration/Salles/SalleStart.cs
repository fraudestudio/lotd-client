﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ProceduralGeneration.Salles
{
    public class SalleStart : Salle
    {
        public SalleStart(int ligne, int colonne) : base(ligne, colonne)
        {
        }

        public override TypeSalle Type => TypeSalle.START;
        public override bool EstVide => true;
    }
}
