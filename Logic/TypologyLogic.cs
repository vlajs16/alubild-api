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
    public class TypologyLogic : ITypologyLogic
    {
        private readonly AlubildContext _context;

        public TypologyLogic(AlubildContext context)
        {
            _context = context;
        }


        public async Task<PagedList<Typology>> GetTypologies(TypologyParams typologyParams)
        {
            try
            {
                var typologyFromDb = _context.Typologies.AsQueryable();

                if (typologyParams.CategoryId != null)
                    typologyFromDb = typologyFromDb.Where(x =>
                    x.TypologyCategories.Any(p => p.CategoryId == typologyParams.CategoryId));

                if (!string.IsNullOrWhiteSpace(typologyParams.Name))
                    typologyFromDb = typologyFromDb.Where(x => x.Name == typologyParams.Name);


                var pagedList = await PagedList<Typology>
                    .CreateAsync(typologyFromDb, typologyParams.PageNumber, typologyParams.PageSize);
                return pagedList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>" + ex.Message);
                return null;
            }
        }

        public async Task<Typology> GetTypology(long id)
        {
            try
            {
                var typology = await _context.Typologies.FirstOrDefaultAsync(x => x.Id == id);
                return typology;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>" + ex.Message);
                return null;
            }
        }

        public async Task<ICollection<string>> GetTypologyNames(int categoryId)
        {
            try
            {
                var names = await _context.Typologies
                    .Where(x => x.TypologyCategories.Any(x => x.CategoryId == categoryId))
                    .Select(x => x.Name).Distinct().ToListAsync();
                return names;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>" + ex.Message);
                return null;
            }
        }
    }
}
