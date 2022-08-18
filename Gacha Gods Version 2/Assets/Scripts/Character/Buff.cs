using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public Buff(StatData statData, Condition condition)
    {
        Stat = statData;

        condition.OnConditionHit -= RemoveBuff;
        condition.OnConditionHit += RemoveBuff;

        GameManager.OnRoundEnd -= RemoveBuff;
        GameManager.OnRoundEnd += RemoveBuff;
    }

    public Buff(StatData statData, List<Condition> conditions)
    {
        Stat = statData;

        for (int i = 0; i < conditions.Count; i++)
        {
            conditions[i].OnConditionHit -= RemoveBuff;
            conditions[i].OnConditionHit += RemoveBuff;
        }

        GameManager.OnGameEnd -= RemoveBuff;
        GameManager.OnGameEnd += RemoveBuff;
    }

    public Buff(StatData statData, float timeInSeconds)
    {
        Stat = statData;
        CoroutineManager.instance.StartCoroutine(ExpireAfterSeconds(timeInSeconds));
    }

    public void RemoveBuff()
    {
        OnConditionHit?.Invoke(this);
    }

    public StatData Stat;
    public System.Action<Buff> OnConditionHit;

    public IEnumerator ExpireAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        RemoveBuff();
    }
}

public class Condition
{
    public System.Action Event;
    public int Number;

    public System.Action OnConditionHit;

    public Condition(ref System.Action Event, int Number = 1)
    {
        this.Event = Event;
        this.Number = Number;

        Event -= SubtractNumber;
        Event += SubtractNumber;
    }

    public void SubtractNumber()
    {
        Number--;

        if (Number <= 0)
            OnConditionHit?.Invoke();
    }
}