using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/New Quest Goal", fileName = "New Quest Goal")]

public class EliminateGoal : QuestGoal
{
    public override string GetDescription()
    {
        return $"Eliminate {requiredAmount} monsters";
    }

    /*private void OnEliminate(EliminateGameEvent eventInfo)
    {
        
    }*/

}
