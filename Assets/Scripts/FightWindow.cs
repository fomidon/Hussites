using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FightWindow : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    [SerializeField] public FightResultsWindow _fightResultsWindow; 
    [SerializeField] private MapRegion _currentMapRegion;
    [SerializeField] private int counter = 0;
    [SerializeField] private string type = "";
    public PlayerMovement movement;
    public bool isWindowActivate => _image.IsActive();

    public void ShowFightWindow(string type = "standart")
    {
        _image.gameObject.SetActive(true);
        _currentMapRegion = _player.position;
        this.type = type;
    }

    private (List<IUniversalSoldier>,  List<IUniversalSoldier>, int, bool) Battle()
    {
        var enemyArmy = new List<IUniversalSoldier>();
        var possibleTrophy = 0;
        _ = new List<IUniversalSoldier>();
        bool battleResult;
        switch (type)
        {
            case "standart":
                counter = (counter + 1) % 5;
                if (counter == 0)
                {
                    enemyArmy = BotArmies.BigEnemyArmy;
                    possibleTrophy = BotArmies.BigRevard;
                }
                else
                {
                    enemyArmy = BotArmies.StandartEnemyArmy;
                    possibleTrophy = BotArmies.StandartRevard;
                }
                break;
            case "secondCrusade":
                enemyArmy = BotArmies.SecondCrusadeArmy;
                possibleTrophy = BotArmies.SecondCrusadeRevard;
                break;
            case "silesia":
                enemyArmy = BotArmies.SilesianArmy;
                possibleTrophy = BotArmies.SilesianRevard;
                break;
            case "saxonia":
                enemyArmy = BotArmies.SaxonianArmy;
                possibleTrophy = BotArmies.SaxonianRevard;
                break;
            case "thirdCrusade":
                enemyArmy = BotArmies.ThirdCrusadeArmy;
                possibleTrophy = BotArmies.ThirdCrusadeRevard;
                break;
            case "moderate":
                enemyArmy = BotArmies.ModerateArmy;
                possibleTrophy = BotArmies.ModerateRevard;
                break;
            case "tabor":
                enemyArmy = BotArmies.TaborArmy;
                possibleTrophy = BotArmies.TaborRevard;
                break;
            default:
                enemyArmy = BotArmies.StandartEnemyArmy;
                possibleTrophy = BotArmies.StandartRevard;
                break;
        }

        List<IUniversalSoldier> playerArmy;
        (playerArmy, enemyArmy, battleResult) = AutoBattleCalculation.AutoBattleCalculate(
                    _player.army.GetArmyForBattle(), enemyArmy);
        return (playerArmy, enemyArmy, possibleTrophy, battleResult);
    }
    
    public void ClickFight()
    {
        Debug.Log("I started fight");
        HideFightWindow();
        var enemyArmy = new List<IUniversalSoldier>();
        var possibleTrophy = 0;
        var playerArmy = new List<IUniversalSoldier>();
        bool battleResult;
        //var playerArmyBeforeBattle = GetArmyCountList();
        var playerArmyBeforeBattle = _player.army.GetSoldiersNumbers();
        (playerArmy, enemyArmy, possibleTrophy, battleResult) = Battle();
        //var playerArmyAfterBattle = GetArmyCountList();
        var playerArmyAfterBattle = _player.army.GetSoldiersNumbers();
        
        
        
        if (battleResult)
        {
            _fightResultsWindow.ShowFightResultsWindow(true, playerArmyBeforeBattle, playerArmyAfterBattle);
            _player.ModifyGold(possibleTrophy);
        } else
        {
            _fightResultsWindow.ShowFightResultsWindow(false, playerArmyBeforeBattle, playerArmyAfterBattle);
            movement.EmergencyTeleport(_player);
        }
        _player.army.GetArmyFromBattle(playerArmy);
    }

    public void ClickLeave()
    {
        HideFightWindow();
        movement.EmergencyTeleport(_player);
    }
    
    public void HideFightWindow()
    {
        _image.gameObject.SetActive(false);
    }
    
    private List<int> GetArmyCountList()
    => new()
    {
        _player.army.RecruitsCount,
        _player.army.InfantryOutside.Count,
        _player.army.CavalryOutside.Count,
        _player.army.RangedUnitsOutside.Count
    };
}