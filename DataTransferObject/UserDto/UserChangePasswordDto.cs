﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.UserDto
{
    public class UserChangePasswordDto
    {
        public long Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
