using System;
using UnityEngine;
using Zenject;

public class UnitAbility : UnitBase, IUnitAbility
{
    [SerializeField] private SpriteRenderer frame, art;
    protected AbilityBase _ability;

    public class Factory : PlaceholderFactory<Action<UnitBase>, UnitAbility> { }

    public UnitAbility Initialize(AbilityBase ability, Vector3 startPosition, TypeDisplayAbilityEnum type)
    {
        gameObject.SetActive(true);

        this._ability = ability;
        transform.position = startPosition;
        transform.localScale = new Vector3(0, 0, 0);

        frame.sprite = ability.Frame;
        art.sprite = ability.Art;
        frame.sortingOrder = GetSortingOrder - 1;
        art.sortingOrder = GetSortingOrder;
        switch (type)
        {
            case TypeDisplayAbilityEnum.PlayerTarget:
                frame.enabled = true;
                frame.color = new Color(1, 0, 0, 0.8f);
                art.color = Color.white;
                break;
            case TypeDisplayAbilityEnum.PlayerAttack:
                frame.enabled = true;
                frame.color = new Color(0, 1, 0, 0.8f);
                art.color = Color.white;
                break;
            case TypeDisplayAbilityEnum.Al:
                frame.enabled = false;
                art.color = new Color(0.6f, 0.6f, 0.6f, 1);
                art.sortingOrder = GetSortingOrder - 20;
                break;
        }

        return this;
    }

    #region IUnitAbility

    #endregion
}
