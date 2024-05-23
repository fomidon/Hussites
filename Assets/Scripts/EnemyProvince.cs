using UnityEngine;
using UnityEngine.UI;

public class EnemyProvince : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Player _player;
    private bool _canClick = true;
    private MapRegion _currentMapRegion;

    public void ShowEnemyProvince()
    {
        _image.gameObject.SetActive(true);
    }
    
    public void ClickRob()
    {
        _player.HireRecruits(1);
        Debug.Log(_player._recruitsCount);
    }
    
    public void HideEnemyProvince()
    {
        _image.gameObject.SetActive(false);
    }
}