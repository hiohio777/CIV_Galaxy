using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class MovingPlanet : MonoBehaviour
{
    private Transform _transform;
    private Action execute, StartAnimation;

    private Vector3 positionTarget;
    private float scaleTarget;
    private float timePositionTarget, timeScaleTarget;

    private IGalaxyUITimer _galaxyUITimer;

    [Inject]
    public void Inject(IGalaxyUITimer galaxyUITimer)
    {
        this._galaxyUITimer = galaxyUITimer;
    }

    public void Stop()
    {
        execute?.Invoke();
    }

    public MovingPlanet SetPosition(Vector3 positionTarget, float timePositionTarget)
    {
        this.positionTarget = positionTarget;
        this.timePositionTarget = timePositionTarget;

        StartAnimation += StartPositionTarget;
        return this;
    }

    public MovingPlanet SetScale(float scaleTarget, float timeScaleTarget)
    {
        this.scaleTarget = scaleTarget;
        this.timeScaleTarget = timeScaleTarget;

        StartAnimation += StartScaleTarget;
        return this;
    }

    public void Run() => Run(null);
    public void Run(Action execute)
    {
        this.execute = execute;
        StartAnimation?.Invoke();
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

        StartAnimation -= StartPositionTarget;
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

        StartAnimation -= StartScaleTarget;
        EndAnimation();
    }

    private void EndAnimation()
    {
        if (StartAnimation == null) Stop();
    }

    private void Awake() => _transform = GetComponent<Transform>();
}
