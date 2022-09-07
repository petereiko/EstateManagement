using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstateManagement.WebService.Models
{
    public class CreateEstateResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public int Id { get; set; }
    }
}