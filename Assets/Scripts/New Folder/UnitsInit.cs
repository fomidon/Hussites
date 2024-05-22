using System;

public static class UnitsInit
{
    public static MeleeSoldier InitInfantrySoldiers(double basicHealth = -1)
    {
        var unit = new MeleeSoldier(250, 25, 2, 2, 0);
        if (basicHealth != -1)
        {
            unit.SetHealthManually(basicHealth);
        }
        return unit;
    }

    public static MeleeSoldier InitCavalrySoldiers(double basicHealth = -1)
    {
        var unit = new MeleeSoldier(125, 25, 2, 20, 0);
        if (basicHealth != -1)
        {
            unit.SetHealthManually(basicHealth);
        }
        return unit;
    }

    public static DistantSoldier InitCrossbowSoldiers(double basicHealth = -1)
    {
        var unit = new DistantSoldier(125, 10, 2);
        if (basicHealth != -1)
        {
            unit.SetHealthManually(basicHealth);
        }
        return unit;
    }
}