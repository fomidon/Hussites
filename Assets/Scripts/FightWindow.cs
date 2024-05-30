using System.Collections.Generic;
using UnityEngine;
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

    public void ShowFightWindow(string type = "standart")
    {
        _image.gameObject.SetActive(true);
        _currentMapRegion = _player.position;
        this.type = type;
    }

    public (List<IUniversalSoldier>,  List<IUniversalSoldier>, int, bool) Battle()
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
        (playerArmy, enemyArmy, possibleTrophy, battleResult) = Battle();

        if (battleResult)
        {
            _player.ModifyGold(possibleTrophy);
        } else
        {
            movement.EmergencyTeleport(_player);
        }
        _player.army.GetArmyFromBattle(playerArmy);
        _fightResultsWindow.ShowFightResultsWindow();
        
    }
    
    public void HideFightWindow()
    {
        _image.gameObject.SetActive(false);
    }
}