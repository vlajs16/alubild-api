using DataAccessLibrary;
using Domain;
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
    public class GlassPackageLogic : Repository<GlassPackage, AlubildContext>,
        IGlassPackageLogic
    {
        public GlassPackageLogic(AlubildContext context)
            : base(context)
        {

        }
    }
}
