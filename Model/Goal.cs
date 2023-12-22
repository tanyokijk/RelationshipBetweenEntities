using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model
{
    public class Goal
    {
        public int Id { get; set; }

        public int? PlayerId { get; set; }

        public Player? Player { get; set; }

        public int? MatchId { get; set; }

        public Match? Match { get; set; }

        public int Minute { get; set; }
    }
}
