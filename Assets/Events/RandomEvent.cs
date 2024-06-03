using System;
using UnityEngine;

public class RandomEvent: MonoBehaviour
{
    public string text;

    public string firstButtonText;
    public Func<Player, bool> canFirstButtonClick;
    public Action<Player> onFirstButtonClick;

    public string secondButtonText;
    public Func<Player, bool> canSecondButtonClick;
    public Action<Player> onSecondButtonClick;

    public string thirdButtonText;
    public Func<Player, bool> canThirdButtonClick;
    public Action<Player> onThirdButtonClick;
}