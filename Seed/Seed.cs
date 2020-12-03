using DataAccessLibrary;
using Domain;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeedData
{
    public class Seed
    {
        public static void SeedRoles(RoleManager<Role> rolesManager)
        {
            if (!rolesManager.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role { Name = "Client"},
                    new Role { Name = "Administrator"},
                    new Role { Name = "Developper"}
                };

                foreach (var role in roles)
                {
                    rolesManager.CreateAsync(role).Wait();
                }
            }
        }

        public static void SeedCategories(AlubildContext context)
        {
            if (!context.Categories.Any())
            {
                var categoryData = System.IO.File.ReadAllText("../Seed/Data/Categories.json");
                var categories = JsonConvert.DeserializeObject<ICollection<Category>>(categoryData);

                foreach (var category in categories)
                {
                    context.Categories.Add(category);
                }

                context.SaveChanges();
            }
        }

        public static void SeedColors(AlubildContext context)
        {
            if (!context.Colors.Any())
            {
                var colorsData = System.IO.File.ReadAllText("../Seed/Data/Colors.json");
                var colors = JsonConvert.DeserializeObject<ICollection<Color>>(colorsData);

                foreach (var color in colors)
                {
                    color.Category = context.Categories.FirstOrDefault(x => x.Name == color.Category.Name);
                    context.Colors.Add(color);
                }

                context.SaveChanges();
            }
        }

        public static void SeedManufacturers(AlubildContext context)
        {
            if (!context.Manufacturers.Any() && !context.Series.Any())
            {
                var manufacturersData = System.IO.File.ReadAllText("../Seed/Data/Manufacturers.json");
                var manufacturers = JsonConvert.DeserializeObject<ICollection<Manufacturer>>(manufacturersData);

                foreach (var manufacturer in manufacturers)
                {
                    manufacturer.Category = context.Categories.FirstOrDefault(x => x.Name == manufacturer.Category.Name);
                    context.Manufacturers.Add(manufacturer);
                }

                context.SaveChanges();
            }
        }

        public static void SeedQualities(AlubildContext context)
        {
            if (!context.Qualities.Any())
            {
                var qualitiesData = System.IO.File.ReadAllText("../Seed/Data/Qualities.json");
                var qualities = JsonConvert.DeserializeObject<ICollection<Quality>>(qualitiesData);

                foreach (var quality in qualities)
                {
                    quality.Category = context.Categories.FirstOrDefault(x => x.Name == quality.Category.Name);
                    context.Qualities.Add(quality);
                }

                context.SaveChanges();
            }
        }

        public static void SeedGlassQualities(AlubildContext context)
        {
            if (!context.GlassQualities.Any())
            {
                var qualitiesData = System.IO.File.ReadAllText("../Seed/Data/GlassQualities.json");
                var qualities = JsonConvert.DeserializeObject<ICollection<GlassQuality>>(qualitiesData);

                foreach (var quality in qualities)
                {
                    context.GlassQualities.Add(quality);
                }

                context.SaveChanges();
            }
        }

        public static void SeedGuides(AlubildContext context)
        {
            if (!context.Guides.Any())
            {
                var guidesData = System.IO.File.ReadAllText("../Seed/Data/Guides.json");
                var guides = JsonConvert.DeserializeObject<ICollection<Guide>>(guidesData);

                foreach (var guide in guides)
                {
                    context.Guides.Add(guide);
                }

                context.SaveChanges();
            }
        }

        public static void SeedTabakera(AlubildContext context)
        {
            if (!context.Tabakeras.Any())
            {
                var tabakereData = System.IO.File.ReadAllText("../Seed/Data/Tabakera.json");
                var tabakere = JsonConvert.DeserializeObject<ICollection<Tabakera>>(tabakereData);

                foreach (var tabakera in tabakere)
                {
                    context.Tabakeras.Add(tabakera);
                }

                context.SaveChanges();
            }
        }

        public static void SeedTypologies(AlubildContext context)
        {
            if (!context.Typologies.Any())
            {
                var typologiesData = System.IO.File.ReadAllText("../Seed/Data/Typologies.json");
                var typologies = JsonConvert.DeserializeObject<ICollection<Typology>>(typologiesData);

                foreach (var typology in typologies)
                {
                    context.Typologies.Add(typology);
                }

                context.SaveChanges();
            }
        }

        public static void SeedTypologyModels(AlubildContext context)
        {
            if (!context.TypologyModels.Any())
            {
                var modelsData = System.IO.File.ReadAllText("../Seed/Data/TypologyModels.json");
                var models = JsonConvert.DeserializeObject<ICollection<TypologyModel>>(modelsData);

                foreach (var model in models)
                {
                    model.Typology = context.Typologies
                        .FirstOrDefault(x => x.Name == model.Typology.Name);

                    foreach (var modelCat in model.TypologyModelCategories)
                    {
                        modelCat.Category = context.Categories
                            .FirstOrDefault(x => x.Name == modelCat.Category.Name);
                    }


                    context.TypologyModels.Add(model);
                }

                context.SaveChanges();
            }
        }
    }
}
