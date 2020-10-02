using System;
using UnityEngine;
using Zenject;

public class Planet : UnitBase, IPlanet
{
    [SerializeField] protected SpriteRenderer art;

    public class Factory : PlaceholderFactory<Action<object>, Planet> { }

    public Planet Initialize(SpriteUnitEnum typePlanet)
    {
        gameObject.SetActive(true);

        switch (typePlanet)
        {
            case SpriteUnitEnum.Ideal: art.color = Color.green; break;
            case SpriteUnitEnum.Ice: art.color = Color.blue; break;
            case SpriteUnitEnum.Hot: art.color = Color.red; break;
            case SpriteUnitEnum.GasGiants: art.color = Color.cyan; break;
        }

        transform.position = new Vector3(0, 0, 0);
        art.sortingOrder = GetSortingOrder;

        return this;
    }

    #region IPlanet
    public void OpenPlanet(Vector3 positionTarget, Action actFinish)
    {
        TtransformUnit.localScale = new Vector3(0, 0, 0);
        TtransformUnit.position = new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f), 0);

        Action endAct = () => SetScale(0f, 0.2f).Run(actFinish);
        SetScale(1.3f, 0.3f).Run(() => SetScale(1, 0.2f).Run(() => SetPosition(positionTarget, UnityEngine.Random.Range(1f, 2f)).Run(endAct)));
    }

    public void ConquestPlanets(Vector3 startPosition, Vector3 positionTarget, Action actFinish)
    {
        TtransformUnit.localScale = new Vector3(0, 0, 0);
        TtransformUnit.position = new Vector3(startPosition.x + UnityEngine.Random.Range(-30f, 30f), startPosition.y + UnityEngine.Random.Range(-30f, 30f), 0);

        float speed = UnityEngine.Random.Range(0.8f, 1f);

        Action endAct = () => SetScale(0f, 0.2f).Run(actFinish);
        Action toMoveTarget = () => SetPosition(positionTarget, speed).Run(endAct);
        Action toMoveGalaxyAct = () => SetPosition(new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f)), speed).Run(toMoveTarget);
        Action startAct = () => SetScale(0.5f, 0.2f).Run(toMoveTarget);
        SetScale(1f, 0.2f).Run(startAct);
    }
    #endregion
}

public interface IPlanet
{
    /// <summary>
    /// Открыть планету(получить новую планету из галактики)
    /// </summary>
    void OpenPlanet(Vector3 positionTarget, Action actFinish);
    void ConquestPlanets(Vector3 startPosition, Vector3 positionTarget, Action actFinish);
    void Destroy();
}
