using System;
using UnityEngine;
using Zenject;

public class UnitAbility : UnitBase, IUnitAbility
{
    [SerializeField] private SpriteRenderer frame, art;

    public class Factory : PlaceholderFactory<Action<object>, UnitAbility> { }

    public UnitAbility Initialize(AttackerAbility ability, Vector3 startPosition, TypeDisplayAbilityEnum type)
    {
        gameObject.SetActive(true);

        transform.position = startPosition;
        transform.localScale = new Vector3(0, 0, 0);

        frame.sprite = ability.Frame;
        art.sprite = ability.Art;
        switch (type)
        {
            case TypeDisplayAbilityEnum.PlayerTarget:
                InitTypeDisplay(new Color(1, 0, 0, 0.8f));
                break;
            case TypeDisplayAbilityEnum.PlayerAttack:
                InitTypeDisplay(new Color(0, 1, 0, 0.8f));
                break;
            case TypeDisplayAbilityEnum.Al:
                frame.enabled = false;
                art.color = new Color(0.6f, 0.6f, 0.6f, 1);
                art.sortingOrder = GetSortingOrder - 20;
                break;
        }

        return this;
    }

    private void InitTypeDisplay(Color color)
    {
        frame.enabled = true;
        frame.sortingOrder = GetSortingOrder;
        art.sortingOrder = GetSortingOrder + 1;
        art.color = Color.white;

        frame.color = color;
    }

    #region IUnitAbility

    #endregion
}
