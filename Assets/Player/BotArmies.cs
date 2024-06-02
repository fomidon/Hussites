using System.Collections.Generic;

class BotArmies
{
    public static List<IUniversalSoldier> ArmyByRequirement(int infantryCount, int cavalryCount = 0, 
        int crossbowMenCount = 0)
    {
        var finishArmy = new List<IUniversalSoldier>();
        for (var i = 0; i < infantryCount; i++)
        {
            finishArmy.Add(new UniversalSoldier(UnitsInit.InitInfantrySoldiers()));
        }

        for (var i = 0; i < cavalryCount; i++)
        {
            finishArmy.Add(new UniversalSoldier(UnitsInit.InitCavalrySoldiers()));
        }

        for (var i = 0; i < crossbowMenCount; i++)
        {
            finishArmy.Add(new UniversalSoldier(UnitsInit.InitCrossbowSoldiers()));
        }
        return finishArmy;
    }

    public static int RevardByRequirement(int infantryCount, int cavalryCount, int crossbowMenCount = 0)
    {
        return infantryCount * ArmyCosts.InfantryTrophy + cavalryCount * ArmyCosts.CavalryTrophy + 
            crossbowMenCount * ArmyCosts.CrossbowMenTrophy;
    }

    public static List<IUniversalSoldier> StandartEnemyArmy { get => ArmyByRequirement(2, 0, 0); }

    public static int StandartRevard { get => RevardByRequirement(2, 0, 0); }

    public static List<IUniversalSoldier> BigEnemyArmy { get => ArmyByRequirement(3, 1, 1); }

    public static int BigRevard { get => RevardByRequirement(3, 1, 1); }

    public static List<IUniversalSoldier> TrainingArmy { get => ArmyByRequirement(1, 0, 0); }

    public static int TrainingRevard { get => RevardByRequirement(1, 0, 0); }


}