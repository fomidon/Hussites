using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Player _player;
    private bool _canClick = true;
    private MapRegion _currentMapRegion;

    public void ShowCity()
    {
        _image.gameObject.SetActive(true);
    }
    
    public void ClickTrainInfantry()
    
    {
        _player.TrainRecruit("Пехота");
        Debug.Log(_player.infantryOutside.Count);
    }
    
    public void ClickTrainCavarly()
    {
        _player.TrainRecruit("Кавалерия");
        Debug.Log(_player.cavalryOutside.Count);
    }
    
    public void ClickTrainRanged()
    {
        _player.TrainRecruit("Дальний бой");
        Debug.Log(_player.rangedUnitsOutside.Count);
    }
    
    public void HideCity()
    {
        _image.gameObject.SetActive(false);
    }
}