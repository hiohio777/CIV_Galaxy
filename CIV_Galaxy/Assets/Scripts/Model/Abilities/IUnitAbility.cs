using System;
using UnityEngine;

public interface IUnitAbility
{
    void Destroy();
    UnitBase Hide(bool isHide);
    MovingObject SetPosition(Vector3 positionTarget, float timePositionTarget);
    MovingObject SetScale(float scaleTarget, float timeScaleTarget);
    void Run(Action execute);
}