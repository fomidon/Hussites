using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class AutoBattleCalculation
{
    public const int DistantPhaseLength = 5;
    public const double DistanceSupportInMeleeCoefficient = 1;
    public const double FrontWidth = 250;

    public static (IDistantSoldier, IDistantSoldier) SingleDistantDamageExchange(
        IDistantSoldier firstSoldier, IDistantSoldier secondSoldier)
    {
        var firstDamage = firstSoldier.Damage;
        var secondDamage = secondSoldier.Damage;
        if (firstSoldier.CurrentAmmunition > 0)
        {
            secondSoldier.GetDamage(firstDamage);
            firstSoldier.Volley();
        }
        if (secondSoldier.CurrentAmmunition > 0)
        {
            firstSoldier.GetDamage(secondDamage);
            secondSoldier.Volley();
        }
        return (firstSoldier, secondSoldier);
    }

    public static (List<IDistantSoldier>, List<IDistantSoldier>) DistantPhase(
        List<IDistantSoldier> firstArmy, List<IDistantSoldier> secondArmy)
    {
        var firstArmySize = firstArmy.Count;
        var secondArmySize = secondArmy.Count;

        for (var i = 0; i < Math.Max(firstArmySize, secondArmySize); i++)
        {
            for (var j = 0; j < DistantPhaseLength; j++)
            {
                (firstArmy[i % firstArmySize], secondArmy[i % secondArmySize]) =
                    SingleDistantDamageExchange(firstArmy[i % firstArmySize], secondArmy[i % secondArmySize]);
            }
        }
        return (firstArmy, secondArmy);
    }

    public static void GetDamageFromDistance(this IMeleeSoldier meleeUnit, List<IDistantSoldier> distanceUnits)
    {
        foreach(var unit in distanceUnits)
        {
            if (unit.CurrentAmmunition > 0)
            {
                meleeUnit.GetDamage(unit.Damage * DistanceSupportInMeleeCoefficient);
                unit.Volley();
            }
        }
    }

    private static double GetUnitDamage(IMeleeSoldier unit)
    {
        if (unit.NumberOfPeople > FrontWidth)
        {
            return unit.Damage * FrontWidth / unit.NumberOfPeople;
        }
        return unit.Damage;
    }

    public static (IMeleeSoldier, IMeleeSoldier) MeleeEpisode(IMeleeSoldier firstUnit, IMeleeSoldier secondUnit, 
        List<IDistantSoldier> firstDistanceSupport, List<IDistantSoldier> secondDistanceSupport)
    {
        var firstUnitDamage = firstUnit.ChargeCoefficient * firstUnit.Damage * (1 - secondUnit.ChargeResistance);
        var secondUnitDamage = secondUnit.ChargeCoefficient * secondUnit.Damage * (1 - firstUnit.ChargeResistance);
        firstUnit.GetDamage(secondUnitDamage);
        secondUnit.GetDamage(firstUnitDamage);

        while (Math.Min(firstUnit.Health, secondUnit.Health) > 0)
        {
            firstUnit.GetDamageFromDistance(secondDistanceSupport);
            secondUnit.GetDamageFromDistance(firstDistanceSupport);
            (firstUnitDamage, secondUnitDamage) = (GetUnitDamage(firstUnit), GetUnitDamage(secondUnit));
            firstUnit.GetDamage(secondUnitDamage);
            secondUnit.GetDamage(firstUnitDamage);
        }
        return (firstUnit, secondUnit);
    }

    public static (List<IMeleeSoldier>, List<IMeleeSoldier>, bool resultFlag) MeleePhase(
        List<IMeleeSoldier> firstMeleeArmy, 
        List<IDistantSoldier> firstDistantArmy, List<IMeleeSoldier> secondMeleeArmy, 
        List<IDistantSoldier> secondDistantArmy)
    {
        bool resultFlag = true;
        while (true)
        {
            firstMeleeArmy = firstMeleeArmy.OrderBy(x => x.Health).ToList();
            secondMeleeArmy = secondMeleeArmy.OrderBy(x => x.Health).ToList();
            if (Math.Min(firstMeleeArmy[^1].Health, secondMeleeArmy[^1].Health) <= 1e-9)
            {
                resultFlag = secondMeleeArmy[^1].Health <= 1e-9;
                break;
            }
            (firstMeleeArmy[^1], secondMeleeArmy[^1]) = MeleeEpisode(firstMeleeArmy[^1], secondMeleeArmy[^1],
                firstDistantArmy, secondDistantArmy);
        }
        return (firstMeleeArmy, secondMeleeArmy, resultFlag);
    }

    private static List<UniversalSoldier> CalculateOneArmyLosses(
        List<IDistantSoldier> distanceArmy, List<IMeleeSoldier> meleeArmy)
    {
        var army = new List<UniversalSoldier>();

        foreach (var unit in distanceArmy)
        {
            unit.CalculateLosses();
            army.Add(new UniversalSoldier(unit));
        }
        foreach (var unit in meleeArmy)
        {
            unit.CalculateLosses();
            army.Add(new UniversalSoldier(unit));
        }
        return army;
    }

    public static (List<IUniversalSoldier>, List<IUniversalSoldier>) CalculateFullLosses(
        List<IDistantSoldier> firstDistanceArmy, List<IDistantSoldier> secondDistanceArmy,
        List<IMeleeSoldier> firstMeleeArmy, List<IMeleeSoldier> secondMeleeArmy)
    {
        var firstArmy = CalculateOneArmyLosses(firstDistanceArmy, firstMeleeArmy);
        var secondArmy = CalculateOneArmyLosses(secondDistanceArmy, secondMeleeArmy);

        return (firstArmy.Select(x => (IUniversalSoldier)x).ToList(), 
                secondArmy.Select(x => (IUniversalSoldier)x).ToList());
    }

    private static List<IDistantSoldier> BuildDistanceArmy(List<IUniversalSoldier> baseArmy)
    {
        return baseArmy.Where(x => !x.meleeFlag).Select(x => 
                                          {
                                              x.TryToDistant(out var unit);
                                              return unit;
                                          }
                                          ).ToList();
    }

    private static List<IMeleeSoldier> BuildMeleeArmy(List<IUniversalSoldier> baseArmy) 
    {
        return baseArmy.Where(x => x.meleeFlag).Select(x =>
                                            {
                                                x.TryToMelee(out var unit);
                                                return unit;
                                            }
                                          ).ToList();
    }

    public static (List<IUniversalSoldier>, List<IUniversalSoldier>, bool resultFlag) AutoBattleCalculate(
        List<IUniversalSoldier> firstArmy, List<IUniversalSoldier> secondArmy)
    {
        var firstDistanceArmy = BuildDistanceArmy(firstArmy);

        var secondDistanceArmy = BuildDistanceArmy(secondArmy);

        if (firstDistanceArmy.Count * secondDistanceArmy.Count > 0)
            (firstDistanceArmy, secondDistanceArmy) = DistantPhase(firstDistanceArmy,
                                                                   secondDistanceArmy);
        
        var firstMeleeArmy = BuildMeleeArmy(firstArmy);
        var secondMeleeArmy = BuildMeleeArmy(secondArmy);
        bool resultFlag;
        (firstMeleeArmy, secondMeleeArmy, resultFlag) = MeleePhase(firstMeleeArmy, firstDistanceArmy, 
            secondMeleeArmy, secondDistanceArmy);

        (firstArmy, secondArmy) = CalculateFullLosses(firstDistanceArmy, secondDistanceArmy,
            firstMeleeArmy, secondMeleeArmy);
        return (firstArmy, secondArmy, resultFlag);
    }
}