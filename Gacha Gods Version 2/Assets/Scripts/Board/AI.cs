using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class AI : MonoBehaviour
{
    CharacterStats stats;
    Animator animator;

    CharacterStats target;

    bool canChooseAction = true;

    public System.Action OnAttack;
    public System.Action OnSpellCast;

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
                Cast();
        }
        else
        {
            if (!HasTarget())
                target = FindTarget(stats.Character.Attack);

            if (HasTarget())
                Attack();
        }

        if (!HasTarget())
        {
            target = FindClosestEnemy();
            Move();
        }
    }

    bool HasTarget()
    {
        return target != null && !target.IsDead();
    }

    bool TargetIsInRange()
    {
        return Vector3.SqrMagnitude(transform.position - target.transform.position) < stats.GetStat(Stat.Range);
    }

    bool CanCastSpell()
    {
        return false;
    }

    void Attack()
    {
        OnAttack?.Invoke();
        //animator.Play("Attack");
        canChooseAction = false;
        StartCoroutine(AllowAction(1 / stats.GetStat(Stat.AtkSpd)));

        //Instantiate(stats.Attack.Prefab, transform.position, Quaternion.identity);

        target.TakeDamage(stats.GetStat(Stat.Dmg));
    }

    void Move()
    {
        //animator.Play("Move");
        transform.MoveToPosition(target.transform.position, stats.GetStat(Stat.Spd));
    }

    void Cast()
    {
        OnSpellCast?.Invoke();
        //animator.Play("Cast");
        canChooseAction = false;
        StartCoroutine(AllowAction(1 / stats.GetStat(Stat.SpellSpd)));

        Instantiate(stats.Spell.Prefab, transform.position, Quaternion.identity);
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

    public void Buff()
    {
        Condition condition = new Condition(ref OnAttack, 5);
        Condition condition1 = new Condition(ref OnSpellCast, 3);
        Condition condition2 = new Condition(ref GameManager.OnRoundEnd);
        List<Condition> conditions = new List<Condition>() { condition, condition1, condition2 };
        Buff buff = new(StatManager.CreateStat(Stat.Health, 1, 3, 100), conditions);
        stats.AddBuff(buff);
    }

    CharacterStats FindClosestEnemy()
    {
        List<CharacterStats> enemies = this.enemies;

        if (enemies.Count == 0)
            return null;

        return enemies.OrderBy(x => Vector3.SqrMagnitude(transform.position - x.transform.position)).ToList()[0];
    }

    CharacterStats FindFurtherEnemy()
    {
        List<CharacterStats> enemies = this.enemies;

        if (enemies.Count == 0)
            return null;

        return enemies.OrderByDescending(x => Vector3.SqrMagnitude(transform.position - x.transform.position)).ToList()[0];
    }

    CharacterStats FindTarget(AbilityData abilityData)
    {
        if (abilityData.TargetType == TargetType.Current)
            return target;

        if (abilityData.TargetType == TargetType.Closest)
            return FindClosestEnemy();

        List<CharacterStats> possibleTargets = new();

        if (abilityData.TeamType == TeamType.Ally)
            possibleTargets = allies;
        else
            possibleTargets = enemies;

        //which ones are in range
        possibleTargets = possibleTargets.Where(x => Vector3.SqrMagnitude(transform.position - x.transform.position) < abilityData.MaxRange * abilityData.MaxRange).ToList();


        switch (abilityData.TargetType)
        {
            case TargetType.Closest:

                break;
            case TargetType.Furthest:
                break;
            case TargetType.HighestHealth:
                break;
            case TargetType.LowestHealth:
                break;
            case TargetType.HighestDensity:
                break;
            default:
                break;
        }

        return null;
    }
}
