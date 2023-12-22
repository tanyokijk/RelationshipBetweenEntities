namespace Model;

using System.Numerics;
using System.Text.RegularExpressions;
using static System.Formats.Asn1.AsnWriter;
public class Team
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string City { get; set; }

    public int Wins { get; set; }

    public int Defeats { get; set; }

    public int Draws { get; set; }

    public int ScoredGoals { get; set; }

    public int MissedGoals { get; set; }

    public List<Player> Players { get; set; } = new();

    public List<Match> Matches { get; set; } = new();
}