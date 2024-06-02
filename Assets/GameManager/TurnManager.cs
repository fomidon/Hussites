using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }
    public const int baseMoveLimit = 2;
    public const int baseRecruitLimit = 5;

    public TextMeshProUGUI turnsCountTMP;
    public int moveLimit;
    public int recruitLimit;

    private int remainingMoves;
    private int remainingRecruits;
    public int TurnsCount { get; private set; } = 1;

    public bool EndTurnToCompute { get; set; } = false;
    public bool BeginTurnToCompute { get; set; } = false;

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

    private void StartTurn()
    {
        BeginTurnToCompute = true;
        remainingMoves = moveLimit;
        remainingRecruits = recruitLimit;
    }

    public bool CanMove() => remainingMoves > 0;
    private bool CanRecruit() => remainingRecruits > 0;

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
        //turnsCountTMP.text = $"Ход: {TurnsCount}";
        EndTurnToCompute = true;
        StartTurn();
    }

    private void Update()
    {
        CheatTurn();
    }

    private void CheatTurn()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            EndTurn();
        }
    }

    public void LoadFromSave(ProgressData save)
    {
        TurnsCount = save.Turn;
        StartTurn();
    }
}