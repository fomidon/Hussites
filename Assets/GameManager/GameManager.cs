using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Farm _farm;
    [SerializeField] private City _city;
    [SerializeField] private EnemyProvince _enemyProvince;
    public GameObject playerPrefab; // Префаб игрока
    private Player _player; // Модель игрока
    private PlayerMovement _playerMovement; // Контроллер перемещения игрока
    public MapRegion currentRegion; // Текущий регион, в котором находится игрок
    public bool standartInterfaceBlock = false;
    private GameObject playerObject;

    private void Start()
    {
        if (ProgressSaveManager.TryReadFromSave(SaveType.BasicSave, out var baseData))
        {
            LoadFromSave(baseData);
        }
        else
        {
            InitializeGame();
        }
    }

    private void InitializeGame()
    {
        // Создание игрока из префаба
        playerObject = Instantiate(playerPrefab);
        _player = playerObject.GetComponent<Player>();

        // Создание экземпляра контроллера перемещения
        _playerMovement = playerObject.GetComponent<PlayerMovement>();
        _playerMovement.SetFirstCurrentPosition();

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
        currentRegion = _playerMovement.MoveToRegion(_player, targetRegion);
        //UpdateUI();
    }

    public void ShowRegion(MapRegion currentRegion)
    {
        switch (currentRegion.regionType)
        {
            case "farm":
                _city.HideCity();
                _enemyProvince.HideEnemyProvince();
                _farm.ShowFarm();
                break;
            case "city":
                _farm.HideFarm();
                _enemyProvince.HideEnemyProvince();
                _city.ShowCity();
                break;
            case "enemy":
                _farm.HideFarm();
                _city.HideCity();
                _enemyProvince.ShowEnemyProvince();
                break;
            default:
                Debug.LogWarning("Неизвестный тип региона");
                break;
        }
    }

    public void Update()
    {
        SaveHotkeys();
    }

    public void SaveHotkeys()
    {
        if (Input.GetKeyUp(KeyCode.F5))
        {
            ProgressSaveManager.SaveProgress(SaveType.ManualSave, _player);
        }

        if (Input.GetKeyUp(KeyCode.F9))
        {
            if (ProgressSaveManager.TryReadFromSave(SaveType.ManualSave, out var saveData))
            {
                LoadFromSave(saveData);
            }
        }
    }

    public void LoadFromSave(ProgressData progressData)
    {
        // Создание игрока из префаба
        if (_player != null)
        {
            Destroy(playerObject);
        }

        playerObject = Instantiate(playerPrefab);
        _player = playerObject.GetComponent<Player>();
        _player.InitializeFromSave(progressData);

        // Создание экземпляра контроллера перемещения
        _playerMovement = playerObject.GetComponent<PlayerMovement>();
        _playerMovement.initialRegion = GameObject.Find(progressData.Position)
            .GetComponent<MapRegion>();
        _playerMovement.SetFirstCurrentPosition();

        // Установка начального региона для игрока
        currentRegion = _playerMovement.initialRegion;
        _player.position = currentRegion;
    }
}