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

        // TODO: Ova mislam deka nigde ne go koristis, a vo baza ti kreira nepotrebna kolona vo Matches (Team_TeamId) koja e sekogas null
        public List<Match> Matches { get; set; }
    }
}