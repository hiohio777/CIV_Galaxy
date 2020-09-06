using System;
using UnityEngine;
using Zenject;

public class Planet : MonoBehaviour, IPlanet
{
    [SerializeField] private MovingPlanet moving;
    [SerializeField] private SpriteRenderer art;

    private Action<Planet> buffered; 

    public class Factory : PlaceholderFactory<Action<Planet>, Planet> { }

    [Inject]
    public void Inject(Action<Planet> buffered)
    {
        this.buffered = buffered;
    }

    public Planet Initialize(TypePlanetEnum typePlanet)
    {
        switch (typePlanet)
        {
            case TypePlanetEnum.Ideal: art.color = Color.green; break;
            case TypePlanetEnum.Ice: art.color = Color.blue; break;
            case TypePlanetEnum.Hot: art.color = Color.red; break;
            case TypePlanetEnum.GasGiants: art.color = Color.cyan; break;
        }

        gameObject.SetActive(true);
        transform.position = new Vector3(0, 0, 0);

        return this;
    }

    #region IPlanet

    public Planet Hide(bool isHide)
    {
        if (isHide) art.enabled = false;
        else art.enabled = true;

        return this;
    }

    public void Destroy()
    {
        buffered.Invoke(this);
        gameObject.SetActive(false);
    }

    public MovingPlanet SetPosition(Vector3 positionTarget, float timePositionTarget) =>
        moving.SetPosition(positionTarget, timePositionTarget);
    public MovingPlanet SetScale(float scaleTarget, float timeScaleTarget) =>
        moving.SetScale(scaleTarget, timeScaleTarget);
    public void Run() => moving.Run();
    public void Run(Action execute) => moving.Run(execute);

    #endregion
}

public interface IPlanet
{
    MovingPlanet SetPosition(Vector3 positionTarget, float timePositionTarget);
    MovingPlanet SetScale(float scaleTarget, float timeScaleTarget);
    void Run();
    void Run(Action execute);
    void Destroy();
    Planet Hide(bool isOpen);
}
