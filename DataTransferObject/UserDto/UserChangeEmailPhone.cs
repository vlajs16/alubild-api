using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.UserDto
{
    public class UserChangeEmailPhone
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
