using System;
using System.Collections.Generic;
using System.Linq;

public class ProgressData
{
    public int RecruitsData { get; set; }
    public List<double> InfantryUnitsData { get; set; }
    public List<double> CavalryUnitsData { get; set; }
    public List<double> DistantUnitsData { get; set; }

    public int Gold { get; set; }
    public int Piety { get; set; }
    public string Position { get; set; }

    public double DamageModifier { get; set; }
    public double DamageResistanceModifier { get; set; }

    public ProgressData()
    {

    }

    public ProgressData(Player player)
    {
        RecruitsData = player.army._recruitsCount;
        InfantryUnitsData = player.army.infantryOutside.Select(x => x.GlobalHealth).ToList();
        CavalryUnitsData = player.army.cavalryOutside.Select(x => x.GlobalHealth).ToList();
        DistantUnitsData = player.army.rangedUnitsOutside.Select(x => x.GlobalHealth).ToList();
        Gold = player.Gold;
        Piety = player.Piety;
        Position = player.position.name;
        DamageModifier = player.army.DamageModifier;
        DamageResistanceModifier = player.army.DamageResistanceModifier;
    }

    public ProgressData(int recruitsData, List<double> infantryUnitsData, 
        List<double> cavalryUnitsData, List<double> distantUnitsData, 
        int gold, int piety, string position, double DamageModifier, double DamageResistanceModifier)
    {
        RecruitsData = recruitsData;
        InfantryUnitsData = infantryUnitsData;
        CavalryUnitsData = cavalryUnitsData;
        DistantUnitsData = distantUnitsData;
        Gold = gold;
        Piety = piety;
        Position = position;
        this.DamageModifier = DamageModifier;
        this.DamageResistanceModifier = DamageResistanceModifier;
    }
}