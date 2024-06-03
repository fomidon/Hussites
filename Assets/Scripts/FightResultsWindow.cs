using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightResultsWindow : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    [SerializeField] private EnemyProvince _enemyProvince;
    [SerializeField] private TMP_Text _winOrNoText;
    [SerializeField] private List<TMP_Text> armyStatsTMP;
    
    private MapRegion _currentMapRegion;

    public void ShowFightResultsWindow(bool result, List<(int,int)> armyBeforeBattle, List<(int,int)> armyAfterBattle)
    {
        _image.gameObject.SetActive(true);
        _winOrNoText.text = result ? "Победа" : "Поражение";
        for (var i = 0; i < armyStatsTMP.Count; i++)
            armyStatsTMP[i].text = StatsToString(armyBeforeBattle[i].Item1, armyAfterBattle[i].Item1, armyAfterBattle[i].Item2);
    }
    
    public void ClickOk()
    {
        HideFightResultsWindow();
        if (_winOrNoText.text == "Победа")
            _enemyProvince.ShowEnemyProvince();
    }

    private void HideFightResultsWindow()
    {
        _image.gameObject.SetActive(false);
    }

    private string StatsToString(int countBefore, int countAfter, int maxCount)
        => $"({countBefore} \u2192 {countAfter}) / {maxCount}";
}