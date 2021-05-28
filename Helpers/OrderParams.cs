﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    public class OrderParams
    {
        private const int MaxPageSize = 20;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 50;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public long UserId { get; set; }

    }
}
