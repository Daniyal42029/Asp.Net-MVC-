using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class DbActionResult
    {
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public bool Action { get; set; }
        public object Value { get; set; }
    }
}