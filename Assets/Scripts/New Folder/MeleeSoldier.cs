﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MeleeSoldier : IMeleeSoldier
{
    public UnitsType unitType { get; }

    public double MaxHealth { get; private set; }

    public int MaxPeopleNumber { get; private set; }

    public double GlobalHealth { get; private set; }

    public double Health { get => GlobalHealth - HealthLossesOnBattle; }

    public int NumberOfPeople { get => (int)Math.Ceiling(Health / MaxHealth * MaxPeopleNumber); }

    public double SingleSoldierDamage { get; private set; }

    public double Damage { get => SingleSoldierDamage * NumberOfPeople * DamageModifier; }

    public double ChargeCoefficient { get; private set; }

    public double ChargeResistance { get; private set; }

    public double HealthLossesOnBattle { get; private set; }

    public double DamageModifier { get; private set; } = 1;
    public double DamageResistModifier { get; private set; } = 1;

    public void GetDamage(double damage)
    {
        HealthLossesOnBattle += Math.Min(damage * DamageResistModifier, Health);
    }

    public void CalculateLosses()
    {
        GlobalHealth -= HealthLossesOnBattle / 2;
        HealthLossesOnBattle = 0;
    }

    public MeleeSoldier(int MaxPeopleNumber, double SingleSoldierHealth, double SingleSoldierDamage,
        double ChargeCoefficient, double ChargeResistance, UnitsType unitType)
    {
        this.MaxPeopleNumber = MaxPeopleNumber;
        MaxHealth = SingleSoldierHealth * MaxPeopleNumber;
        GlobalHealth = MaxHealth;
        this.SingleSoldierDamage = SingleSoldierDamage;
        this.ChargeCoefficient = ChargeCoefficient;
        this.ChargeResistance = ChargeResistance;
        this.unitType = unitType;
        DamageModifier = 1;
        DamageResistModifier = 1;
    }

    public void SetHealthManually(double health)
    {
        if (health > MaxHealth)
        {
            throw new Exception("Слишком много здоровья у юнита");
        }
        GlobalHealth = health;
    }

    public void SetModificators(double damageModifier, double damageResistModifier)
    {
        DamageModifier = damageModifier;
        DamageResistModifier = damageResistModifier;
    }
}
