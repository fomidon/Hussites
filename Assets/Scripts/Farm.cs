using UnityEngine;
using UnityEngine.UI;

public class Farm : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    private bool _canClick = true;
    private MapRegion _currentMapRegion;

    public void ShowFarm()
    {
        _image.gameObject.SetActive(true);
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