using System;
using UnityEngine;
using Zenject;

public class UnitBase : MonoBehaviour
{
    [SerializeField] private GameObject unitDisplay;

    protected Transform _transform;
    private static int sortingOrder = 0;
    private MovingObject moving;
    private Action<UnitBase> _buffered;

    [Inject]
    public void Inject(Action<UnitBase> buffered)
    {
        this._buffered = buffered;
        _transform = transform;
        moving = GetComponent<MovingObject>();
    }

    protected int GetSortingOrder => ++sortingOrder;

    public void Destroy()
    {
        _buffered.Invoke(this);
        gameObject.SetActive(false);
    }

    public UnitBase Hide(bool isHide)
    {
        if (isHide) unitDisplay.SetActive(false);
        else unitDisplay.SetActive(true);

        return this;
    }

    #region Moving
    public MovingObject SetPosition(Vector3 positionTarget, float timePositionTarget) =>
        moving.SetPosition(positionTarget, timePositionTarget);
    public MovingObject SetScale(float scaleTarget, float timeScaleTarget) =>
        moving.SetScale(scaleTarget, timeScaleTarget);
    public void Run() => moving.Run();
    public void Run(Action execute) => moving.Run(execute);
    #endregion
}
