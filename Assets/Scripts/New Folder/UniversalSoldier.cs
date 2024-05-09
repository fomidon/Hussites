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

    public UniversalSoldier(IMeleeSoldier meleeData, IDistantSoldier distantData, bool meleeFlag)
    {
        this.meleeData = meleeData;
        this.distantData = distantData;
        this.meleeFlag = meleeFlag;
    }

    public UniversalSoldier(IMeleeSoldier meleeData) 
    {
        this.meleeData = meleeData;
        meleeFlag = true;
    }

    public UniversalSoldier(IDistantSoldier distantData)
    {
        this.distantData = distantData;
        meleeFlag = false;
    }
}
