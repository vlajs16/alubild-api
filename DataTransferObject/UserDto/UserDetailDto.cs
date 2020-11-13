using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.UserDto
{
    public class UserDetailDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
