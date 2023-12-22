using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Match
    {
        public int Id { get; set; }

        public List<Team> Teams { get; set; } = new List<Team>(); //я пробувала зробити через O:M але нічого не вийшло :( 

        public int CountScoredTeam1 { get; set; }

        public int CountScoredTeam2 { get; set; }

        public DateOnly Date { get; set; }

        public List<Goal> Goals { get; set; } = new();
    }
}
