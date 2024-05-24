using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }
    public const int baseMoveLimit = 2;
    public const int baseRecruitLimit = 2;

    public TextMeshProUGUI turnsCountTMP;
    public int moveLimit;
    public int recruitLimit;

    private int remainingMoves;
    private int remainingRecruits;
    public int TurnsCount { get; private set; } = 1;

    public bool EndTurnToCompute { get; set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartTurn();
    }

    public void StartTurn()
    {
        remainingMoves = moveLimit;
        remainingRecruits = recruitLimit;
    }

    public bool CanMove() => remainingMoves > 0;
    public bool CanRecruit() => remainingRecruits > 0;

    public void UseMove()
    {
        if (CanMove())
        {
            remainingMoves--;
        }
    }

    public void UseRecruit()
    {
        if (CanRecruit())
        {
            remainingRecruits--;
        }
    }

    public void EndTurn()
    {
        TurnsCount++;
        turnsCountTMP.text = $"Ход: {TurnsCount}";
        EndTurnToCompute = true;
        StartTurn();
    }
}