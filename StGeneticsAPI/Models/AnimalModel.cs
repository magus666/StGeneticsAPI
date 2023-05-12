using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StGeneticsAPI.Models
{
    public class AnimalModel
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}