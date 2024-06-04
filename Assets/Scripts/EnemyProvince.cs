using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyProvince : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    [SerializeField] public TMP_Text _regionName;
    private MapRegion _currentMapRegion;

    public void ShowEnemyProvince()
    {
        _image.gameObject.SetActive(true);
    }
    
    public void ClickRob()
    {
        _player.ModifyGold(30000);
        HideEnemyProvince();
    }
    
    public void HideEnemyProvince()
    {
        _image.gameObject.SetActive(false);
    }
}