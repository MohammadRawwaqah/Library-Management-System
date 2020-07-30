using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Managment_System.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public byte DiscountRate { get; set; }

        
        
    }
}