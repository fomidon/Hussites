using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UniversalSoldier : IUniversalSoldier
{
    public IMeleeSoldier? meleeData { get; set; }
    public IDistantSoldier? distantData { get; set; }

    public bool meleeFlag { get; set; }
    public UnitsType unitType { get; }

    public UniversalSoldier(IMeleeSoldier meleeData, IDistantSoldier distantData, bool meleeFlag)
    {
        this.meleeData = meleeData;
        this.distantData = distantData;
        this.meleeFlag = meleeFlag;

        if (meleeFlag)
        {
            unitType = meleeData.unitType;
        }
        else
        {
            unitType = distantData.unitType;
        }
    }

    public UniversalSoldier(IMeleeSoldier meleeData)
    {
        this.meleeData = meleeData;
        meleeFlag = true;
        unitType = meleeData.unitType;
    }

    public UniversalSoldier(IDistantSoldier distantData)
    {
        this.distantData = distantData;
        meleeFlag = false;
        unitType = distantData.unitType;
    }
}