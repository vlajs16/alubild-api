﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TypologyModelCategory> TypologyModelCategories { get; set; }
    }
}
