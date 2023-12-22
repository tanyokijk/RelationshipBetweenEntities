namespace Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Player
{

    public int Id { get; set; }

    public string Name { get; set; }

    public string Country { get; set; }

    public string Position { get; set; }

    public int Number { get; set; }

    public int? TeamId { get; set; }

    public Team? Team { get; set; }

    public List<Goal> Goals { get; set; } = new();
}
