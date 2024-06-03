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

    public static List<IUniversalSoldier> SecondCrusadeArmy { get => ArmyByRequirement(3, 1, 0); }

    public static int SecondCrusadeRevard { get => RevardByRequirement(3, 1, 0); }

    public static List<IUniversalSoldier> SilesianArmy { get => ArmyByRequirement(0, 4, 0); }

    public static int SilesianRevard { get => RevardByRequirement(0, 4, 0); }

    public static List<IUniversalSoldier> SaxonianArmy { get => ArmyByRequirement(5, 0, 2); }

    public static int SaxonianRevard { get => RevardByRequirement(5, 0, 2); }

    public static List<IUniversalSoldier> ThirdCrusadeArmy { get => ArmyByRequirement(4, 2, 2); }

    public static int ThirdCrusadeRevard { get => RevardByRequirement(4, 2, 2); }

    public static List<IUniversalSoldier> ModerateArmy
    {
        get 
            {
            var finishArmy = new List<IUniversalSoldier>();
            for (var i = 0; i < 10; i++)
            {
                var unit = UnitsInit.InitInfantrySoldiers();
                unit.SetModificators(1, 0.6);
                finishArmy.Add(new UniversalSoldier(unit));
            }

            for (var i = 0; i < 5; i++)
            {
                var unit = UnitsInit.InitCavalrySoldiers();
                unit.SetModificators(1, 0.6);
                finishArmy.Add(new UniversalSoldier(unit));
            }

            for (var i = 0; i < 5; i++)
            {
                var unit = UnitsInit.InitCrossbowSoldiers();
                unit.SetModificators(1, 0.6);
                finishArmy.Add(new UniversalSoldier(unit));
            }
            return finishArmy;
        } }

    public static int ModerateRevard { get => RevardByRequirement(10, 5, 5); }

    public static List<IUniversalSoldier> TaborArmy
    {
        get
        {
            var finishArmy = new List<IUniversalSoldier>();
            for (var i = 0; i < 8; i++)
            {
                var unit = UnitsInit.InitInfantrySoldiers();
                unit.SetModificators(1.15, 0.51);
                finishArmy.Add(new UniversalSoldier(unit));
            }

            for (var i = 0; i < 3; i++)
            {
                var unit = UnitsInit.InitCavalrySoldiers();
                unit.SetModificators(1.15, 0.51);
                finishArmy.Add(new UniversalSoldier(unit));
            }

            for (var i = 0; i < 3; i++)
            {
                var unit = UnitsInit.InitCrossbowSoldiers();
                unit.SetModificators(1.15, 0.51);
                finishArmy.Add(new UniversalSoldier(unit));
            }
            return finishArmy;
        }
    }

    public static int TaborRevard { get => RevardByRequirement(8, 3, 3); }
}