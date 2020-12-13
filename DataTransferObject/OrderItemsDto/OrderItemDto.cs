using DataTransferObject.SimpleDto;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.OrderItemsDto
{
    public class OrderItemDto
    {
        public long Id { get; set; }
        public long OrderUserId { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
        public CategoryDto Category { get; set; }
        public TypologyModel TypologyModel { get; set; }
        public ColorDto Color { get; set; }
        public string ColorString { get; set; }
        public SideChecker Side { get; set; }
        public QualityDto Quality { get; set; }
        public Guide Guide { get; set; }
        public Tabakera Tabakera { get; set; }
        public SeriesDto Series { get; set; }
        public GlassPackageDto GlassPackage { get; set; }
        public GlassQuality GlassQuality { get; set; }
        public bool Insert { get; set; }
        public bool Delete { get; set; }
        public bool Update { get; set; }
    }
}
