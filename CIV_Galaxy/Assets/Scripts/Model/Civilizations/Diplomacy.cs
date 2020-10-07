using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zenject;

public class Diplomacy
{
    private List<ICivilizationAl> _anotherCivilization;
    private ICivilizationPlayer _player;
    private List<DiplomaticRelations> relations = new List<DiplomaticRelations>();
    private int _dangerPlayer; // Настройка сложности(агрессивность Al по отношению к игроку)
    private IGalaxyUITimer _galaxyUITimer;

    public DiplomaticRelations this[int index] {
        get {
            return relations[index];
        }
        set {
            relations[index] = value;
        }
    }
    public int Count => relations.Count;
    public IEnumerator GetEnumerator() => relations.GetEnumerator();

    [Inject]
    public void Inject(List<ICivilizationAl> anotherCivilization, ICivilizationPlayer player, PlayerSettings playerSettings, IGalaxyUITimer galaxyUITimer)
    {
        this._anotherCivilization = anotherCivilization;
        this._player = player;
        this._galaxyUITimer = galaxyUITimer;
        _dangerPlayer = playerSettings.GetDifficultSettings.DangerPlayer;
    }

    public void Initialize(ICivilizationAl civilization)
    {
        foreach (var item in _anotherCivilization.Where(x => x.DataBase.Name != civilization.DataBase.Name).ToList())
            relations.Add(new DiplomaticRelations(civilization, item, _dangerPlayer, _galaxyUITimer));
        relations.Add(new DiplomaticRelations(civilization, _player, _dangerPlayer, _galaxyUITimer));
    }

    // Изменить отношение к данной цивилизации
    public void ChangeRelations(ICivilization civTarget, int count)
    {
        foreach (var item in relations)
        {
            if (item.AnotherCiv == civTarget)
                item.ChangeRelations(count);
        }
    }

    public ICivilization FindEnemy(AttackerAbility ability)
    {
        foreach (var item in relations)
            item.CalculateDanger(ability);

        DiplomaticRelations enemy = relations[0];
        for (int i = 1; i < relations.Count; i++)
            if (relations[i].Danger > enemy.Danger)
                enemy = relations[i];

        if (enemy.Danger > 0)
            return enemy.AnotherCiv;
        return null;
    }

    public ICivilization FindFriend()
    {
        var civsFriend = relations.Where(x => x.AnotherCiv.IsOpen && x.Relation == DiplomaticRelationsEnum.Friendship).ToList();

        if (civsFriend.Count > 0)
            return civsFriend[UnityEngine.Random.Range(0, civsFriend.Count)].AnotherCiv;
        else return null;
    }

    /// <summary>
    /// Узнать отношение к игроку
    /// </summary>
    public DiplomaticRelationsEnum GetRelationsPlayer()
    {
        return relations.Where(x => x.AnotherCiv is ICivilizationPlayer).FirstOrDefault().Relation;
    }
}
