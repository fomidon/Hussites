using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Farm : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    [SerializeField] public TMP_Text _regionName;
    // private MapRegion _currentMapRegion;

    public void ShowFarm(MapRegion _currentMapRegion)
    {
        _image.gameObject.SetActive(true);
        var name = _currentMapRegion;
        _regionName.text = name.regionName;

    }
    
    public void ClickHire()
    {
        _player.army.HireRecruits(1);
        //Debug.Log(_player.army._recruitsCount);
    }
    
    public void HideFarm()
    {
        _image.gameObject.SetActive(false);
    }
}