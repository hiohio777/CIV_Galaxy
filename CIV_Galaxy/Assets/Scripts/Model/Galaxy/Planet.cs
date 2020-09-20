using System;
using UnityEngine;
using Zenject;

public class Planet : MonoBehaviour, IPlanet
{
    [SerializeField] private MovingObject moving;
    [SerializeField] private SpriteRenderer art; 

    private Action<Planet> _buffered;
    private Transform _transform;
    private static int sortingOrder = 1;

    public class Factory : PlaceholderFactory<Action<Planet>, Planet> { }

    [Inject]
    public void Inject(Action<Planet> buffered)
    {
        this._buffered = buffered;
        _transform = transform;
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

        art.sortingOrder = sortingOrder++;

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
        _buffered.Invoke(this);
        gameObject.SetActive(false);
    }

    public void OpenPlanet(Vector3 positionTarget, bool isHide, Action actFinish)
    {
        _transform.localScale = new Vector3(0, 0, 0);
        _transform.position = new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f), 0);

        Hide(isHide).SetScale(1.3f, 0.3f).Run(() => SetScale(1, 0.2f).Run(() => SetPosition(positionTarget, UnityEngine.Random.Range(1f, 2f)).Run(actFinish)));
    }

    public MovingObject SetPosition(Vector3 positionTarget, float timePositionTarget) =>
        moving.SetPosition(positionTarget, timePositionTarget);
    public MovingObject SetScale(float scaleTarget, float timeScaleTarget) =>
        moving.SetScale(scaleTarget, timeScaleTarget);
    public void Run() => moving.Run();
    public void Run(Action execute) => moving.Run(execute);

    #endregion
}

public interface IPlanet
{
    /// <summary>
    /// Открыть планету(получить новую планету из галактики)
    /// </summary>
    void OpenPlanet(Vector3 positionTarget, bool isHide, Action actFinish);
    MovingObject SetPosition(Vector3 positionTarget, float timePositionTarget);
    MovingObject SetScale(float scaleTarget, float timeScaleTarget);
    void Run();
    void Run(Action execute);
    void Destroy();
    Planet Hide(bool isOpen);
}
