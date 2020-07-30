using Library_Managment_System.Dtos;
using Library_Managment_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Managment_System.ViewModels
{
    public class UserRegisterFormVM
    {
        public List<MembershipType> MembershipTypes { get; set; }
        public UserDto user { get; set; }
    }
}