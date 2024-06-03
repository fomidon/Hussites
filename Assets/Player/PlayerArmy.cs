using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerArmy
{
    // Списки для хранения количества войск
    public const int MaxArmySize = 20;

    public int RecruitsCount { get; private set; } = 0;
    private List<MeleeSoldier> _infantryUnits = new();
    public List<MeleeSoldier> InfantryOutside { get => _infantryUnits.ToList(); }

    private List<MeleeSoldier> _cavalryUnits = new();
    public List<MeleeSoldier> CavalryOutside { get => _cavalryUnits.ToList(); }

    private List<DistantSoldier> _rangedUnits = new();
    public List<DistantSoldier> RangedUnitsOutside { get => _rangedUnits.ToList(); }

    public double DamageModifier { get; private set; } = 1;

    public double DamageResistanceModifier { get; set; } = 1;

    public double DamagePietyModifier { get; set; } = 1;

    public int armySize
    {
        get => RecruitsCount + _infantryUnits.Count +
            _cavalryUnits.Count + _rangedUnits.Count;
    }

    // Методы для найма и улучшения войск
    public bool CanHireRecruits(int amount)
    {
        return armySize + amount <= MaxArmySize;
    }

    public void HireRecruits(int amount)
    {
        if (!CanHireRecruits(amount))
        {
            Debug.LogWarning("Вы не можете нанимать новобранцев сверх лимита");
            return;
        }
        RecruitsCount += amount;
    }
    public bool CanTrainRecruits()
    {
        return RecruitsCount > 0;
    }

    public void TrainRecruit(string unitType)
    {
        if (!CanTrainRecruits())
        {
            Debug.LogWarning("Нет новобранцев для обучения");
            return;
        }
        RecruitsCount--;

        switch (unitType)
        {
            case "Пехота":
                _infantryUnits.Add(UnitsInit.InitInfantrySoldiers());
                break;
            case "Кавалерия":
                _cavalryUnits.Add(UnitsInit.InitCavalrySoldiers());
                break;
            case "Дальний бой":
                _rangedUnits.Add(UnitsInit.InitCrossbowSoldiers());
                break;
        }
    }

    // Конструкторы
    public PlayerArmy()
    {
        RecruitsCount = 0;
    }

    public PlayerArmy(int recruitsCount, List<MeleeSoldier> infantryUnits, 
        List<MeleeSoldier> cavalryUnits, List<DistantSoldier> rangedUnits)
    {
        RecruitsCount = recruitsCount;
        _infantryUnits = infantryUnits;
        _cavalryUnits = cavalryUnits;
        _rangedUnits = rangedUnits;
    }

    public PlayerArmy(ProgressData save)
    {
        RecruitsCount = save.RecruitsData;
        _infantryUnits = save.InfantryUnitsData.Select(x => UnitsInit.InitInfantrySoldiers(x)).ToList();
        _cavalryUnits = save.CavalryUnitsData.Select(x => UnitsInit.InitCavalrySoldiers(x)).ToList();
        _rangedUnits = save.DistantUnitsData.Select(x => UnitsInit.InitCrossbowSoldiers(x)).ToList();
        DamageModifier = save.DamageModifier;
        DamageResistanceModifier = save.DamageResistanceModifier;
    }

    // Интерфейс показа юнитов на экране
    public void UpdateUI()
    {
        UpdateRecruitsText();
        UpdateUnitText(_infantryUnits, "Пехота");
        UpdateUnitText(_cavalryUnits, "Кавалерия");
        UpdateUnitText(_rangedUnits, "Дальний бой");
    }

    private void UpdateRecruitsText()
    {
        var unitTextMesh = GameObject.Find($"Text (Новобранцы)").GetComponent<TextMeshProUGUI>();
        if (unitTextMesh != null)
            unitTextMesh.text = (RecruitsCount * 250).ToString();
    }

    private void UpdateUnitText<T>(List<T> Units, string unitType) where T : ISoldier
    {
        var unitTextMesh = GameObject.Find($"Text ({unitType})").GetComponent<TextMeshProUGUI>();
        if (unitTextMesh != null)
        {
            var soldiersNumber = Units.Select(x => x.NumberOfPeople).Sum();
            var soldiersMaxNumber = Units.Select(x => x.MaxPeopleNumber).Sum();
            unitTextMesh.text = soldiersNumber.ToString() + "/" + soldiersMaxNumber.ToString();
        }
    }

    public void GetDamageModifier(int valueInPersents)
    {
        DamageModifier *= 1 + valueInPersents / 100d;
    }

    public void GetDamageResistanceModifier(int valueInPersents)
    {
        DamageResistanceModifier *= 1 - valueInPersents / 100d;
    }

    public IUniversalSoldier PrepareUnit(DistantSoldier unit)
    {
        unit.SetModificators(DamageModifier * DamagePietyModifier, DamageResistanceModifier);
        return new UniversalSoldier(unit);
    }

    public IUniversalSoldier PrepareUnit(MeleeSoldier unit)
    {
        unit.SetModificators(DamageModifier * DamagePietyModifier, DamageResistanceModifier);
        return new UniversalSoldier(unit);
    }

    public List<IUniversalSoldier> GetArmyForBattle()
    {
        return _infantryUnits.Select(x => PrepareUnit(x))
            .Concat(_cavalryUnits.Select(x => PrepareUnit(x)))
            .Concat(_rangedUnits.Select(x => PrepareUnit(x))).ToList();
    }

    public void ModifyPiety(int piety)
    {
        if (piety <= 50)
        {
            DamagePietyModifier = 1;
        } else if (piety <= 80)
        {
            DamagePietyModifier = 1.15;
        } else
        {
            DamagePietyModifier = 1.3;
        }
    }

    public void GetArmyFromBattle(List<IUniversalSoldier> basicArmy)
    {
        _infantryUnits = basicArmy.Where(x => x.unitType == UnitsType.Infantry).Select(x => 
            {
                x.TryToMelee(out var finish);
                return UnitsInit.InitInfantrySoldiers(finish.Health);
            }).ToList();

        _cavalryUnits = basicArmy.Where(x => x.unitType == UnitsType.Cavalry).Select(x =>
            {
                x.TryToMelee(out var finish);
                return UnitsInit.InitCavalrySoldiers(finish.Health);
            }).ToList();

        _rangedUnits = basicArmy.Where(x => x.unitType == UnitsType.CrossbowMen).Select(x => 
            {
                x.TryToDistant(out var finish);
                return UnitsInit.InitCrossbowSoldiers(finish.Health);
            }).ToList();
    }

    //Стоимость армии
    public int ArmyMaintenance {  get => _infantryUnits.Count * ArmyCosts.InfantryMaintenance +
            _cavalryUnits.Count * ArmyCosts.CavalryMaintenance + 
            _rangedUnits.Count * ArmyCosts.CrossbowMenMaintenance; }
}