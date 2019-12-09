using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SharePointAddIn1Web.Models
{
    [Serializable]
    public class CarModel
    {
        public int ID { get; set; }
        [Required]
        public string Brand { get; set; }
        public string Title { get; set; }
        public double? Price { get; set; }
        [Required]
        public string Seria { get; set; }

    }
}