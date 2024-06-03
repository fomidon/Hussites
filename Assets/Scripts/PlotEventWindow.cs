using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlotEventWindow : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    [SerializeField] public RandomEvent currentEvent = RandomEvents.HussitPreacher;

    [SerializeField] public TMP_Text eventText;
    [SerializeField] public TMP_Text eventName;

    public Dictionary<RandomEventType, RandomEvent> events =
        new Dictionary<RandomEventType, RandomEvent> 
        {
            { RandomEventType.HussitPreacher, RandomEvents.HussitPreacher },
            { RandomEventType.TaborHelps, RandomEvents.TaborHelps },
            { RandomEventType.TaborDemands, RandomEvents.TaborDemands },
            { RandomEventType.SplitUp, RandomEvents.SplitUp },
        };

    public void ShowEventWindow(RandomEventType type = RandomEventType.HussitPreacher)
    {
        currentEvent = events[type];
        _image.gameObject.SetActive(true);
    }

    public void ClickOk()
    {
        return;
    }
    

    public void HideRandomEvent()
    {
        _image.gameObject.SetActive(false);
    }

    public void Update()
    {
        eventText.text = currentEvent.text;
        eventName.text = "Plot event";
    }
}
