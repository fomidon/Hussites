using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DistantSoldier : IDistantSoldier
{
    public int BaseAmmunition { get; } = 20;

    public int CurrentAmmunition { get; private set; }

    public double MaxHealth { get; private set; }

    public int MaxPeopleNumber { get; private set; }

    public double GlobalHealth { get; private set; }

    public double Health { get => GlobalHealth - HealthLossesOnBattle; }

    public int NumberOfPeople { get => (int)Math.Ceiling(Health / MaxHealth * MaxPeopleNumber); }

    public double SingleSoldierDamage { get; private set; }

    public double Damage { get => SingleSoldierDamage * NumberOfPeople; }

    public double HealthLossesOnBattle { get; private set; } = 0;

    public void GetDamage(double damage)
    {
        HealthLossesOnBattle += Math.Min(damage, Health);
    }

    public void CalculateLosses()
    {
        GlobalHealth -= HealthLossesOnBattle / 2;
        HealthLossesOnBattle = 0;
    }

    public DistantSoldier(int MaxPeopleNumber, double SingleSoldierHealth, double SingleSoldierDamage)
    {
        this.MaxPeopleNumber = MaxPeopleNumber;
        MaxHealth = SingleSoldierHealth * MaxPeopleNumber;
        GlobalHealth = MaxHealth;
        this.SingleSoldierDamage = SingleSoldierDamage;
        CurrentAmmunition = BaseAmmunition;
    }

    public void Volley()
    {
        CurrentAmmunition--;
    }

    public void SetHealthManually(double health)
    {
        if (health > MaxHealth)
        {
            throw new Exception("Слишком много здоровья у юнита");
        }
        GlobalHealth = health;
    }
}
