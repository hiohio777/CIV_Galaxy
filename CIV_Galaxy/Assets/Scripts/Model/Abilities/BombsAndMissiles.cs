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
        // Урон по индустрии
        civilizationTarget.IndustryCiv.Points -= (civilizationTarget.IndustryCiv.Points / 100f ) * (minAttackIndustry + UnityEngine.Random.Range(0, randomAttackIndustry));
    }
}
