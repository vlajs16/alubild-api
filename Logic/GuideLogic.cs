using DataAccessLibrary;
using Domain;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class GuideLogic : Repository<Guide, AlubildContext>,
        IGuideLogic
    {
        public GuideLogic(AlubildContext context)
            : base(context)
        {

        }
    }
}
