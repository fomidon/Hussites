using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IUniversalSoldier
{
    public IMeleeSoldier meleeData { get; set; }

    public IDistantSoldier distantData { get; set; }

    public UnitsType unitType { get; }

    public bool meleeFlag { get; }

    public bool TryToMelee(out IMeleeSoldier melee)
    {
        melee = meleeData;
        return meleeFlag;
    }

    public bool TryToDistant(out IDistantSoldier distant)
    {
        distant = distantData;
        return !meleeFlag;
    }

    public int GetSoldiersNumber()
    {
        if (meleeFlag)
        {
            return meleeData.NumberOfPeople;
        }
        else
        {
            return distantData.NumberOfPeople;
        }
    }
}