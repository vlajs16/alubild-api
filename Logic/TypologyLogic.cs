using DataAccessLibrary;
using Domain;
using Helpers;
using Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class TypologyLogic : Repository<Typology, AlubildContext>,
        ITypologyLogic
    {
        public TypologyLogic(AlubildContext context)
            : base(context)
        {

        }
    }
}
