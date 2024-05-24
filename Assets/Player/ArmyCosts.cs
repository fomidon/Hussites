public class ArmyCosts
{
    public const int InfantryCost = 8000;
    public const int CavalryCost = 16000;
    public const int CrossbowMenCost = 16000;
    public const int TrophyCoeff = 5;
    public const int MaintenanceCoeff = 20;

    public int InfantryTrophy { get => InfantryCost / TrophyCoeff; }
    public int CavalryTrophy { get => CavalryCost / TrophyCoeff; }
    public int CrossbowMenTrophy { get => CrossbowMenCost / TrophyCoeff; }

    public int InfantryMaintenance { get => InfantryCost / MaintenanceCoeff; }
    public int CavalryMaintenance { get => CavalryCost / MaintenanceCoeff; }
    public int CrossbowMenMaintenance { get => CrossbowMenCost / MaintenanceCoeff; }
}