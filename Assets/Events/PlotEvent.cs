using UnityEngine;

public class PlotEvent
{
    public string name;
    public string text;

    public static PlotEvent BremenMusitians = new () 
    { 
        name = "������ �� �������", 
        text = "�������� 1"
    };

    public static PlotEvent Gutenberg = new()
    {
        name = "����� � ��������� �������",
        text = "�������� 2"
    };

    public static PlotEvent Witcher = new()
    {
        name = "������� � ��������� �������",
        text = "�������� 3"
    };

    public static PlotEvent Copernicus = new()
    {
        name = "� ���-���� ��� ��������!",
        text = "�������� 4",
    };

    public static PlotEvent Kuplinov = new()
    {
        name = "����-�����������",
        text = "�������� 5"
    };
}