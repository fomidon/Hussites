using UnityEngine;
using TMPro;
using System.Collections.Generic;
using JetBrains.Annotations;
using System;
using System.Linq;
using UnityEngine.Windows;

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

    public PlayerArmy army = new PlayerArmy();

    // Ссылки на текст для отображения в интерфейсе
    public TextMeshProUGUI pietyText;
    public TextMeshProUGUI goldText;

    // Метод для установки начальных значений
    public void Initialize(int startingPiety, int startingGold)
    {
        Piety = startingPiety;
        Gold = startingGold;

        position = startPosition;
    }

    // Метод для установки начальных значений из сохранения
    public void InitializeFromSave(ProgressData data)
    {
        Gold = data.Gold;
        Piety = data.Piety;
        position = GameObject.Find(data.Position).GetComponent<MapRegion>();
        army = new PlayerArmy(data);
    }

    private void InitializeUnitDictionary(Dictionary<string, int> unitDict, string[] unitTypes)
    {
        foreach (var unitType in unitTypes) unitDict.Add(unitType, 0);
    }

    // Метод для изменения благочестия
    public void ModifyPiety(int amount)
    {
        Piety += amount;
    }

    // Метод для изменения золота
    public void ModifyGold(int amount)
    {
        Gold += amount;
    }

    // Метод для обновления отображения в интерфейсе
    private void UpdateUI()
    {
        pietyText.text = Piety.ToString();
        goldText.text = Gold.ToString();
        army.UpdateUI();
    }

    private void Update()
    {
        if (position != null)
        {
            transform.position = position.Position;
        }

        UpdateUI();
        SaveTest();
    }

    public void SaveTest()
    {
        if (UnityEngine.Input.GetKeyUp(KeyCode.J))
        {
            army.HireRecruits(4);
            army.TrainRecruit("Пехота");
            army.TrainRecruit("Пехота");
            army.TrainRecruit("Кавалерия");
            army.TrainRecruit("Дальний бой");
            Gold = 49992;
            Piety = 38;
        }
    }

    public void ArmyMaintenance()
    {
        
    }
}