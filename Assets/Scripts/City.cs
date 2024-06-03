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
    [SerializeField] private int _infantryMoney = 8000;
    [SerializeField] private int _cavalryMoney = 16000;
    [SerializeField] private int _rangedMoney = 16000;
    [SerializeField] private (int, int) pietyModifier = (1, 1);
    

    public void ShowCity(MapRegion _currentMapRegion)
    {
        _image.gameObject.SetActive(true);
        _recruitsAmount.text = _player.army.RecruitsCount.ToString();
        var piety = _player.Piety;
        if (piety <= 50)
        {
            pietyModifier = (1, 1);
        }
        else if (piety <= 80)
        {
            pietyModifier = (5, 4);
        } else
        {
            pietyModifier = (3, 2);
        }
        _infantryMoney = ArmyCosts.InfantryCost * pietyModifier.Item1 / pietyModifier.Item2;
        _cavalryMoney = ArmyCosts.CavalryCost * pietyModifier.Item1 / pietyModifier.Item2;
        _rangedMoney = ArmyCosts.CrossbowMenCost * pietyModifier.Item1 / pietyModifier.Item2;

        _infatryCost.text = _infantryMoney.ToString();
        _cavarlyCost.text = _cavalryMoney.ToString();
        _rangedCost.text = _rangedMoney.ToString();
        _regionName.text = _currentMapRegion.regionName;
    }

    public void ClickTrainInfantry()
    {
        if (_player.Gold < _infantryMoney || !_player.army.CanTrainRecruits())
        {
            return;
        }
        _player.ModifyGold(-_infantryMoney);
        _player.army.TrainRecruit("Пехота");
        //Debug.Log(_player.army.infantryOutside.Count);
    }

    public void ClickTrainCavarly()
    {
        if (_player.Gold < _cavalryMoney || !_player.army.CanTrainRecruits())
        {
            return;
        }
        _player.ModifyGold(-_cavalryMoney);
        _player.army.TrainRecruit("Кавалерия");
        Debug.Log(_player.army.CavalryOutside.Count);
    }
    
    public void ClickTrainRanged()
    {
        if (_player.Gold < _rangedMoney || !_player.army.CanTrainRecruits())
        {
            return;
        }
        _player.ModifyGold(-_rangedMoney);
        _player.army.TrainRecruit("Дальний бой");
        Debug.Log(_player.army.RangedUnitsOutside.Count);
    }
    
    public void HideCity()
    {
        _image.gameObject.SetActive(false);
    }
}