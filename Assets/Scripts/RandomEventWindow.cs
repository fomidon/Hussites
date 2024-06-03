using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class RandomEventWindow : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] public Player _player;
    [SerializeField] public TMP_Text _regionName;
    [SerializeField] public RandomEvent currentEvent = RandomEvents.HussitPreacher;

    [SerializeField] public TMP_Text eventText;
    [SerializeField] public TMP_Text firstButtonText;
    [SerializeField] public TMP_Text secondButtonText;
    [SerializeField] public TMP_Text thirdButtonText;

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

    public void FirstButtonClick()
    {
        if (!currentEvent.canFirstButtonClick(_player)) 
        {
            return;
        }
        currentEvent.onFirstButtonClick(_player);
        HideRandomEvent();
    }

    public void SecondButtonClick()
    {
        if (!currentEvent.canSecondButtonClick(_player))
        {
            return;
        }
        currentEvent.onSecondButtonClick(_player);
        HideRandomEvent();
    }

    public void ThirdButtonClick()
    {
        if (!currentEvent.canThirdButtonClick(_player))
        {
            return;
        }
        currentEvent.onThirdButtonClick(_player);
        HideRandomEvent();
    }

    public void HideRandomEvent()
    {
        _image.gameObject.SetActive(false);
    }

    public void Update()
    {
        eventText.text = currentEvent.text;
        firstButtonText.text = currentEvent.firstButtonText;
        secondButtonText.text = currentEvent.secondButtonText;
        thirdButtonText.text = currentEvent.thirdButtonText;
    }
}