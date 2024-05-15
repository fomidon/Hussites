using System;

public class SoldierUnitsInitialization
{
    public static MeleeSoldier MakeRecruit()
    {
        return new MeleeSoldier(250, 10, 1, 2, 0);
    }

    public static MeleeSoldier MakeLightInfantry()
    {
        return new MeleeSoldier(250, 25, 2, 2, 0);
    }

    public static MeleeSoldier MakeLightCavalry()
    {
        return new MeleeSoldier(125, 25, 2, 20, 0);
    }

    public static DistantSoldier MakeCrossbowMan()
    {
        return new DistantSoldier(125, 10, 1);
    }
}