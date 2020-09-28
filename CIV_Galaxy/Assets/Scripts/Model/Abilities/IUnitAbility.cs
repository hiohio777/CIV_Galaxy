using System;
using UnityEngine;

public interface IUnitAbility
{
    Transform TtransformUnit { get; }
    void Destroy();
    MovingObject SetPositionBezier(Vector3 positionTarget, Vector3 posBezier1, Vector3 posBezier2, float timePositionTarget);
    MovingObject SetPosition(Vector3 positionTarget, float timePositionTarget);
    MovingObject SetScale(float scaleTarget, float timeScaleTarget);
    MovingObject SetWaitForSeconds(float timeWait);
    void Run(Action execute);
}