using UnityEngine;

public class PlotEvent
{
    public string name;
    public string text;

    public static PlotEvent BremenMusitians = new () 
    { 
        name = "Труппа из Бремена", 
        text = "заглушка 1"
    };

    public static PlotEvent Gutenberg = new()
    {
        name = "Пацан с железными буквами",
        text = "заглушка 2"
    };

    public static PlotEvent Witcher = new()
    {
        name = "Охотник с кошачьими глазами",
        text = "заглушка 3"
    };

    public static PlotEvent Copernicus = new()
    {
        name = "И все-таки она вертится!",
        text = "заглушка 4",
    };

    public static PlotEvent Kuplinov = new()
    {
        name = "Гусь-матершинник",
        text = "заглушка 5"
    };
}