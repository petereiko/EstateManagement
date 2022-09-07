using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstateManagement.WebService.Models
{
    public class CreateEstateRequest
    {
        public string Name { get; set; }
        public int stateId { get; set; }
        public int lgaId { get; set; }
    }
}