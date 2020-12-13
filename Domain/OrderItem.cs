using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OrderItem
    {
        public long Id { get; set; }
        public Order Order { get; set; }
        public long OrderId { get; set; }
        public long OrderUserId { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
        public Category Category { get; set; }
        public TypologyModel TypologyModel { get; set; }
        public Color Color { get; set; }
        public string ColorString { get; set; }
        public SideChecker Side { get; set; }
        public Quality Quality { get; set; }
        public Guide Guide { get; set; }
        public Tabakera Tabakera { get; set; }
        public GlassQuality GlassQuality { get; set; }
        public GlassPackage GlassPackage { get; set; }
        public Series Series { get; set; }
        public bool Delete { get; set; } = false;
        public bool Update { get; set; } = false;
        public bool Insert { get; set; } = false;

    }
}
