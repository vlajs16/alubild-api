using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GlassPackageTypology
    {
        public GlassPackage GlassPackage { get; set; }
        public long GlassPackageId { get; set; }
        public Typology Typology { get; set; }
        public long TypologyId { get; set; }
    }
}
