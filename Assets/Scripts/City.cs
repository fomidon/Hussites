using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    [SerializeField] public TMP_Text _regionName;

    public void ShowCity(MapRegion _currentMapRegion)
    {
        _image.gameObject.SetActive(true);
        _regionName.text = _currentMapRegion.regionName;
    }

    public void ClickTrainInfantry()
    {
        if (_player.Gold < 8000 || !_player.army.CanTrainRecruits())
        {
            return;
        }
        _player.ModifyGold(-ArmyCosts.InfantryCost);
        _player.army.TrainRecruit("Пехота");
        //Debug.Log(_player.army.infantryOutside.Count);
    }

    public void ClickTrainCavarly()
    {
        if (_player.Gold < 16000 || !_player.army.CanTrainRecruits())
        {
            return;
        }
        _player.ModifyGold(-ArmyCosts.CavalryCost);
        _player.army.TrainRecruit("Кавалерия");
        Debug.Log(_player.army.CavalryOutside.Count);
    }
    
    public void ClickTrainRanged()
    {
        if (_player.Gold < 16000 || !_player.army.CanTrainRecruits())
        {
            return;
        }
        _player.ModifyGold(-ArmyCosts.CrossbowMenCost);
        _player.army.TrainRecruit("Дальний бой");
        Debug.Log(_player.army.RangedUnitsOutside.Count);
    }
    
    public void HideCity()
    {
        _image.gameObject.SetActive(false);
    }
}