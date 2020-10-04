using UnityEngine;

public class BombsAndMissiles : AttackerAbility
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
            (civilizationTarget as ICivilizationAl).DiplomacyCiv.ChangeRelations(ThisCivilization, +2);

        // Урон по индустрии
        if (civilizationTarget.IndustryCiv.Points * 100f > civilizationTarget.IndustryCiv.Shields)
        {
            // Щиты пробиты
            civilizationTarget.IndustryCiv.Points -= (civilizationTarget.IndustryCiv.Points / 100f) * (minAttackIndustry + UnityEngine.Random.Range(0, randomAttackIndustry));
            // Нельзя нанести урон больше чем защищают щиты
            if (civilizationTarget.IndustryCiv.Points * 100f < civilizationTarget.IndustryCiv.Shields)
                civilizationTarget.IndustryCiv.Points = civilizationTarget.IndustryCiv.Shields / 100f;
        }
    }
}
