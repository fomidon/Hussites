using UnityEngine;
using UnityEngine.UI;

public class EnemyProvince : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    private bool _canClick = true;
    private MapRegion _currentMapRegion;

    public void ShowEnemyProvince()
    {
        _image.gameObject.SetActive(true);
    }
    
    public void ClickRob()
    {
        _player.ModifyGold(30000);
        Debug.Log(_player.army._recruitsCount);
    }
    
    public void HideEnemyProvince()
    {
        _image.gameObject.SetActive(false);
    }
}