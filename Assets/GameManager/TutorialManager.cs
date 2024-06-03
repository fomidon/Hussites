using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;

public class Tutorial : MonoBehaviour
{
    private List<TutorialStep> steps;
    private bool isEnded;
    public bool isTutorialNeed = true;
    [SerializeField] private Image tutorialWindow;
    [SerializeField] private TMP_Text instructionText;

    [SerializeField] private List<MapRegion> regionsToVisit;
    [SerializeField] private FightWindow fightWindow;
    [SerializeField] private List<GameObject> slides;
    private Queue<MapRegion> visitQueue = new();


    private int previousInfantryCount;
    private int previousRecruitsCount;
    private int previousTurnsCount = 1;

    [SerializeField] private GameManager gameManager;
    private int currentStepIndex;

    private void Start()
    {
        SetupTutorial();
        ShowCurrentStep();
    }

    public void Restart()
    {
        slides[currentStepIndex].SetActive(false);
        tutorialWindow.gameObject.SetActive(false);
        
        previousRecruitsCount = 0;
        previousTurnsCount = 1;
        previousInfantryCount = 0;
        currentStepIndex = 0;
        isEnded = false;
        visitQueue = new();
        SetupTutorial();
        ShowCurrentStep();
    }
    
    private void SetupTutorial()
    {
        foreach (var el in regionsToVisit)
            visitQueue.Enqueue(el);
        //previousRecruitsCount = gameManager.player.army.RecruitsCount;
        //previousInfantryCount = gameManager.player.army.InfantryOutside.Count;
        
        steps = new List<TutorialStep>
        {
            // Первый шаг: Переместиться на первый указанный регион
            new()
            {
                Instruction =
                    "Добро пожаловать, командир! Несомненно, вам уготована великая судьба, однако сперва давайте познакомимся с основными механиками игры. Для начала переместитесь в соседнюю провинцию, кликнув на нее.",
                StepAction = () => {},
                CompletionCondition = PlayerMovedToNextRegion
            },
            // Второй шаг: Нанять новобранцев
            new()
            {
                Instruction =
                    "С каждой локацией в игре можно взаимодействовать. Для создания армии нужны новобранцы, нанимаемые на дружественных фермах. Наймите своих первых бойцов.",
                StepAction = () => {},
                CompletionCondition = PlayerHiredRecruits
            },
            // Третий шаг: Сдать ход
            new()
            {
                Instruction = "Чтобы пойти дальше, вам нужно завершить ход. \nСделайте это.",
                StepAction = () => {},
                CompletionCondition = PlayerEndedTurn
            },
            // Четвертый шаг: Переместиться на второй указанный регион
            new()
            {
                Instruction = "Двигайтесь в Прагу.",
                StepAction = () => {},
                CompletionCondition = PlayerMovedToNextRegion
            },
            // Пятый шаг: Улучшить новобранцев до пехоты
            new()
            {
                Instruction = "В городе вы можете обучать своих новобранцев, делая из них полноценные боевые отряды. В этот раз мы можем позволить себе только легкую пехоту.",
                StepAction = () => {},
                CompletionCondition = PlayerUpgradedRecruits
            },
            // Шестой шаг: Сдать ход
            new()
            {
                Instruction = "Завершите ход.",
                StepAction = () => {},
                CompletionCondition = PlayerEndedTurn
            },
            // Седьмой шаг: выйти из города
            new()
            {
                Instruction = "Здесь нам больше делать нечего, отправляемся.",
                StepAction = () => {},
                CompletionCondition = PlayerMovedToNextRegion
            },

            // Восьмой шаг: Внезапное нападение
            new()
            {
                Instruction = "На Богемию вновь пошли войной! Пока основные силы Табора сражаются с крестоносцами в Моравии, отразите атаку североавстрийских баронов. Увы, я должен покинуть вас до начала боя!",
                StepAction = () =>
                {
                    tutorialWindow.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 186);
                    fightWindow.ShowFightWindow();
                },
                CompletionCondition = () => !fightWindow.isWindowActivate
            }
        };
    }

    private void ShowCurrentStep()
    {
        if (isTutorialNeed && currentStepIndex < steps.Count)
        {
            // Показать инструкцию текущего шага
            DisplayInstruction(steps[currentStepIndex].Instruction);

            // Выполнить действие текущего шага
            ActivateNextSlide();
            steps[currentStepIndex].StepAction?.Invoke();
        }
        else
        {
            isEnded = true;
            slides[^1].SetActive(false);
            tutorialWindow.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!isEnded && steps[currentStepIndex].CompletionCondition())
        {
            NextStep();
        }
    }

    private void ActivateNextSlide()
    {
        if (currentStepIndex > 0)
            slides[currentStepIndex - 1].SetActive(false);
        slides[currentStepIndex].SetActive(true);
    }

    private void NextStep()
    {
        currentStepIndex++;
        ShowCurrentStep();
    }

    private void DisplayInstruction(string instruction)
    {
        if (!tutorialWindow.gameObject.activeSelf)
            tutorialWindow.gameObject.SetActive(true);
        instructionText.text = instruction;
    }

    private bool PlayerMovedToNextRegion()
    {
        if ((visitQueue.Peek().Position - gameManager.currentRegion.Position).magnitude > 0.1f)
            return false;
        visitQueue.Dequeue();
        return true;
    }

    private bool PlayerHiredRecruits()
    {
        if (previousRecruitsCount == gameManager.player.army.RecruitsCount)
            return false;
        previousRecruitsCount = gameManager.player.army.RecruitsCount;
        return true;
    }

    private bool PlayerEndedTurn()
    {
        if (previousTurnsCount == TurnManager.Instance.TurnsCount)
            return false;
        previousTurnsCount = TurnManager.Instance.TurnsCount;
        return true;
    }

    private bool PlayerUpgradedRecruits()
    {
        if (previousInfantryCount == gameManager.player.army.InfantryOutside.Count)
            return false;
        previousInfantryCount = gameManager.player.army.InfantryOutside.Count;
        return true;
    }
}