using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterStats : MonoBehaviour
{
    public StatDictionary Stats => stats;
    StatDictionary stats = new StatDictionary();
    [ReadOnly, SerializeField] StatFloatDictionary totalStats = new StatFloatDictionary();
    public System.Action OnStatsChanged;
    public Character Character => character;
    [SerializeField] Character character;

    List<Buff> buffs = new List<Buff>();
    public System.Action OnDeath;

    public Attack Attack => attack;
    Attack attack;
    public Spell Spell => spell;
    Spell spell;

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
            OnHealthChanged?.Invoke();
        }
    }
    float currentHealth;
    public static System.Action OnHealthChanged;
    public float CurrentMana
    {
        get
        {
            return currentMana;
        }
        set
        {
            currentMana = value;
            OnManaChanged?.Invoke();
        }
    }
    float currentMana;
    public static System.Action OnManaChanged;

    public bool RoundHasEnded => roundHasEnded;
    bool roundHasEnded = false;

    [Header("References")]
    [SerializeField] UnityEngine.UI.Image healthBar;
    public UnityEngine.UI.Image HealthBar => healthBar;
    [SerializeField] TMPro.TextMeshProUGUI HealthLabel;

    private void OnEnable()
    {
        OnHealthChanged += ResetHealthBar;
        OnStatsChanged += ResetHealthBar;
        GameManager.OnRoundEnd += EndRound;
    }

    private void OnDisable()
    {
        OnHealthChanged -= ResetHealthBar;
        OnStatsChanged -= ResetHealthBar;
        GameManager.OnRoundEnd -= EndRound;
    }

    void Start()
    {
        foreach (var item in StatManager.StatDictionary)
        {
            AddStat(item.Value);
        }

        foreach (var item in Character.BaseStats)
        {
            AddStat(StatManager.CreateStat(item.Key, item.Value));
        }

        CurrentHealth = stats[Stat.Health].Total;
    }

    public void InitialiseCharacter(Character character)
    {
        this.character = character;
        spell = character.Spell;
        attack = character.Attack;
    }

    public float GetStat(Stat stat)
    {
        return stats.ContainsKey(stat) ? stats[stat].Total : StatManager.NullStat(stat).Total;
    }

    public void AddStat(StatData stat)
    {
        if(stat.StatRequirement == StatRequirement.Archetype)
        {
            if (!Character.Archetypes.Contains(stat.Archetype))
                return;
        }
        else if (stat.StatRequirement == StatRequirement.Role)
        {
            if (!Character.Roles.Contains(stat.Role))
                return;
        }

        if (stats.ContainsKey(stat.Stat))
        {
            stats[stat.Stat] += stat;
        }
        else
        {
            stats.Add(stat.Stat, StatManager.NullStat(stat.Stat));
            stats[stat.Stat] += stat;
        }

        totalStats[stat.Stat] = stats[stat.Stat].Total;

        OnStatsChanged?.Invoke();
    }

    public void RemoveStat(StatData stat)
    {
        if (stats.ContainsKey(stat.Stat))
        {
            stats[stat.Stat] -= stat;
        }
        else
        {
            stats.Add(stat.Stat, StatManager.NullStat(stat.Stat));
            stats[stat.Stat] -= stat;
        }

        totalStats[stat.Stat] = stats[stat.Stat].Total;

        OnStatsChanged?.Invoke();
    }

    public bool IsDead()
    {
        return CurrentHealth < 0;
    }

    public void UpgradeAttack(Attack attack)
    {
        this.attack = attack;
    }

    public void UpgradeSpell(Spell spell)
    {
        this.spell = spell;
    }

    public void AddBuff(Buff buff)
    {
        if(buffs.Contains(buff))
            throw new System.Exception("Trying to add buff that character already has.");

        buffs.Add(buff);
        AddStat(buff.Stat);

        buff.OnConditionHit -= RemoveBuff;
        buff.OnConditionHit += RemoveBuff;
    }

    void RemoveBuff(Buff buff)
    {
        if (!buffs.Contains(buff))
            throw new System.Exception("Trying to remove buff that character does not have.");

        buffs.Remove(buff);
        RemoveStat(buff.Stat);
        buff.OnConditionHit -= RemoveBuff;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead())
            return;

        if (roundHasEnded)
            return;

        CurrentHealth -= damage;

        if (IsDead())
            OnDeath?.Invoke();
    }

    void ResetHealthBar()
    {
        if (!Stats.ContainsKey(Stat.Health))
            return;

        HealthBar.fillAmount = CurrentHealth / Stats[Stat.Health].Total;

        float currentHealth = Mathf.Round(CurrentHealth);
        currentHealth = Mathf.Max(0, currentHealth);
        float maxHealth = Mathf.Round(Stats[Stat.Health].Total);

        HealthLabel.text = currentHealth + "/" + maxHealth;
    }

    [Button]
    public void RandomiseColour()
    {
        Color color = new Vector4(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1);
        GetComponent<SpriteRenderer>().color = color;
    }

    [Button]
    public void Buff()
    {
        Condition condition = new Condition(ref GameManager.OnRoundEnd, 2);
        Buff buff = new(StatManager.CreateStat(Stat.Health, 100), condition);
        AddBuff(buff);
    }

    void EndRound()
    {
        roundHasEnded = true;
    }
}
