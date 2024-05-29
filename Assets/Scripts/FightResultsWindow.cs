using UnityEngine;
using UnityEngine.UI;

public class FightResultsWindow : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    [SerializeField] private EnemyProvince _enemyProvince;
    private MapRegion _currentMapRegion;

    public void ShowFightResultsWindow()
    {
        _image.gameObject.SetActive(true);
    }
    
    public void ClickOk()
    {
        Debug.Log("You pressed Ok");
        HideFightResultsWindow();
        _enemyProvince.ShowEnemyProvince();
        
    }
    
    public void HideFightResultsWindow()
    {
        _image.gameObject.SetActive(false);
    }
}