using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IMeleeSoldier: ISoldier
{
    public double ChargeCoefficient { get; }

    public double ChargeResistance { get; }
}
