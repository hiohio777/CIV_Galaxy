using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class Diplomacy
{
    private List<ICivilizationAl> _anotherCivilization;
    private ICivilizationPlayer _player;
    private List<DiplomaticRelations> relations = new List<DiplomaticRelations>();

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
    public void Inject(List<ICivilizationAl> anotherCivilization, ICivilizationPlayer player)
    {
        this._anotherCivilization = anotherCivilization;
        this._player = player;
    }

    public void Initialize(ICivilizationAl civilization)
    {
        foreach (var item in _anotherCivilization.Where(x => x.DataBase.Name != civilization.DataBase.Name).ToList())
            relations.Add(new DiplomaticRelations(item).Initialize(civilization));
        relations.Add(new DiplomaticRelations(_player).Initialize(civilization));
    }

    public void DefineRelation()
    {
        relations.ForEach(x => x.ChangeRelations());
    }

    public ICivilization FindEnemy()
    {
        foreach (var item in relations)
            item.CalculateDanger();

        DiplomaticRelations enemy = relations[0];
        for (int i = 1; i < relations.Count; i++)
            if (relations[i].Danger > enemy.Danger)
                enemy = relations[i];

        if (enemy.Danger > 0)
            return enemy.AnotherCiv;
        return null;
    }
}
