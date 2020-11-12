using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GlassPackage
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<GlassPackageTypology> GlassPackageTypologies { get; set; }
    }
}
