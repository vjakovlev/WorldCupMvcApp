using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldCupApplication.Models
{
    public class Player
    {
        public int PlayerId { get; set; }

        [Required]
        [Display(Name = "Player Name")]
        public string PlayerName { get; set; }

        [Required]
        public int Age { get; set; }

        public Team Team { get; set; }

        [NotMapped]
        [Display(Name = "Team")]
        public int SelectedTeamId { get; set; }

        public Position Position { get; set; }

        [NotMapped]
        [Display(Name = "Position")]
        public int SelectedPositionId { get; set; }
    }
}