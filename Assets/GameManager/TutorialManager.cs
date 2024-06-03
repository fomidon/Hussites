using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private List<TutorialStep> steps;
    private bool isEnded;

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
                Instruction = "Переместитесь на соседнюю провинцию",
                StepAction = () => Debug.Log(""),
                CompletionCondition = PlayerMovedToNextRegion
            },
            // Второй шаг: Нанять новобранцев
            new()
            {
                Instruction = "Наймите своих первых новобранцев",
                StepAction = () =>
                {
                    Debug.Log("стрелка на новобранцев");
                },
                CompletionCondition = PlayerHiredRecruits
            },
            // Третий шаг: Сдать ход
            new()
            {
                Instruction = "Чтобы пойти дальше, вам нужно завершить ход. Сделайте это",
                StepAction = () => Debug.Log("\"зажечь\" или переместить стрелочку на кнопку сдать ход"),
                CompletionCondition = PlayerEndedTurn
            },
            // Четвертый шаг: Переместиться на второй указанный регион
            new()
            {
                Instruction = "Двигайтесь в Прагу.",
                StepAction = () => Debug.Log("фокус и стрелка на регион"),
                CompletionCondition = PlayerMovedToNextRegion
            },
            // Пятый шаг: Улучшить новобранцев до пехоты
            new()
            {
                Instruction = "Улучшите новобранцев до пехоты.",
                StepAction = () => Debug.Log("стрелка на кнопку улучшить"),
                CompletionCondition = PlayerUpgradedRecruits
            },
            // Шестой шаг: Сдать ход
            new()
            {
                Instruction = "Сдайте ход.",
                StepAction = () => Debug.Log("\"зажечь\" стрелочку на кнопку сдать ход"),
                CompletionCondition = PlayerEndedTurn
            },
            // Седьмой шаг: выйти из города
            new()
            {
                Instruction = "Переместитесь на соседнюю провинцию",
                StepAction = () => Debug.Log(""),
                CompletionCondition = PlayerMovedToNextRegion
            },
            
            // Восьмой шаг: Внезапное нападение
            new()
            {
                Instruction = "На вас нападают! После боя обучение будет завершено.",
                StepAction = () => fightWindow.ShowFightWindow(),
                CompletionCondition = () => true
            }
        };
    }

    private void ShowCurrentStep()
    {
        if (currentStepIndex < steps.Count)
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
            // TODO: потушить окошко с инструкциями
            
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
        // TODO: логика отображения инструкции на экране
        Debug.Log(instruction);
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