using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    // Позиция игрока на карте и последняя дружественная позиция
    public MapRegion position;
    public MapRegion startPosition;

    // Свойства для хранения валюты
    public int Piety { get; private set; }
    public int Gold { get; private set; }

    // Списки для хранения количества войск

    public PlayerArmy army = new();

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
        army.SetPietyModifier(Piety);
    }

    public bool ArmyMaintenance()
    {
        if (position.regionType.ToLower() != "enemy")
        {
            return false;
        }

        if (Gold - army.ArmyMaintenance > 0)
        {
            Gold -= army.ArmyMaintenance;
            return false;
        }

        Gold = 0;
        return true;
    }
}