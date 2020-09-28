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
    private float timePositionTarget, timeScaleTarget, timeWait;

    // Bezier
    private Vector3 positionStart, posBezier1, posBezier2;

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
        execute = positionAnimation = scaleAnimation = null;
    }

    public MovingObject SetPositionBezier(Vector3 positionTarget, Vector3 posBezier1, Vector3 posBezier2, float timePositionTarget)
    {
        this.posBezier1 = posBezier1;
        this.posBezier2 = posBezier2;
        this.positionTarget = positionTarget;

        this.timePositionTarget = timePositionTarget;

        positionAnimation = StartBezierPositionTarget;
        return this;
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

    public MovingObject SetWaitForSeconds(float timeWait)
    {
        this.timeWait = timeWait;
        return this;
    }
    public void Run() => Run(null);
    public void Run(Action execute)
    {
        this.execute = execute;
        positionAnimation?.Invoke();
        scaleAnimation?.Invoke();
    }

    private void StartBezierPositionTarget() => StartCoroutine(MoveToBezier());
    private void StartPositionTarget() => StartCoroutine(MoveTo());
    private void StartScaleTarget() => StartCoroutine(Resize());

    private IEnumerator MoveToBezier()
    {
        if (timeWait > 0)
        {
            yield return new WaitForSeconds(timeWait / _galaxyUITimer.GetSpeed);
            timeWait = 0;
        }

        float t = 0;
        positionStart = _transform.position;

        while (t < 1)
        {
            if (_galaxyUITimer.IsPause == false)
            {
                t = Mathf.Clamp(t + (Time.deltaTime * _galaxyUITimer.GetSpeed) / timePositionTarget, 0, 1f);

                _transform.position = Bezier.GetPoint(positionStart, posBezier1, posBezier2, positionTarget, t);
                _transform.up = Bezier.GetFirstDerivative(positionStart, posBezier1, posBezier2, positionTarget, t);
            }

            yield return new WaitForFixedUpdate();
        }

        positionAnimation = null;
        EndAnimation();
    }

    private IEnumerator MoveTo()
    {
        if (timeWait > 0)
        {
            yield return new WaitForSeconds(timeWait / _galaxyUITimer.GetSpeed);
            timeWait = 0;
        }

        float speedMove = Vector2.Distance(_transform.position, positionTarget) / timePositionTarget;

        while (_transform.position != positionTarget)
        {
            if (_galaxyUITimer.IsPause == false)
            {
                float stepMove = speedMove * (Time.deltaTime * _galaxyUITimer.GetSpeed);
                _transform.position = Vector3.MoveTowards(_transform.position, positionTarget, stepMove);
            }

            yield return new WaitForFixedUpdate();
        }

        positionAnimation = null;
        EndAnimation();
    }

    private IEnumerator Resize()
    {
        if (timeWait > 0)
        {
            yield return new WaitForSeconds(timeWait / _galaxyUITimer.GetSpeed);
            timeWait = 0;
        }

        var target = new Vector3(scaleTarget, scaleTarget, scaleTarget);
        float speedMove = Vector2.Distance(_transform.localScale, target) / timeScaleTarget;

        while (_transform.localScale != target)
        {
            if (_galaxyUITimer.IsPause == false)
            {
                float stepMove = speedMove * (Time.deltaTime * _galaxyUITimer.GetSpeed);
                _transform.localScale = Vector3.MoveTowards(_transform.localScale, target, stepMove);
            }

            yield return new WaitForFixedUpdate();
        }

        scaleAnimation = null;
        EndAnimation();
    }

    private void EndAnimation()
    {
        if (positionAnimation == null && scaleAnimation == null)
            execute?.Invoke();
    }
}
