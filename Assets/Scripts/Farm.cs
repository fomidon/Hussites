using UnityEngine;
using UnityEngine.UI;

public class Farm : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Player _player;
    private bool _canClick = true;
    private MapRegion _currentMapRegion;

    public void ShowFarm()
    {
        _image.gameObject.SetActive(true);
    }
    
    public void ClickHire()
    {
        _player.HireRecruits(1);
        Debug.Log(_player._recruitsCount);
    }
    
    public void HideFarm()
    {
        _image.gameObject.SetActive(false);
    }
}