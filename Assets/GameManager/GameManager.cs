using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Farm _farm;
    [SerializeField] private City _city;
    [SerializeField] private Tabor _tabor;
    [SerializeField] private EnemyProvince _enemyProvince;
    [SerializeField] private FightWindow _fightWindow;
    [SerializeField] private RandomEventWindow eventWindow;
    [SerializeField] private PlotEventWindow plotEventWindow;
    public GameObject playerPrefab; // Префаб игрока
    [FormerlySerializedAs("_player")] public Player player; // Модель игрока
    private PlayerMovement _playerMovement; // Контроллер перемещения игрока
    public MapRegion currentRegion; // Текущий регион, в котором находится игрок
    public bool standartInterfaceBlock = false;
    private GameObject playerObject;
    public ProgressSaveManager saveManager;

    private void Start()
    {
        Debug.Log("a");
        saveManager = new ProgressSaveManager();
        if (saveManager.TryReadFromSave(SaveType.ManualSave, out var saveData))
        {
            LoadFromSave(saveData);
        }
        else if (saveManager.TryReadFromSave(SaveType.BasicSave, out var baseData))
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
        player = playerObject.GetComponent<Player>();

        // Создание экземпляра контроллера перемещения
        _playerMovement = playerObject.GetComponent<PlayerMovement>();
        _playerMovement.SetFirstCurrentPosition();

        // Установка начального региона для игрока
        currentRegion = _playerMovement.initialRegion;
        player.position = currentRegion;

        // UpdateUI();
    }

    // Метод для перемещения игрока в указанный регион
    public void MovePlayerToRegion(MapRegion targetRegion)
    {
        if (TurnManager.Instance.CanMove())
        {
            currentRegion = _playerMovement.MoveToRegion(player, targetRegion);
            ShowRegion(currentRegion);
        }
        //UpdateUI();
    }

    public void ShowRegion(MapRegion currentRegion)
    {
        switch (currentRegion.regionType)
        {
            case "tabor":
                _city.HideCity();
                _farm.HideFarm();
                _enemyProvince.HideEnemyProvince();
                _tabor.ShowTabor();
                break;
            case "city":
                _farm.HideFarm();
                _tabor.HideTabor();
                _enemyProvince.HideEnemyProvince();
                _city.ShowCity(currentRegion);
                break;
            case "enemy":
                _farm.HideFarm();
                _tabor.HideTabor();
                _city.HideCity();
                _enemyProvince.HideEnemyProvince();
                _fightWindow.ShowFightWindow();
                break;
            default:
                _city.HideCity();
                _tabor.HideTabor();
                _enemyProvince.HideEnemyProvince();
                _farm.ShowFarm(currentRegion);
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
            saveManager.SaveProgress(SaveType.ManualSave, player);
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
        if (player.ArmyMaintenance())
        {
            currentRegion = _playerMovement.EmergencyTeleport(player); 
        }
    }

    public void BeginTurnCompute(int turn) 
    {
        player.army.GetArmyForBattle();
        saveManager.BeginTurn(player);
        TurnEvents(turn);
        try { TurnEvents(turn); } catch { Debug.Log("Событий нет"); }
    }

    public void TurnEvents(int turn)
    {
        MapRegion battlePlace;

        if (turn <= 3)
        {
            return;
        }
        switch (turn)
        {
            case 9:
                battlePlace = GameObject.Find("region22").GetComponent<MapRegion>();
                _playerMovement.Teleport(player, battlePlace);
                _fightWindow.ShowFightWindow("secondCrusade");
                break;
            case 15:
                battlePlace = GameObject.Find("3er4").GetComponent<MapRegion>();
                _playerMovement.Teleport(player, battlePlace);
                _fightWindow.ShowFightWindow("silesia");
                break;
            case 22:
                battlePlace = GameObject.Find("er8").GetComponent<MapRegion>();
                _playerMovement.Teleport(player, battlePlace);
                _fightWindow.ShowFightWindow("saxonia");
                break;
            case 27:
                battlePlace = GameObject.Find("region9").GetComponent<MapRegion>();
                _playerMovement.Teleport(player, battlePlace);
                _fightWindow.ShowFightWindow("thirdCrusade");
                break;
            case 40:
                battlePlace = GameObject.Find("2er31").GetComponent<MapRegion>();
                _playerMovement.Teleport(player, battlePlace);
                if (player.Piety > 50)
                {
                    _fightWindow.ShowFightWindow("moderate");
                } else
                {
                    _fightWindow.ShowFightWindow("tabor");
                }
                break;
            case 5:
                plotEventWindow.ShowEventWindow(PlotEvent.BremenMusitians);
                break;
            case 10:
                plotEventWindow.ShowEventWindow(PlotEvent.Gutenberg);
                break;
            case 13:
                plotEventWindow.ShowEventWindow(PlotEvent.Witcher);
                break;
            case 20:
                plotEventWindow.ShowEventWindow(PlotEvent.Copernicus);
                break;
            case 25:
                plotEventWindow.ShowEventWindow(PlotEvent.Kuplinov);
                break;
            default:
                eventWindow.ShowEventWindow(RandomEvents.GetRandomEvent());
                break;
        }
    }

    public void LoadFromSave(ProgressData progressData)
    {
        // Создание игрока из префаба
        if (player != null)
        {
            Destroy(playerObject);
        }

        playerObject = Instantiate(playerPrefab);
        player = playerObject.GetComponent<Player>();
        player.InitializeFromSave(progressData);

        // Создание экземпляра контроллера перемещения
        _playerMovement = playerObject.GetComponent<PlayerMovement>();
        _playerMovement.initialRegion = GameObject.Find(progressData.Position)
            .GetComponent<MapRegion>();
        _playerMovement.SetFirstCurrentPosition();

        // Установка начального региона для игрока
        currentRegion = _playerMovement.initialRegion;
        player.position = currentRegion;

        //Номер и начало хода
        TurnManager.Instance.LoadFromSave(progressData);

        SetPlayerInLocations();
    }

    public void SetPlayerInLocations()
    {
        if (player != null)
        {
            _farm._player = player;
            _city._player = player;
            _enemyProvince._player = player;
            _fightWindow._player = player;
            _fightWindow.movement = _playerMovement;
            eventWindow._player = player;
            _tabor._player = player;
        }
    }
}