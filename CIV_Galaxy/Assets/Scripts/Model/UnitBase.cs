using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UnitBase : MonoBehaviour
{
    [SerializeField] private GameObject unitDisplay;
    private static int sortingOrder = 0;
    private MovingObject moving;
    private Action<UnitBase> _buffered;

    [Inject]
    public void Inject(Action<UnitBase> buffered)
    {
        this._buffered = buffered;
        TtransformUnit = transform;
        moving = GetComponent<MovingObject>();
    }

    public Transform TtransformUnit { get; private set; }
    protected int GetSortingOrder => ++sortingOrder;

    public void Destroy()
    {
        _buffered.Invoke(this);
        gameObject.SetActive(false);
    }

    #region Moving
    public MovingObject SetPositionBezier(Vector3 positionTarget, Vector3 posBezier1, Vector3 posBezier2, float timePositionTarget) =>
        moving.SetPositionBezier(positionTarget, posBezier1, posBezier2, timePositionTarget);
    public MovingObject SetPosition(Vector3 positionTarget, float timePositionTarget) =>
        moving.SetPosition(positionTarget, timePositionTarget);
    public MovingObject SetScale(float scaleTarget, float timeScaleTarget) =>
        moving.SetScale(scaleTarget, timeScaleTarget);
    public void Run() => moving.Run();
    public void Run(Action execute) => moving.Run(execute);
    #endregion
}