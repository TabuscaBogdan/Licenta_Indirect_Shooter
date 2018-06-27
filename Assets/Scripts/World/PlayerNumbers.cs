using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlayerNumbers
{
    public string name;
    public int lives;
    public int team;
    public int kills;

    public PlayerNumbers(string Name, int Lives, int Team, int Kills)
    {
        name = Name;
        lives = Lives;
        team = Team;
        kills = Kills;
    }
}
