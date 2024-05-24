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
    public ProgressSaveManager saveManager;

    private void Start()
    {
        saveManager = new ProgressSaveManager();
        if (saveManager.TryReadFromSave(SaveType.BasicSave, out var baseData))
        {
            LoadFromSave(baseData);
        }
        else
        {
            InitializeGame();
        }
        _farm._player = _player;
        _city._player = _player;
        _enemyProvince._player = _player;
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
        if (TurnManager.Instance.CanMove())
        {
            currentRegion = _playerMovement.MoveToRegion(_player, targetRegion);
            ShowRegion(currentRegion);
        }
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
                _farm.HideFarm();
                _city.HideCity();
                _enemyProvince.HideEnemyProvince();
                Debug.LogWarning("Неизвестный тип региона");
                break;
        }
    }

    public void Update()
    {
        SaveHotkeys();
        if (TurnManager.Instance.EndTurnToCompute)
        {
            TurnManager.Instance.EndTurnToCompute = false;
            EndTurnCompute(TurnManager.Instance.TurnsCount);
        }

        if (TurnManager.Instance.BeginTurnToCompute)
        {
            TurnManager.Instance.BeginTurnToCompute = false;
            BeginTurnCompute(TurnManager.Instance.TurnsCount);
        }
    }

    public void SaveHotkeys()
    {
        if (Input.GetKeyUp(KeyCode.F5))
        {
            saveManager.SaveProgress(SaveType.ManualSave, _player);
        }

        if (Input.GetKeyUp(KeyCode.F9))
        {
            if (saveManager.TryReadFromSave(SaveType.ManualSave, out var saveData))
            {
                LoadFromSave(saveData);
            }
        }
    }

    public void EndTurnCompute(int turn)
    {
        if (_player.ArmyMaintenance())
        {
            currentRegion = _playerMovement.EmergencyTeleport(_player); 
        }
    }

    public void BeginTurnCompute(int turn) 
    {
        saveManager.BeginTurn(_player);
        try { TurnEvents(turn); } catch { Debug.Log("Событий нет"); }
    }

    public void TurnEvents(int turn)
    {
       throw new NotImplementedException();
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

        //Номер и начало хода
        TurnManager.Instance.LoadFromSave(progressData);
    }
}