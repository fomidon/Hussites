using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlotEventWindow : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    [SerializeField] public PlotEvent currentEvent = PlotEvent.BremenMusitians;

    [SerializeField] public TMP_Text eventText;
    [SerializeField] public TMP_Text eventName;

    public void ShowEventWindow(PlotEvent plotEvent)
    {
        currentEvent = plotEvent;
        _image.gameObject.SetActive(true);
        eventName.text = currentEvent.name;
        eventText.text = currentEvent.text;
    }

    public void ClickOk()
    {
        HideRandomEvent();
    }
    

    public void HideRandomEvent()
    {
        _image.gameObject.SetActive(false);
    }

    public void Update()
    {
        eventText.text = currentEvent.text;
        eventName.text = currentEvent.name;
    }
}
