using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class UserLog
    {
        public DateTime LogDateTime { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }

    }
}
