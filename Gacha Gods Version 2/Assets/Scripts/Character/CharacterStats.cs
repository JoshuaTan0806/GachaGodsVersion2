using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterStats : MonoBehaviour
{
    public StatDictionary Stats => stats;
    StatDictionary stats = new StatDictionary();
    [ReadOnly, SerializeField] StatFloatDictionary totalStats = new StatFloatDictionary();
    public System.Action<Stat> OnStatsChanged;

    public Character Character => character;
    Character character;

    List<Buff> buffs = new List<Buff>();
    public System.Action OnDeath;
    public System.Action OnKill;

    public List<AbilityData> Attacks => attacks;
    List<AbilityData> attacks;
    public List<AbilityData> Spells => spells;
    List<AbilityData> spells;
    public List<Trait> Traits => traits;
    List<Trait> traits = new();

    public bool RoundHasEnded => roundHasEnded;
    bool roundHasEnded = false;

    [Header("References")]
    [SerializeField] UnityEngine.UI.Image healthBar;
    public UnityEngine.UI.Image HealthBar => healthBar;
    [SerializeField] TMPro.TextMeshProUGUI HealthLabel;

    [SerializeField] UnityEngine.UI.Image cooldownBar;
    public UnityEngine.UI.Image CooldownBar => cooldownBar;

    public List<CharacterStats> Enemies => enemies;
    List<CharacterStats> enemies;
    public List<CharacterStats> Allies => allies;
    List<CharacterStats> allies;

    public Node currentNode => pathfinder.start;
    Pathfinder pathfinder;
    
    private void Awake()
    {
        pathfinder = GetComponent<Pathfinder>();
    }

    private void OnEnable()
    {
        OnStatsChanged += ResetHealthBar;
        GameManager.OnRoundEnd += EndRound;
    }

    private void OnDisable()
    {
        OnStatsChanged -= ResetHealthBar;
        GameManager.OnRoundEnd -= EndRound;
    }

    public void InitialiseCharacter(Character character)
    {
        this.character = character;
        GetComponent<CharacterAppearance>().Initialise(character);

        foreach (var item in character.BaseStats)
        {
            AddStat(StatManager.CreateStat(item.Key, item.Value));
        }

        foreach (var item in character.Traits)
        {
            AddTrait(item);
        }
  
        AddStat(StatManager.CreateStat(Stat.CurrentHealth, GetStat(Stat.Health)));
        AddStat(StatManager.CreateStat(Stat.CurrentSpellCD, GetStat(Stat.SpellCD)));

        attacks = new List<AbilityData>(character.Attacks);
        spells = new List<AbilityData>(character.Spells);

        allies = BattleManager.FindAllies(this);
        enemies = BattleManager.FindEnemies(this);

        if (enemies == BattleManager.targetableEnemies)
            healthBar.color = Color.green;
        else
            healthBar.color = Color.red;
    }

    public float GetStat(Stat stat)
    {
        return stats.ContainsKey(stat) ? stats[stat].Total : StatManager.NullStat(stat).Total;
    }

    public void AddStat(StatData stat)
    {
        if(stat.StatRequirement == StatRequirement.Trait)
        {
            if (!traits.Contains(stat.Trait))
                return;
        }
        else if (stat.StatRequirement == StatRequirement.Character)
        {
            if (character != stat.Character)
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

        OnStatsChanged?.Invoke(stat.Stat);
    }

    public void SetStat(StatData stat)
    {
        stats[stat.Stat] = stat;
        OnStatsChanged?.Invoke(stat.Stat);
    }

    public void AddTrait(Trait trait)
    {
        if (!traits.Contains(trait))
            traits.Add(trait);
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

        OnStatsChanged?.Invoke(stat.Stat);
    }

    public bool IsDead()
    {
        return GetStat(Stat.CurrentHealth) <= 0;
    }

    public void UpgradeAttack(List<AbilityData> attacks)
    {
        this.attacks = new List<AbilityData>(attacks);
    }

    public void UpgradeSpell(List<AbilityData> spells)
    {
        this.spells = new List<AbilityData>(spells);
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

        RemoveStat(StatManager.CreateStat(Stat.CurrentHealth, damage));

        //this is a clamp
        if (GetStat(Stat.CurrentHealth) < 0)
            SetStat(StatManager.CreateStat(Stat.CurrentHealth));
        else if(GetStat(Stat.CurrentHealth) > GetStat(Stat.Health))
            SetStat(StatManager.CreateStat(Stat.CurrentHealth, GetStat(Stat.Health)));

        if (IsDead())
            OnDeath?.Invoke();
    }

    void ResetHealthBar(Stat stat)
    {
        if (stat != Stat.Health && stat != Stat.CurrentHealth)
            return;

        HealthBar.fillAmount = GetStat(Stat.CurrentHealth) / GetStat(Stat.Health);

        float currentHealth = Mathf.Round(GetStat(Stat.CurrentHealth));
        float maxHealth = Mathf.Round(GetStat(Stat.Health));

        HealthLabel.text = currentHealth + "/" + maxHealth;
    }

    void EndRound()
    {
        roundHasEnded = true;
    }

    [Button]
    public void Buff()
    {
        Condition condition = new Condition(ref GameManager.OnRoundEnd, 2);
        Buff buff = new(StatManager.CreateStat(Stat.Health, 100), condition);
        AddBuff(buff);
    }
}
