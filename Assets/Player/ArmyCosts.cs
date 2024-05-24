public class ArmyCosts
{
    public const int InfantryCost = 8000;
    public const int CavalryCost = 16000;
    public const int CrossbowMenCost = 16000;
    public const int TrophyCoeff = 5;
    public const int MaintenanceCoeff = 20;

    public static int InfantryTrophy { get => InfantryCost / TrophyCoeff; }
    public static int CavalryTrophy { get => CavalryCost / TrophyCoeff; }
    public static int CrossbowMenTrophy { get => CrossbowMenCost / TrophyCoeff; }

    public static int InfantryMaintenance { get => InfantryCost / MaintenanceCoeff; }
    public static int CavalryMaintenance { get => CavalryCost / MaintenanceCoeff; }
    public static int CrossbowMenMaintenance { get => CrossbowMenCost / MaintenanceCoeff; }
}