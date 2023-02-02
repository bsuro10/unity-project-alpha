using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalCompletedEvent : UnityEvent { }

public abstract class QuestGoal : ScriptableObject
{
    protected string description;
    public int currentAmount { get; private set; }
    public int requiredAmount;
    public bool isCompleted { get; protected set; }
    [HideInInspector] public GoalCompletedEvent OnGoalCompleted;

    public virtual string GetDescription()
    {
        return description;
    }

    public virtual void Initialize()
    {
        isCompleted = false;
        OnGoalCompleted = new GoalCompletedEvent();
    }

    protected void Evaluate()
    {
        if (currentAmount >= requiredAmount)
        {
            Complete();
        }
    }

    private void Complete()
    {
        isCompleted = true;
        OnGoalCompleted.Invoke();
        OnGoalCompleted.RemoveAllListeners();
    }
}
