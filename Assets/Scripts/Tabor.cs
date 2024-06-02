using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tabor : MonoBehaviour
{
    [SerializeField] private Image _image;
    public Player _player;
    private bool flagLessDamage = true;
    private bool flagMoreDamage = true;
    private bool flagLessRangedDamage = true;

    public void ShowTabor()
    {
        _image.gameObject.SetActive(true);

    }
    
    public void ClickLessDamage()
    {
        _player.ModifyGold(40000);
        if (_player.Gold < 15000 || !flagLessDamage)
            return;
        _player.army.GetDamageResistanceModifier(40);
        _player.ModifyGold(-15000);
        flagLessDamage = false;
    }
    public void ClickMoreDamage()
    {
        _player.ModifyGold(40000);
        if (_player.Gold < 40000 || !flagMoreDamage)
        {
            Debug.Log("good thing");
            return;
        }
        Debug.Log("bad thing");
        _player.army.GetDamageModifier(40);
        _player.ModifyGold(-40000);
        flagMoreDamage = false;
    }
    public void ClickLessRangedDamage()
    {
        if (_player.Gold < 40000 || !flagLessRangedDamage)
            return;
        _player.army.GetDamageResistanceModifier(15);
        _player.ModifyGold(-40000);
        flagLessRangedDamage = false;
    }
    
    public void HideTabor()
    {
        _image.gameObject.SetActive(false);
    }
}