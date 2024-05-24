using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    private bool _canClick = true;
    private MapRegion _currentMapRegion;

    public void ShowCity()
    {
        _image.gameObject.SetActive(true);
    }

    public void ClickTrainInfantry()
    {
        if (_player.Gold < 8000 || !_player.army.CanTrainRecruits())
        {
            return;
        }
        _player.ModifyGold(-ArmyCosts.InfantryCost);
        _player.army.TrainRecruit("Пехота");
        Debug.Log(_player.army.infantryOutside.Count);
    }

    public void ClickTrainCavarly()
    {
        if (_player.Gold < 16000 || !_player.army.CanTrainRecruits())
        {
            return;
        }
        _player.ModifyGold(-ArmyCosts.CavalryCost);
        _player.army.TrainRecruit("Кавалерия");
        Debug.Log(_player.army.cavalryOutside.Count);
    }
    
    public void ClickTrainRanged()
    {
        if (_player.Gold < 16000 || !_player.army.CanTrainRecruits())
        {
            return;
        }
        _player.ModifyGold(-ArmyCosts.CrossbowMenCost);
        _player.army.TrainRecruit("Дальний бой");
        Debug.Log(_player.army.rangedUnitsOutside.Count);
    }
    
    public void HideCity()
    {
        _image.gameObject.SetActive(false);
    }
}