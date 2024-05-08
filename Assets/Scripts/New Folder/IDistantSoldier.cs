using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDistantSoldier : ISoldier
{     
    public int BaseAmmunition {  get; }
    public int CurrentAmmunition { get; }
    public void Volley();
}
