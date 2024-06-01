using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Farm : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    [SerializeField] public TMP_Text _regionName;

    public void ShowFarm(MapRegion _currentMapRegion)
    {
        _image.gameObject.SetActive(true);
        _regionName.text = _currentMapRegion.regionName;

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