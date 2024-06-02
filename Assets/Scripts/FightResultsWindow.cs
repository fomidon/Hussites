using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightResultsWindow : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    [SerializeField] private EnemyProvince _enemyProvince;
    [SerializeField] private TMP_Text _winOrNoText;
    
    private MapRegion _currentMapRegion;

    public void ShowFightResultsWindow(bool result)
    {
        _image.gameObject.SetActive(true);
        _winOrNoText.text = result ? "Победа" : "Поражение";
    }
    
    public void ClickOk()
    {
        HideFightResultsWindow();
        if (_winOrNoText.text == "Победа")
            _enemyProvince.ShowEnemyProvince();
    }
    
    public void HideFightResultsWindow()
    {
        _image.gameObject.SetActive(false);
    }
}