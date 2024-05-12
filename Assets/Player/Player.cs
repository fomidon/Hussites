﻿using UnityEngine;
using TMPro;
using System.Collections.Generic;
using JetBrains.Annotations;

public class Player : MonoBehaviour
{
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

    // Словари для хранения количества войск
    private readonly Dictionary<string, int> _infantryUnits = new();
    private readonly Dictionary<string, int> _cavalryUnits = new();
    private readonly Dictionary<string, int> _rangedUnits = new();

    // Ссылки на текст для отображения в интерфейсе
    public TextMeshProUGUI pietyText;
    public TextMeshProUGUI goldText;

    // Метод для установки начальных значений
    public void Initialize(int startingPiety, int startingGold)
    {
        Piety = startingPiety;
        Gold = startingGold;

        position = startPosition;
        
        InitializeUnitDictionary(_infantryUnits, _infarnyUnitTypes);
        InitializeUnitDictionary(_cavalryUnits, _cavarlyUnitTypes);
        InitializeUnitDictionary(_rangedUnits, _rangedUnitTypes);

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
    public void ModifyUnitCount(string unitType, int amount)
    {
        if (_infantryUnits.ContainsKey(unitType))
        {
            _infantryUnits[unitType] += amount;
        }
        else if (_cavalryUnits.ContainsKey(unitType))
        {
            _cavalryUnits[unitType] += amount;
        }
        else if (_rangedUnits.ContainsKey(unitType))
        {
            _rangedUnits[unitType] += amount;
        }

        UpdateUI();
    }

    // Метод для обновления отображения в интерфейсе
    private void UpdateUI()
    {
        pietyText.text = Piety.ToString();
        goldText.text = Gold.ToString();
        UpdateUnitText(_infantryUnits);
        UpdateUnitText(_cavalryUnits);
        UpdateUnitText(_rangedUnits);
    }
    
    private void UpdateUnitText(Dictionary<string, int> unitDict)
    {
        foreach (var (unitType, count) in unitDict)
        {
            var unitTextMesh = GameObject.Find($"Text ({unitType})").GetComponent<TextMeshProUGUI>();
            if (unitTextMesh != null) 
                unitTextMesh.text = count.ToString();
        }
    }

    public bool TryMovePosition(MapRegion region)
    {
        if ((region.position - position.position).magnitude > GameManager.TransitionMaxLength)
        {
            return false;
        }
        if (region.regionType.ToLower() == "enemy")
        {
            lastFriendlyPosition = position;
        }
        position = region;
        return true;
    }
    private void Update()
    {
        if (position != null)
        {
            transform.position = position.position;
        }
    }
}