using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldCupApplication.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        [Required]
        [Display(Name = "Team Name")]
        public string TeamName { get; set; }

        public List<Player> Players { get; set; }

        public Continent Continent { get; set; }

        [NotMapped]
        [Display(Name = "Continent")]
        public int SelectedContinentId { get; set; }

        public List<Match> Matches { get; set; }
    }
}