using System;

public class TutorialStep
{
    public string Instruction; // Текст инструкции
    public Action StepAction; // Действие, которое выполняется на этом шаге
    public Func<bool> CompletionCondition; // Условие завершения шага
}