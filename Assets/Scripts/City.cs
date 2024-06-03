using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    [SerializeField] public TMP_Text _regionName;
    [SerializeField] private TMP_Text _recruitsAmount;
    [SerializeField] private TMP_Text _infatryCost;
    [SerializeField] private TMP_Text _cavarlyCost;
    [SerializeField] private TMP_Text _rangedCost;
    

    public void ShowCity(MapRegion _currentMapRegion)
    {
        _image.gameObject.SetActive(true);
        _recruitsAmount.text = _player.army.RecruitsCount.ToString();
        _infatryCost.text = ArmyCosts.InfantryCost.ToString();
        _cavarlyCost.text = ArmyCosts.CavalryCost.ToString();
        _rangedCost.text = ArmyCosts.CrossbowMenCost.ToString();
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