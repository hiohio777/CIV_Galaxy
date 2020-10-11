using UnityEngine;

public class Bombs : AttackerAbility
{
    [SerializeField, Header("Урон инустрии(%)"), Range(0, 500)] private int minAttackIndustry;
    [SerializeField, Header("Урон инустрии(Рандомный)(%)"), Range(0, 500)] private int randomAttackIndustry;

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

            civilizationTarget.Shake(0.5f, 15);
            civilizationTarget.IndustryCiv.Points -= damege;
            // Нельзя нанести урон больше чем защищают щиты
            if (civilizationTarget.IndustryCiv.Points * 100f < civilizationTarget.IndustryCiv.Shields)
                civilizationTarget.IndustryCiv.Points = civilizationTarget.IndustryCiv.Shields / 100f;
        }
    }

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

    public override string GetInfo(bool isPlayer = true)
    {
        string info = string.Empty;

        if (isPlayer)
        {
            info += GetInfoCountUnits;
            info += $"{LocalisationGame.Instance.GetLocalisationString("industry_damage")}: <color=lime>{minAttackIndustry}</color>";
            if (randomAttackIndustry > 0) info += $" - <color=lime>{randomAttackIndustry + minAttackIndustry}</color>\r\n";
        }

        return info;
    }
}
