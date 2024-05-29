using UnityEngine;
using UnityEngine.UI;

public class FightWindow : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    [SerializeField] public FightResultsWindow _fightResultsWindow;
    private MapRegion _currentMapRegion;

    public void ShowFightWindow()
    {
        _image.gameObject.SetActive(true);
    }
    
    public void ClickFight()
    {
        Debug.Log("I started fight");
        HideFightWindow();
        _fightResultsWindow.ShowFightResultsWindow();
        
    }
    
    public void HideFightWindow()
    {
        _image.gameObject.SetActive(false);
    }
}