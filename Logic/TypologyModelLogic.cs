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
    public class TypologyModelLogic : ITypologyModelLogic
    {
        private readonly AlubildContext _context;

        public TypologyModelLogic(AlubildContext context)
        {
            _context = context;
        }

        public async Task<PagedList<TypologyModel>> Get(TypologyModelParams modelParams)
        {
            try
            {
                var typology = await _context.Typologies
                    .FirstOrDefaultAsync(x => x.Id == modelParams.TypologyId);

                if (typology == null)
                    return null;

                var typologyModels = _context.TypologyModels
                    .Include(x => x.TypologyModelCategories)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.Typology)
                    .Where(x => x.TypologyId == modelParams.TypologyId)
                    .AsQueryable();

                if(modelParams.CategoryId != null)
                {
                    var category = await _context.Categories
                        .FirstOrDefaultAsync(x => x.Id == modelParams.CategoryId);

                    if (category == null)
                        return null;

                    typologyModels = typologyModels
                        .Where(x => x.TypologyModelCategories
                        .Any(p => p.CategoryId == modelParams.CategoryId));

                }

                var pagedList = await PagedList<TypologyModel>.CreateAsync(typologyModels, 
                    modelParams.PageNumber, modelParams.PageSize);

                return pagedList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>> " + ex.Message);
                return null;
            }
        }

        public async Task<TypologyModel> GetById(long id, long typologyId)
        {
            try
            {
                var typology = await _context.Typologies.FirstOrDefaultAsync(x => x.Id == typologyId);
                if (typology == null)
                    return null;

                var typologyModel = await _context.TypologyModels
                    .Where(x => x.TypologyId == typologyId)
                    .FirstOrDefaultAsync(x => x.Id == id);

                return typologyModel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>> " + ex.Message);
                return null;
            }
        }
    }
}
