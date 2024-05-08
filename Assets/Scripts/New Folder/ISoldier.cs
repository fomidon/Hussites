using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ISoldier
{
    public double Health { get; }
    
    public double Damage { get; }

    public void GetDamage(double damage);
    
    public double HealthLossesOnBattle {  get; }

    public void CalculateLosses();

    public int NumberOfPeople { get; }
}