using DataAccessLibrary;
using Domain;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class TabakeraLogic : Repository<Tabakera, AlubildContext>,
        ITabakeraLogic
    {
        public TabakeraLogic(AlubildContext context) 
            : base(context)
        {

        }
    }
}
