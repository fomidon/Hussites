using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player playerPrefab;
    public int startingPiety = 100;
    public int startingGold = 50;

    void Start()
    {
        // Создание игрока и инициализация его значений
        var playerInstance = Instantiate(playerPrefab);
        playerInstance.Initialize(startingPiety, startingGold);
    }
}