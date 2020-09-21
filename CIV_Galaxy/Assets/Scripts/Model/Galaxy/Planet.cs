using System;
using UnityEngine;
using Zenject;

public class Planet : UnitBase, IPlanet
{
    [SerializeField] protected SpriteRenderer art;

    public class Factory : PlaceholderFactory<Action<UnitBase>, Planet> { }

    public Planet Initialize(TypePlanetEnum typePlanet)
    {
        gameObject.SetActive(true);

        switch (typePlanet)
        {
            case TypePlanetEnum.Ideal: art.color = Color.green; break;
            case TypePlanetEnum.Ice: art.color = Color.blue; break;
            case TypePlanetEnum.Hot: art.color = Color.red; break;
            case TypePlanetEnum.GasGiants: art.color = Color.cyan; break;
        }

        transform.position = new Vector3(0, 0, 0);
        art.sortingOrder = GetSortingOrder;

        return this;
    }

    #region IPlanet
    public void OpenPlanet(Vector3 positionTarget, bool isHide, Action actFinish)
    {
        _transform.localScale = new Vector3(0, 0, 0);
        _transform.position = new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f), 0);

        Action endAct = () => SetScale(0f, 0.2f).Run(actFinish);
        Hide(isHide).SetScale(1.3f, 0.3f).Run(() => SetScale(1, 0.2f).Run(() => SetPosition(positionTarget, UnityEngine.Random.Range(1f, 2f)).Run(endAct)));
    }

    public void ConquestPlanets(Vector3 startPosition, Vector3 positionTarget, Action actFinish)
    {
        _transform.localScale = new Vector3(0, 0, 0);
        _transform.position = new Vector3(startPosition.x + UnityEngine.Random.Range(-30f, 30f), startPosition.y + UnityEngine.Random.Range(-30f, 30f), 0);

        float speed = UnityEngine.Random.Range(0.5f, 1f);

        Action endAct = () => SetScale(0f, 0.2f).Run(actFinish);
        Action toMoveTarget = () => SetPosition(positionTarget, speed).Run(endAct);
        Action toMoveGalaxyAct = () => SetPosition(new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f)), speed).Run(toMoveTarget);
        Action startAct = () => SetScale(0.5f, 0.2f).Run(toMoveTarget);

        Hide(false).SetScale(1f, 0.2f).Run(startAct);
    }
    #endregion
}

public interface IPlanet
{
    /// <summary>
    /// Открыть планету(получить новую планету из галактики)
    /// </summary>
    void OpenPlanet(Vector3 positionTarget, bool isHide, Action actFinish);
    void ConquestPlanets(Vector3 startPosition, Vector3 positionTarget, Action actFinish);
    void Destroy();
}
