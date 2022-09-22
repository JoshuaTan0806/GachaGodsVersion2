using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

public class AI : MonoBehaviour
{
    CharacterStats stats;
    Animator animator;

    CharacterStats target;

    bool canChooseAction = true;

    public System.Action OnAttack;
    public System.Action OnSpellCast;

    float GetStat(Stat stat) => stats.GetStat(stat);

    List<CharacterStats> enemies => stats.Enemies;
    List<CharacterStats> allies => stats.Allies;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
        //animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        stats.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        stats.OnDeath -= OnDeath;
    }

    void Update()
    {
        if (!BattleManager.battleHasStarted)
            return;

        ReduceSpellCooldown();

        if (enemies.Count == 0)
            return;

        if (stats.IsDead())
            return;

        if (!canChooseAction)
            return;

        if (HasTarget() && !TargetIsInRange())
            target = null;

        if (CanCastSpell())
        {
            target = FindTarget(stats.Character.Spell);

            if (HasTarget())
            {
                Cast();
                return;
            }
            else
            {
                target = FindClosestTarget(stats.Character.Spell);
                Move();
            }
        }
        else
        {
            target = FindTarget(stats.Character.Attack);

            if (HasTarget())
            {
                Attack();
                return;
            }
            else
            {
                target = FindClosestTarget(stats.Character.Attack);
                Move();
            }
        }
    }

    private void ReduceSpellCooldown()
    {
        stats.RemoveStat(StatManager.CreateStat(Stat.CurrentSpellCD, Time.deltaTime));
        stats.CooldownBar.fillAmount = GetStat(Stat.CurrentSpellCD) / GetStat(Stat.SpellCD);
    }

    bool HasTarget()
    {
        return target != null && !target.IsDead();
    }

    bool TargetIsInRange()
    {
        return true; 
            //Vector3.SqrMagnitude(transform.position - target.transform.position) < GetStat(Stat.Range);
    }

    bool CanCastSpell()
    {
        return GetStat(Stat.CurrentSpellCD) <= 0;
    }

    void Attack()
    {
        OnAttack?.Invoke();
        //animator.Play("Attack");
        canChooseAction = false;
        StartCoroutine(AllowAction(1 / stats.Character.Attack.ActionSpeed * GetStat(Stat.ActionSpdMult)));

        //Ability g = Instantiate(stats.Attack.Prefab, transform.position, Quaternion.identity);
        //g.Initialise(stats, attack);

        if (IsAlly(target))
            target.TakeDamage(-GetStat(Stat.DmgMult));
        else
            target.TakeDamage(GetStat(Stat.DmgMult));
    }

    void Move()
    {
        if (!HasTarget())
            return;

        //animator.Play("Move");
        transform.MoveToPosition(target.transform.position, GetStat(Stat.Spd));
    }

    void Cast()
    {
        OnSpellCast?.Invoke();
        //animator.Play("Cast");
        canChooseAction = false;
        StartCoroutine(AllowAction(1 / stats.Character.Spell.ActionSpeed * GetStat(Stat.ActionSpdMult)));
        stats.SetStat(StatManager.CreateStat(Stat.CurrentSpellCD, GetStat(Stat.SpellCD)));

        if (IsAlly(target))
            target.TakeDamage(-GetStat(Stat.SpellDmgMult));
        else
            target.TakeDamage(GetStat(Stat.SpellDmgMult));

        // g = Instantiate(stats.Spell.Prefab, transform.position, Quaternion.identity);
        //g.Initialise(stats, spell);
    }

    IEnumerator AllowAction(float time)
    {
        yield return new WaitForSeconds(time);
        canChooseAction = true;
    }

    void OnDeath()
    {
        BattleManager.KillCharacter(stats);
    }

    CharacterStats FindClosestTarget(AbilityData abilityData)
    {
        List<CharacterStats> possibleTargets;

        if (abilityData.TeamType == TeamType.Ally)
        {
            possibleTargets = new List<CharacterStats>(allies);

            if (abilityData.AllyTargetType == AllyTargetType.Others)
                possibleTargets.Remove(stats);
        }
        else
            possibleTargets = new List<CharacterStats>(enemies);

        if (possibleTargets.Count == 0)
            return null;

        return possibleTargets.OrderBy(x => Vector3.SqrMagnitude(transform.position - x.transform.position)).ToList()[0];
    }

    CharacterStats FindTarget(AbilityData abilityData)
    {
        List<CharacterStats> possibleTargets;

        if (abilityData.TeamType == TeamType.Ally)
        {
            possibleTargets = new List<CharacterStats>(allies);

            if (abilityData.AllyTargetType == AllyTargetType.Others)
                possibleTargets.Remove(stats);
        }
        else
            possibleTargets = new List<CharacterStats>(enemies);

        //if theres a range, limit possible targets to ones in range
        possibleTargets = possibleTargets.Where(x => Vector3.SqrMagnitude(transform.position - x.transform.position) < abilityData.Range * abilityData.Range).ToList();

        if (possibleTargets.Count == 0)
            return null;

        switch (abilityData.TargetType)
        {
            case TargetType.Current:
                return target;
            case TargetType.Closest:
                possibleTargets = possibleTargets.OrderBy(x => Vector3.SqrMagnitude(transform.position - x.transform.position)).ToList();
                break;
            case TargetType.Furthest:
                possibleTargets = possibleTargets.OrderByDescending(x => Vector3.SqrMagnitude(transform.position - x.transform.position)).ToList();
                break;
            case TargetType.HighestStat:
                possibleTargets = possibleTargets.OrderByDescending(x => x.GetStat(abilityData.HighestStat)).ToList();
                break;
            case TargetType.LowestStat:
                possibleTargets = possibleTargets.OrderBy(x => x.GetStat(abilityData.LowestStat)).ToList();
                break;
        }

        return possibleTargets[0];
    }

    bool IsAlly(CharacterStats stat)
    {
        return allies.Contains(stat);
    }

    public void Buff()
    {
        Condition condition = new Condition(ref OnAttack, 5);
        Condition condition1 = new Condition(ref OnSpellCast, 3);
        Condition condition2 = new Condition(ref GameManager.OnRoundEnd);
        List<Condition> conditions = new List<Condition>() { condition, condition1, condition2 };
        Buff buff = new(StatManager.CreateStat(Stat.Health, 1, 3, 100), conditions);
        stats.AddBuff(buff);
    }
}
