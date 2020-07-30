using Library_Managment_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Managment_System.Dtos
{
    public class NewRentalDto
    {
        public int? UserId { get; set; }
        public List<int?> BooksIds { get; set; }



    }
}