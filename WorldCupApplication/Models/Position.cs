using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorldCupApplication.Models
{
    public class Position
    {
        public int PositionId { get; set; }

        [Required]
        [Display(Name = "Player Position")]
        public string PlayerPosition { get; set; }

        public List<Player> Players { get; set; }
    }
}