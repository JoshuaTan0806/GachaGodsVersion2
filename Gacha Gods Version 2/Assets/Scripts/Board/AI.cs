using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    CharacterStats stats;
    Animator animator;

    CharacterStats target;

    bool canChooseAction = true;

    public System.Action OnAttack;
    public System.Action OnSpellCast;

    List<CharacterStats> enemies => stats.Enemies;

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

        if (!HasTarget())
            target = FindClosestEnemy();

        if (CanCastSpell())
            Cast();

        if (TargetIsInRange())
            Attack();
        else
            Move();
    }

    CharacterStats FindClosestEnemy()
    {
        float minDist = Mathf.Infinity;
        CharacterStats closestEnemy = null;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
                continue;

            float dist = Vector3.SqrMagnitude(transform.position - enemies[i].transform.position);

            if(dist < minDist)
            {
                minDist = dist;
                closestEnemy = enemies[i];
            }
        }

        return closestEnemy;
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

    void GetStunned(float time)
    {
        //animator.Play("Stun");
        canChooseAction = false;
        StartCoroutine(AllowAction(time));
    }

    IEnumerator AllowAction(float time)
    {
        yield return new WaitForSeconds(time);
        canChooseAction = true;
    }

    [Button]
    public void Buff()
    {
        Condition condition = new Condition(ref OnAttack, 5);
        Condition condition1 = new Condition(ref OnSpellCast, 3);
        Condition condition2 = new Condition(ref GameManager.OnRoundEnd);
        List<Condition> conditions = new List<Condition>() { condition, condition1, condition2 };
        Buff buff = new(StatManager.CreateStat(Stat.Health, 1, 3, 100), conditions);
        stats.AddBuff(buff);
    }

    void OnDeath()
    {
        BattleManager.KillCharacter(stats);
    }
}
