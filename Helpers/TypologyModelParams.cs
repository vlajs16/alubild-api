﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    public class TypologyModelParams
    {
        private const int _maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 50;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > _maxPageSize) ? _maxPageSize : value; }
        }

        public long TypologyId { get; set; }
        public int? CategoryId { get; set; }
    }
}
