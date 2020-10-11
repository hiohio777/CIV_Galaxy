using System;
using UnityEngine;
using Zenject;

public class UnitBase : MonoBehaviour
{
    private static int sortingOrder = 0;
    private MovingObject moving;
    private Action<object> _buffered;

    [Inject]
    public void Inject(Action<object> buffered)
    {
        this._buffered = buffered;
        TtransformUnit = transform;
        moving = GetComponent<MovingObject>();
    }

    public Transform TtransformUnit { get; private set; }
    protected int GetSortingOrder => ++sortingOrder;

    public virtual void Destroy()
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
    public MovingObject SetWaitForSeconds(float timeWait) => moving.SetWaitForSeconds(timeWait);
    public void Run() => moving.Run();
    public void Run(Action execute) => moving.Run(execute);
    #endregion
}