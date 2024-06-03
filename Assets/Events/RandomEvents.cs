using UnityEngine;

public class RandomEvents
{
    public static RandomEvent HussitPreacher = new()
    {
        text = "�������� ���������� ������������",

        firstButtonText = "��������",
        canFirstButtonClick = x => x.Piety > 2,
        onFirstButtonClick = x => x.ModifyPiety(-2),

        secondButtonText = "��������",
        canSecondButtonClick = x => true,
        onSecondButtonClick = x => x.ModifyPiety(2),

        thirdButtonText = "������������",
        canThirdButtonClick = x => x.Gold > 2000,
        onThirdButtonClick = (Player pl) => { pl.ModifyGold(-2000); pl.ModifyPiety(4); }
    };

    public static RandomEvent TaborDemands = new()
    {
        text = "����� ������� ������",

        firstButtonText = "����������",
        canFirstButtonClick = x => x.Gold >= 10000,
        onFirstButtonClick = x => x.ModifyGold(-10000),

        secondButtonText = "���� �����������",
        canSecondButtonClick = x => true,
        onSecondButtonClick = x => x.army.HireRecruits(-x.army.RecruitsCount),

        thirdButtonText = "��������� �������",
        canThirdButtonClick = x => x.Gold >= 5000,
        onThirdButtonClick = x =>
        {
            x.ModifyGold(-5000);
            x.army.HireRecruits(-(x.army.RecruitsCount / 2));
        }
    };

    public static RandomEvent SplitUp = new()
    {
        text = "����� ������",

        firstButtonText = "������ �� ����!",
        canFirstButtonClick = x => true,
        onFirstButtonClick = x => { },

        secondButtonText = "������� �����",
        canSecondButtonClick = x => true,
        onSecondButtonClick = x => x.ModifyGold(5000),

        thirdButtonText = "���������� �������",
        canThirdButtonClick = x => true,
        onThirdButtonClick = x => x.ModifyPiety(4)
    };

    public static RandomEvent TaborHelps = new()
    {
        text = "����� ���������� ������",

        firstButtonText = "������ ��������",
        canFirstButtonClick = x => true,
        onFirstButtonClick = x => x.ModifyGold(8000),

        secondButtonText = "������� ���������",
        canSecondButtonClick = x => x.army.CanHireRecruits(1),
        onSecondButtonClick = (Player pl) =>
        {
            pl.army.HireRecruits(1);
            pl.army.TrainRecruit("���������");
        },

        thirdButtonText = "������� ������������",
        canThirdButtonClick = x => x.army.CanHireRecruits(1),
        onThirdButtonClick = (Player pl) =>
        {
            pl.army.HireRecruits(1);
            pl.army.TrainRecruit("������� ���");
        }
    };

    public static RandomEventType GetRandomEvent()
    {
        var generator = new System.Random();
        var number = generator.Next(1, 5);
        switch (number)
        {
            case 1:
                return RandomEventType.HussitPreacher;
            case 2:
                return RandomEventType.TaborDemands;
            case 3:
                return RandomEventType.SplitUp;
            case 4:
                return RandomEventType.TaborHelps;
        }
        return RandomEventType.HussitPreacher;
    }
}