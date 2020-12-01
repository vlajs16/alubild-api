using DataAccessLibrary;
using Domain;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class CategoryLogic : Repository<Category, AlubildContext>,
        ICategoryLogic
    {
        public CategoryLogic(AlubildContext context)
            : base(context)
        {

        }
    }
}
