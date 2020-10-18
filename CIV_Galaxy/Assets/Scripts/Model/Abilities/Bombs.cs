using System;
using UnityEngine;

public class Bombs : AttackerAbility
{
    [SerializeField, Header("Урон инустрии(%)"), Range(0, 500)] private int minAttackIndustry;
    [SerializeField, Header("Урон инустрии(Рандомный)(%)"), Range(0, 500)] private int randomAttackIndustry;
    [SerializeField, Range(0, 100)] private float attackScaner, attackScience, attackAbility;
    [SerializeField] private Sprite attackScanerEffectSprite, attackScienceEffectSprite, attackAbilityEffectSprite;

    public void AddBonus_minAttackIndustry(int bonus)
    {
        minAttackIndustry += bonus;
        if (minAttackIndustry < 0) minAttackIndustry = 0;
    }
    public void AddBonus_randomAttackIndustry(int bonus)
    {
        randomAttackIndustry += bonus;
        if (randomAttackIndustry < 0) randomAttackIndustry = 0;
    }

    public void AddBonus_attackScaner(float count) => attackScaner += count;
    public void AddBonus_attackAbility(float count) => attackAbility += count;
    public void AddBonus_attackScience(float count) => attackScience += count;

    public override bool Apply(ICivilization civilizationTarget)
    {
        StartAttack(civilizationTarget);
        return true;
    }

    public override bool ApplyAl(Diplomacy diplomacyCiv)
    {
        var target = diplomacyCiv.FindEnemy(this);
        if (target != null)
        {
            StartAttack(target); // Цель найдена
            return true;
        }
        else return false;
    }

    protected override void Finall(IUnitAbility unit, ICivilization civilizationTarget)
    {
        // Ухудшить отношения, если напал игрок
        if (ThisCivilization is ICivilizationPlayer)
            (civilizationTarget as ICivilizationAl).DiplomacyCiv.ChangeRelations(ThisCivilization, +1);

        // Урон по индустрии
        if (civilizationTarget.IndustryCiv.Points * 100f > civilizationTarget.IndustryCiv.Shields)
        {
            // Щиты пробиты
            float damege = (civilizationTarget.IndustryCiv.Points / 100f) * (minAttackIndustry + UnityEngine.Random.Range(0, randomAttackIndustry));
            float shields = damege / 100 * civilizationTarget.IndustryCiv.Shields;
            damege -= shields;

            civilizationTarget.Shake(0.5f, 15);
            civilizationTarget.IndustryCiv.Points -= damege;
            // Нельзя нанести урон больше чем защищают щиты
            if (civilizationTarget.IndustryCiv.Points * 100f < civilizationTarget.IndustryCiv.Shields)
                civilizationTarget.IndustryCiv.Points = civilizationTarget.IndustryCiv.Shields / 100f;
        }

        // Урон по сканеру(приостанавливает работу)
        if(attackScaner > 0) 
        {
            float damage = attackScaner - (attackScaner / 100 * civilizationTarget.IndustryCiv.Shields); // Щиты
            civilizationTarget.ScanerCiv.Damage(damage);
            // Эффетк
            civilizationTarget.ExicuteSpecialEffect(attackScanerEffectSprite, EffectEnum.Standart);
        }
        // Урон по науке(приостанавливает работу)
        if (attackScience > 0)
        {
            float damage = attackScience - (attackScience / 100 * civilizationTarget.IndustryCiv.Shields); // Щиты
            civilizationTarget.ScienceCiv.Damage(damage);
            // Эффетк
            civilizationTarget.ExicuteSpecialEffect(attackScienceEffectSprite, EffectEnum.Standart);
        }
        // Урон по верфям(приостанавливает работу)
        if (attackAbility > 0)
        {
            float damage = attackAbility - (attackAbility / 100 * civilizationTarget.IndustryCiv.Shields); // Щиты
            civilizationTarget.AbilityCiv.Damage(damage);
            // Эффетк
            civilizationTarget.ExicuteSpecialEffect(attackAbilityEffectSprite, EffectEnum.Standart);
        }
    }

    public override string GetInfo(bool isPlayer = true)
    {
        string info = string.Empty;

        if (isPlayer)
        {
            info += GetInfoCountUnits;
            info += $"{LocalisationGame.Instance.GetLocalisationString("industry_damage")}: <color=lime>{minAttackIndustry}</color>";
            if (randomAttackIndustry > 0) info += $" - <color=lime>{randomAttackIndustry + minAttackIndustry}</color>\r\n";


            if (attackScaner > 0)
            {
                info += $"{LocalisationGame.Instance.GetLocalisationString("bombs_damage_ability")}: +{attackScaner}\r\n";
            }
            if (attackScience > 0)
            {
                info += $"{LocalisationGame.Instance.GetLocalisationString("bombs_damage_science")}: +{attackScience}\r\n";
            }
            if (attackAbility > 0)
            {
                info += $"{LocalisationGame.Instance.GetLocalisationString("bombs_damage_ability")}: +{attackAbility}\r\n";
            }
        }

        return info;
    }
}
