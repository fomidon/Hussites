using UnityEngine;
using TMPro;
using System.Collections.Generic;
using JetBrains.Annotations;
using System;
using System.Linq;

public class Player : MonoBehaviour
{
    // Позиция игрока на карте и последняя дружественная позиция
    public MapRegion position;
    public MapRegion startPosition;

    // Массивы с классами юнитов разных родов войск
    private readonly string[] _infarnyUnitTypes = { "Новобранцы", "Пехота" };
    private readonly string[] _cavarlyUnitTypes = { "Кавалерия" };
    private readonly string[] _rangedUnitTypes = { "Дальний бой" };

    // Свойства для хранения валюты
    public int Piety { get; private set; }
    public int Gold { get; private set; }

    // Списки для хранения количества войск
    public const int MaxArmySize = 20;
    public int _recruitsCount { get; private set; } = 0;
    private readonly List<MeleeSoldier> _infantryUnits = new List<MeleeSoldier>();
    public List<MeleeSoldier> infantryOutside{ get => _infantryUnits.ToList(); }

    private readonly List<MeleeSoldier> _cavalryUnits = new List<MeleeSoldier>();
    public List<MeleeSoldier> cavalryOutside { get => _cavalryUnits.ToList(); }

    private readonly List<DistantSoldier> _rangedUnits = new List<DistantSoldier>();
    public List<DistantSoldier> rangedUnitsOutside { get => _rangedUnits.ToList(); }

    public int armySize { get => _recruitsCount + _infantryUnits.Count + 
            _cavalryUnits.Count + _rangedUnits.Count; }

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

    // Метод для изменения количества войск
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
                _infantryUnits.Add(UnitsInit.InitInfantrySoldiers());
                break;
            case "Кавалерия":
                _cavalryUnits.Add(UnitsInit.InitCavalrySoldiers());
                break;
            case "Дальний бой":
                _rangedUnits.Add(UnitsInit.InitCrossbowSoldiers());
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
        UpdateUnitText(_infantryUnits, "Пехота");
        UpdateUnitText(_cavalryUnits, "Кавалерия");
        UpdateUnitText(_rangedUnits, "Дальний бой");
    }

    private void UpdateRecruitsText()
    {
        var unitTextMesh = GameObject.Find($"Text (Новобранцы)").GetComponent<TextMeshProUGUI>();
        if (unitTextMesh != null) 
            unitTextMesh.text = _recruitsCount.ToString();
    }
    
    private void UpdateUnitText<T>(List<T> Units, string unitType)
    {
        var unitTextMesh = GameObject.Find($"Text ({unitType})").GetComponent<TextMeshProUGUI>();
        if (unitTextMesh != null) 
            unitTextMesh.text = Units.Count.ToString();
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