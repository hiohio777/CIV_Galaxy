using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class MovingObject : MonoBehaviour
{
    private Transform _transform;
    private Action execute, positionAnimation, scaleAnimation;

    private Vector3 positionTarget;
    private float scaleTarget;
    private float timePositionTarget, timeScaleTarget;

    private IGalaxyUITimer _galaxyUITimer;

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer)
    {
        this._galaxyUITimer = galaxyUITimer; 
        _transform = GetComponent<Transform>();
    }

    public void Stop()
    {
        StopAllCoroutines();
        execute = positionAnimation = scaleAnimation =null;
    }

    public MovingObject SetPosition(Vector3 positionTarget, float timePositionTarget)
    {
        this.positionTarget = positionTarget;
        this.timePositionTarget = timePositionTarget;

        positionAnimation = StartPositionTarget;
        return this;
    }

    public MovingObject SetScale(float scaleTarget, float timeScaleTarget)
    {
        this.scaleTarget = scaleTarget;
        this.timeScaleTarget = timeScaleTarget;

        scaleAnimation = StartScaleTarget;
        return this;
    }

    public void Run() => Run(null);
    public void Run(Action execute)
    {
        this.execute = execute;
        positionAnimation?.Invoke();
        scaleAnimation?.Invoke();
    }

    private void StartPositionTarget()
    {
        StartCoroutine(MoveTo());
    }

    private void StartScaleTarget()
    {
        StartCoroutine(Resize());
    }

    private IEnumerator MoveTo()
    {
        float speedMove = Vector2.Distance(_transform.position, positionTarget) / timePositionTarget;

        while (_transform.position != positionTarget)
        {
            if (_galaxyUITimer.IsPause == false)
            {
                float stepMove = speedMove * Time.deltaTime;
                _transform.position = Vector3.MoveTowards(_transform.position, positionTarget, stepMove);
            }

            yield return new WaitForFixedUpdate();
        }

        positionAnimation = null;
        EndAnimation();
    }

    private IEnumerator Resize()
    {
        var target = new Vector3(scaleTarget, scaleTarget, scaleTarget);
        float speedMove = Vector2.Distance(_transform.localScale, target) / timeScaleTarget;

        while (_transform.localScale != target)
        {
            if (_galaxyUITimer.IsPause == false)
            {
                float stepMove = speedMove * Time.deltaTime;
                _transform.localScale = Vector3.MoveTowards(_transform.localScale, target, stepMove);
            }

            yield return new WaitForFixedUpdate();
        }

        scaleAnimation = null;
        EndAnimation();
    }

    private void EndAnimation()
    {
        if (positionAnimation == null && scaleAnimation == null) execute?.Invoke();
    }
}
