using DataAccessLibrary;
using Domain;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class GlassQualityLogic : Repository<GlassQuality, AlubildContext>,
        IGlassQualityLogic
    {
        public GlassQualityLogic(AlubildContext context)
            : base(context)
        {

        }
    }
}
