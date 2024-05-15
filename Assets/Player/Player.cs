using UnityEngine;
using TMPro;
using System.Collections.Generic;
using JetBrains.Annotations;
using System;

public class Player : MonoBehaviour
{
    //Словари для создания юнитов по их русскоязычному названию
    public readonly Dictionary<string, Func<MeleeSoldier>> MeleeSoldiersInit = 
        new Dictionary<string, Func<MeleeSoldier>>
        {
            { "Новобранцы", SoldierUnitsInitialization.MakeRecruit },
            { "Пехота", SoldierUnitsInitialization.MakeLightInfantry },
            { "Кавалерия", SoldierUnitsInitialization.MakeLightCavalry },    
        };

    public readonly Dictionary<string, Func<DistantSoldier>> DistantSoldiersInit =
        new Dictionary<string, Func<DistantSoldier>>
        {
            { "Дальний бой", SoldierUnitsInitialization.MakeCrossbowMan }
        };

    // Позиция игрока на карте и последняя дружественная позиция
    public MapRegion position;
    public MapRegion lastFriendlyPosition; //{ get; private set; }
    public MapRegion startPosition;

    // Массивы с классами юнитов разных родов войск
    private readonly string[] _infarnyUnitTypes = { "Новобранцы", "Пехота"};
    private readonly string[] _cavarlyUnitTypes = { "Кавалерия" };
    private readonly string[] _rangedUnitTypes = { "Дальний бой" };
    
    // Свойства для хранения валюты
    public int Piety { get; private set; }
    public int Gold { get; private set; }

    // Поля для хранения войск
    public const int MaxArmySize = 20;
    private int _recruitsCount = 0;
    private readonly List<MeleeSoldier> _infantryUnits = new List<MeleeSoldier>();
    private readonly List<MeleeSoldier> _cavalryUnits = new List<MeleeSoldier>();
    private readonly List<DistantSoldier> _rangedUnits = new List<DistantSoldier>();
    private int armySize { get => _recruitsCount + _infantryUnits.Count + _cavalryUnits.Count + 
            _rangedUnits.Count; }

    // Ссылки на текст для отображения в интерфейсе
    public TextMeshProUGUI pietyText;
    public TextMeshProUGUI goldText;

    // Метод для установки начальных значений
    public void Initialize(int startingPiety, int startingGold)
    {
        Piety = startingPiety;
        Gold = startingGold;

        position = startPosition;

        UpdateUI();
    }
    
    private void InitializeUnitDictionary(Dictionary<string, int> unitDict, string[] unitTypes)
    {
        foreach (var unitType in unitTypes) unitDict.Add(unitType, 0);
    }

    // Метод для изменения благочестия
    public void ModifyPiety(int amount)
    {
        Piety += amount;
        UpdateUI();
    }

    // Метод для изменения золота
    public void ModifyGold(int amount)
    {
        Gold += amount;
        UpdateUI();
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
            throw new Exception("Вы не можете нанимать новобранцев сверх лимита");
        }
        _recruitsCount += amount;
    }
    public bool CanTrainRecruits()
    {
        return _recruitsCount > 0;
    }

    public void TrainRecruit(string unitType)
    {
        if (!CanTrainRecruits())
        {
            throw new Exception("Нет новобранцев для обучения");
        }
        _recruitsCount--;

        switch (unitType)
        {
            case "Пехота":
                _infantryUnits.Add(MeleeSoldiersInit["Пехота"]());
                break;
            case "Кавалерия":
                _cavalryUnits.Add(MeleeSoldiersInit["Кавалерия"]());
                break;
            case "Дальний бой":
                _rangedUnits.Add(DistantSoldiersInit["Дальний бой"]());
                break;
        }
        UpdateUI();
    }

    // Метод для обновления отображения в интерфейсе
    private void UpdateUI()
    {
        pietyText.text = Piety.ToString();
        goldText.text = Gold.ToString();
        UpdateRecruitsText();
        UpdateUnitText("Пехота", _infantryUnits);
        UpdateUnitText("Кавалерия", _cavalryUnits);
        UpdateUnitText("Дальний бой", _rangedUnits);
    }
    
    private void UpdateRecruitsText()
    {
        var unitTextMesh = GameObject.Find($"Text (Новобранцы)").GetComponent<TextMeshProUGUI>();
        if (unitTextMesh != null)
            unitTextMesh.text = _recruitsCount.ToString();
    }

    private void UpdateUnitText<T>(string unitType, List<T> units)
    {
        var unitTextMesh = GameObject.Find($"Text ({unitType})").GetComponent<TextMeshProUGUI>();
        if (unitTextMesh != null) 
            unitTextMesh.text = units.Count.ToString();
    }

    public bool CanMovePosition(MapRegion region)
    {
        return !((region.position - position.position).magnitude > GameManager.TransitionMaxLength);
    }

    // Методы для перемещения по карте
    public bool TryMovePosition(MapRegion region)
    {
        if (!CanMovePosition(region))
        {
            return false;
        }
        if (region.regionType.ToLower() == "enemy")
        {
            lastFriendlyPosition = position;
        }
        position = region;
        HireRecruits(10);
        return true;
    }

    private void Update()
    {
        if (position != null)
        {
            transform.position = position.position;
        }
        UpdateUI();
    }
}