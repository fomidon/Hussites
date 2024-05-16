using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab; // Префаб игрока
    private Player _player; // Модель игрока
    private PlayerMovement _playerMovement; // Контроллер перемещения игрока
    public MapRegion currentRegion; // Текущий регион, в котором находится игрок
    

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        // Создание игрока из префаба
        var playerObject = Instantiate(playerPrefab);
        _player = playerObject.GetComponent<Player>();

        // Создание экземпляра контроллера перемещения
        _playerMovement = playerObject.GetComponent<PlayerMovement>();

        // Установка начального региона для игрока
        currentRegion = _playerMovement.initialRegion;
        _player.position = currentRegion;
        
        // UpdateUI();
    }

    // // Метод для обновления интерфейса (Контроллер интерфейса) 
    // private void UpdateUI()
    // {
    //     UIManager.Instance.UpdateRegionInfo(currentRegion);
    //     UIManager.Instance.UpdatePlayerResources(_player);
    // }

    // Метод для перемещения игрока в указанный регион
    public void MovePlayerToRegion(MapRegion targetRegion)
    {
        currentRegion = _playerMovement.MoveToRegion(_player,targetRegion);
        //UpdateUI();
    }
}