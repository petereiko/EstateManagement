using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstateManagement.WebService.Models
{
    public class LgaModel
    {
        public int Id { get; set; }
        public string LgaName { get; set; }
        public int StateId { get; set; }
    }
}