using System;
using System.Collections.Generic;
using System.Linq;

public class ProgressData
{
    public int RecruitsData { get; private set; }
    public List<double> InfantryUnitsData { get; private set; }
    public List<double> CavalryUnitsData { get; private set; }
    public List<double> DistantUnitsData { get; private set; }

    public int Gold { get; private set; }
    public int Piety { get; private set; }
    public string Position { get; private set; }

    public ProgressData(Player player)
    {
        RecruitsData = player._recruitsCount;
        InfantryUnitsData = player.infantryOutside.Select(x => x.GlobalHealth).ToList();
        CavalryUnitsData = player.cavalryOutside.Select(x => x.GlobalHealth).ToList();
        DistantUnitsData = player.rangedUnitsOutside.Select(x => x.GlobalHealth).ToList();
        Gold = player.Gold;
        Piety = player.Piety;
        Position = player.position.name;
    }

    public ProgressData(int recruitsData, List<double> infantryUnitsData, 
        List<double> cavalryUnitsData, List<double> distantUnitsData, 
        int gold, int piety, string position)
    {
        RecruitsData = recruitsData;
        InfantryUnitsData = infantryUnitsData;
        CavalryUnitsData = cavalryUnitsData;
        DistantUnitsData = distantUnitsData;
        Gold = gold;
        Piety = piety;
        Position = position;
    }
}