using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldCupApplication.Models
{
    public class Match
    {
        public int MatchId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Match Date")]
        public DateTime MatchDateTime { get; set; }


        public Team TeamA { get; set; }

        [NotMapped]
        [Display(Name = "Team A")]
        public int SelectedTeamAId { get; set; }

        public Team TeamB { get; set; }

        [NotMapped]
        [Display(Name = "Team B")]
        public int SelectedTeamBId { get; set; }
    }
}