using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorldCupApplication.Models
{
    public class Continent
    {
        public int ContinentId { get; set; }

        [Required]
        [Display(Name = "Continent Name")]
        public string ContinentName { get; set; }

        public List<Team> Teams { get; set; }
    }
}