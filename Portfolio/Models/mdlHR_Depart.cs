using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class mdlHR_Depart
    {
        public string ID { get; set; }
        public string FirstName {get; set;}

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string HireDate { get; set; }

        public string JobTitle { get; set; }

        public string Salary { get; set; }

        public string EmploymentStatus { get; set; }

        public string mode { get; set; }
    }
}