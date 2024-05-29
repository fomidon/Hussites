using UnityEngine;
using UnityEngine.UI;

public class EnemyProvince : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    private MapRegion _currentMapRegion;

    public void ShowEnemyProvince()
    {
        _image.gameObject.SetActive(true);
    }
    
    public void ClickRob()
    {
        _player.ModifyGold(30000);
    }
    
    public void HideEnemyProvince()
    {
        _image.gameObject.SetActive(false);
    }
}