using System;

public static class UnitsInit
{
    public static MeleeSoldier InitInfantrySoldiers()
    {
        return new MeleeSoldier(250, 25, 2, 2, 0);
    }

    public static MeleeSoldier InitCavalrySoldiers()
    {
        return new MeleeSoldier(125, 25, 2, 20, 0);
    }

    public static DistantSoldier InitCrossbowSoldiers()
    {
        return new DistantSoldier(125, 10, 2);
    }
}