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
    [SerializeField] private TMP_Text _cavalryCost;
    [SerializeField] private TMP_Text _rangedCost;
    [SerializeField] private int _infantryMoney;
    [SerializeField] private int _cavalryMoney;
    [SerializeField] private int _rangedMoney;
    [SerializeField] private (int, int) pietyCoeff;

    public void ShowCity(MapRegion _currentMapRegion)
    {
        _image.gameObject.SetActive(true);
        if (_player.Piety <= 50)
        {
            pietyCoeff = (1, 1);
        } else if (_player.Piety <= 80)
        {
            pietyCoeff = (5, 4);
        } else
        {
            pietyCoeff = (3, 2);
        }

        _infantryMoney = ArmyCosts.InfantryCost * pietyCoeff.Item1 / pietyCoeff.Item2;
        _cavalryMoney = ArmyCosts.CavalryCost * pietyCoeff.Item1 / pietyCoeff.Item2;
        _rangedMoney = ArmyCosts.CrossbowMenCost * pietyCoeff.Item1 / pietyCoeff.Item2;
        UpdateUI(_currentMapRegion);
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

    public void UpdateUI(MapRegion _currentMapRegion)
    {
        _recruitsAmount.text = _player.army.RecruitsCount.ToString();
        _infatryCost.text = _infantryMoney.ToString();
        _cavalryCost.text = _cavalryMoney.ToString();
        _rangedCost.text = _rangedMoney.ToString();
        _regionName.text = _currentMapRegion.regionName;
    }

    public void HideCity()
    {
        _image.gameObject.SetActive(false);
    }
}